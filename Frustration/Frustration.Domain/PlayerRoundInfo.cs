namespace Frustration.Domain;

public class PlayerRoundInfo
{
    public Guid PlayerId { get; set; }
    public bool CompletedTask { get; set; }
    public uint? Score { get; set; }
}
