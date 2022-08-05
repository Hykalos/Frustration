namespace Frustration.Domain;

public class Player
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public uint CurrentScore { get; set; }
    public uint CurrentTask { get; set; }
}
