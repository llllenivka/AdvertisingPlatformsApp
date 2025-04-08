using System.IO.Pipes;
using Core.Storage;

namespace Test;

public class Tests
{
    [Test]
    public void CorrectPostPlatform()
    {
        LocationStorage _storage = new LocationStorage();
        Assert.Pass();
    }
}