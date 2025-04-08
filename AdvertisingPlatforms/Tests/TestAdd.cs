using Core.Storage;

namespace Tests;

public class TestAdd
{
    [Fact]
    public void AddCorrectPlatform()
    {
        LocationStorage _storage = new LocationStorage();
        string testData = "Глобальная реклама:/ru";
        var result = _storage.Add(testData);
        foreach (var item in result)
        {
            Assert.True(item.Item1);
            Assert.Contains(testData, item.Item2);
        }
    }
    
    [Fact]
    public void AddCorrectPlatforms()
    {
        LocationStorage _storage = new LocationStorage();
        string testData = "Глобальная реклама:/ru\n" +
                          "Яндекс.Директ:/ru,/ru/msk";
        var checkData = testData.Split('\n');
        var result = _storage.Add(testData);
        
        for (int i = 0; i < result.Count; i++)
        {
            Assert.True(result[i].Item1);
            Assert.Contains(checkData[i], result[i].Item2);
        }
    }
    
    [Fact]
    public void AddIncorrectPlatforms()
    {
        LocationStorage _storage = new LocationStorage();
        string testData = "/ru\n" +
                          "Яндекс.Директ:ru,ru/msk\n" +
                          "Крымские издания:/ru//crimea\n" +
                          "Электробусы:/ru/msk/electro  buses\n" +
                          "БайкалМедиа:/ru/irkutsk,/ru/chita/";
        var checkData = testData.Split('\n');
        var result = _storage.Add(testData);
        
        for (int i = 0; i < result.Count; i++)
        {
            Assert.False(result[i].Item1);
            Assert.Contains(checkData[i], result[i].Item2);
        }
    }
}