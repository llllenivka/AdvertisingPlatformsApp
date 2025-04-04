namespace Core.Models;

public class PlatformModel
{
    public readonly Guid Id;
    public string Name { get; private set; }

    public PlatformModel(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
}