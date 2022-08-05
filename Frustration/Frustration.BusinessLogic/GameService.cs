namespace Frustration.BusinessLogic;

public class GameService : IGameService
{
    public Game CompleteRound(Game currentGame, IEnumerable<Guid> playerCompletedRoundIds, IEnumerable<(Guid playerId, uint points)>? points)
    {
        throw new NotImplementedException();
    }

    public Game StartNewGame(IEnumerable<string> players, bool trackPoints)
    {
        if (!players.Any())
            throw new ArgumentException("You need at least one player", nameof(players));

        var playerObjects = players.Select(p => new Player(p));

        return new Game(playerObjects, trackPoints);
    }
}
