namespace Api.Core.Models;

    public class LocationModel
    {
        public string Location { get; set; }
        public HashSet<string> Platforms { get; set; }
        public List<string> Parent { get; set; }

        public LocationModel(string location, string platform)
        {
            Location = location;
            
            Platforms = new HashSet<string>();
            Platforms.Add(platform);
            
            Parent = FindParents(location);
        }

        private List<string> FindParents(string location)
        {
            var parents = new List<string>();
            
            var splitLocation = location.Split('/');
            for (int i = 1; i < splitLocation.Length - 1; i++)
            {
                if(i == 1)
                    parents.Add($"/{splitLocation[i]}");
                else
                    parents.Add($"{parents[i - 2]}/{splitLocation[i]}");
            }
        
            return parents;
        }

}