namespace Frustration.BusinessLogic;

public class GameService : IGameService
{
    public Game CompleteRound(Game currentGame, IEnumerable<Guid> playerCompletedRoundIds, IEnumerable<(Guid playerId, uint points)>? points)
    {
        if (!playerCompletedRoundIds.Any())
            throw new ArgumentException("No players completed the round. At least one player has to complete the round.", nameof(playerCompletedRoundIds));

        if (currentGame.TrackPoints)
        {
            if (points == null)
                throw new ArgumentException("Points should be included", nameof(points));

            if(points.Any(p => p.points % 5 != 0))
                throw new ArgumentException("Points were not correct. Points should always be a multiple of 5", nameof(points));

            if (points.Count() != currentGame.Players.Count())
                throw new ArgumentException("Points are missing for 1 or more players", nameof(points));

            if (points.Any(p => !currentGame.Players.Select(player => player.Id).Contains(p.playerId)))
                throw new ArgumentException("Points were given to a player who isn't part of the current game", nameof(points));

            foreach (var p in currentGame.Players)
            {
                var score = points.Single(s => s.playerId.Equals(p.Id));

                p.IncrementScore(score.points);
            }
        }

        foreach(var player in currentGame.Players.Where(p => playerCompletedRoundIds.Contains(p.Id)))
        {
            player.CompleteTask();
        }

        return currentGame;
    }

    public Game StartNewGame(IEnumerable<string> players, bool trackPoints)
    {
        if (!players.Any())
            throw new ArgumentException("You need at least one player", nameof(players));

        var playerObjects = players.Select(p => new Player(p));

        return new Game(playerObjects, trackPoints);
    }
}
