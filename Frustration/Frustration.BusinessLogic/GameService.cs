namespace Frustration.BusinessLogic;

public class GameService : IGameService
{
    public Game CompleteRound(Game currentGame, IEnumerable<PlayerRoundInfo> playerRoundInfo)
    {
        if (!playerRoundInfo.Any(p => p.CompletedTask))
            throw new ArgumentException("No players completed the round. At least one player has to complete the round.", nameof(playerRoundInfo));

        if (playerRoundInfo.Count() != currentGame.Players.Count())
            throw new ArgumentException("Player count didn't match player round info count");

        if (playerRoundInfo.Any(p => !currentGame.Players.Select(player => player.Id).Contains(p.PlayerId)))
            throw new ArgumentException("Info were given to a player who isn't part of the current game", nameof(playerRoundInfo));

        if (currentGame.TrackPoints)
        {
            if (playerRoundInfo.Any(p => p.Score == null))
                throw new ArgumentException("Points should be included", nameof(playerRoundInfo));

            if(playerRoundInfo.Any(p => p.Score % 5 != 0))
                throw new ArgumentException("Points were not correct. Points should always be a multiple of 5", nameof(playerRoundInfo));
        }

        foreach(var player in currentGame.Players)
        {
            var roundInfo = playerRoundInfo.Single(p => p.PlayerId.Equals(player.Id));

            if(roundInfo.CompletedTask)
                player.CompleteTask();

            if (currentGame.TrackPoints)
                player.IncrementScore(roundInfo.Score.Value);
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
