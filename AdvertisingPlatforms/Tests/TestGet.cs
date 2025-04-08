using Core.Storage;

namespace Tests;

public class TestGet
{
    private LocationStorage _storage;
    public TestGet()
    {
        _storage = new LocationStorage();
        string testData = "Глобальная реклама:/ru\n" +
                          "Реклама в метро:/ru/msk/metro,/ru/spb/metro\n" +
                          "Московские новости:/ru/msk\n" +
                          "Петербургский вестник:/ru/spb";
        
        var result = _storage.Add(testData);
    }
    
    [Fact]
    public void GetCorrectPlatform()
    {
        var result = _storage.Get("/ru");
        List<string> checkPlatforms = new() { "Глобальная реклама" };
        
        Assert.True(result.Item1);
        Assert.Equal(checkPlatforms, result.Item2);
        
    }
    
    [Fact]
    public void GetCorrectPlatforms()
    {
        var result = _storage.Get("/ru/spb/metro");
        
        List<string> checkPlatforms = new()
        {
            "Глобальная реклама",
            "Реклама в метро",
            "Петербургский вестник"
        };
        
        Assert.True(result.Item1);
        Assert.Equal(checkPlatforms.Count, result.Item2.Count);
        checkPlatforms.ForEach(item => Assert.Contains(item, result.Item2));
    }
    
    [Fact]
    public void GetIncorrectPlatforms()
    {
        var result = _storage.Get("/error");
        
        Assert.False(result.Item1);
        Assert.Null(result.Item2);
    }
}