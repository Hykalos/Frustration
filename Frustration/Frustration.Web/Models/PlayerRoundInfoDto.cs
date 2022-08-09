namespace Frustration.Web.Models;

public class PlayerRoundInfoDto
{
    public Guid PlayerId { get; set; }
    public string CompletedTaskSwitch { get; set; } = string.Empty;
    public bool CompletedTask => CompletedTaskSwitch.Equals("on", StringComparison.InvariantCultureIgnoreCase);
    public uint? Score { get; set; }
}
