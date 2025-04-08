using Api.Core.Models;

namespace Api.Core.Storage;

public class LocationStorage
{
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

    public void  Add(string content)
    {
        var lines = content.Split('\n');
        foreach (var line in lines)
        {
            CreateLocation(line);
        }
    }

    private void CreateLocation(string line)
    {
        var platformLocation = line.Split(':');
        var platform = platformLocation[0];
        var locations= platformLocation[1];
        var splitLocations = locations.Split(',');
        

        foreach (var location in splitLocations)
        {
            if(!Locations.ContainsKey(location))
                Locations.Add(location, new LocationModel(location, platform));
            else
                Locations[location].Platforms.Add(platform);
        }
        
        
        
    }
    
    
    
}