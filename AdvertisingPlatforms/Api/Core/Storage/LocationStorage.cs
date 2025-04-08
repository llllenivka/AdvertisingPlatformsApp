using Api.Core.Models;

namespace Api.Core.Storage;

public class LocationStorage
{
    const string ALLOWED_SYMBOLS = "/abcdefghijklmnopqrstuvwxyz";
    
    private Dictionary<string, LocationModel> Locations;
    public int Count => Locations.Count;

    public LocationStorage()
    {
        Locations = new Dictionary<string, LocationModel>();
    }

    public Dictionary<string, LocationModel> Get()
    {
        return Locations;
    }

    public (bool, List<string>) Get(string location)
    {
        List<string> platforms = new();

        AddPlatforms(location, platforms);

        if (Locations.ContainsKey(location))
        {
            foreach (var parent in Locations[location].Parent)
            {
                AddPlatforms(parent, platforms);
            }
        }
        
        if(platforms.Count > 0)
            return (true, platforms);
        
        return (false, null);
    }

    private void AddPlatforms(string location, List<string> platforms)
    {
        if (Locations.ContainsKey(location))
        {
            foreach (var item in Locations[location].Platforms)
            {
                platforms.Add(item);
            }
        }
    }

    public List<(bool, string)>  Add(string content)
    {
        if (Locations.Count != 0) Locations.Clear();
        List<(bool, string)> result = new();
        var lines = content.Split('\n');
        foreach (var line in lines)
        {
            if (CreateLocation(line))
            {
                result.Add((true, line));
            }
            else
            {
                result.Add((false, line));
            }
            
        }
        
        return result;
    }

    private bool CreateLocation(string line)
    {
        var platformLocation = line.Split(':');

        if (!IsCorrectLine(platformLocation))
        {
            return false;
        }
        
        var platform = platformLocation[0];
        var locations= platformLocation[1];
        
        var splitLocations = locations.Split(',');

        if (!IsCorrectLocations(splitLocations))
        {
            return false;
        }
        
        foreach (var location in splitLocations)
        {
            if(!Locations.ContainsKey(location))
                Locations.Add(location, new LocationModel(location, platform));
            else
                Locations[location].Platforms.Add(platform);
        }
        
        return true;
    }

    private bool IsCorrectLine(string[] line)
    {
        if (line.Length != 2
            || string.IsNullOrEmpty(line[0])
            || string.IsNullOrEmpty(line[1]))
        {
            return false;
        }
        return true;
    }

    private bool IsCorrectLocations(string[] locations)
    {
        foreach (var location in locations)
        {
            if(string.IsNullOrEmpty(location))
                return false;

            if (!location.All(sym => ALLOWED_SYMBOLS.Contains(sym)))
                return false;
            
            if(location.Contains("//"))
                return false;
            
            if(location[0] != '/' || location[^1] == '/')
                return false;

        }
        
        return true;
    }
    
    
    
}