namespace Frustration.Domain;

public class Round
{
    public Round(IEnumerable<PlayerRoundInfo> playerRoundInfo)
    {
        if (!playerRoundInfo.Any())
            throw new ArgumentException("At least one player has to complete the round");

        PlayerRoundInfo = playerRoundInfo;
    }

    public IEnumerable<PlayerRoundInfo> PlayerRoundInfo { get; set; } = Enumerable.Empty<PlayerRoundInfo>();
}
