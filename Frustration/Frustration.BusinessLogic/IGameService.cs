namespace Frustration.BusinessLogic;

public interface IGameService
{
    /// <summary>
    /// Start a new game of Frustration
    /// </summary>
    /// <param name="players">The list of player names</param>
    Game StartNewGame(IEnumerable<string> players, bool trackPoints);

    /// <summary>
    /// Complete a round
    /// </summary>
    /// <param name="currentGame">The current state of the game</param>
    /// <param name="playerRoundInfo">Info for each player for the round</param>
    /// <exception cref="ArgumentException"></exception>
    Game CompleteRound(Game currentGame, IEnumerable<PlayerRoundInfo> playerRoundInfo);

    /// <summary>
    /// Check whether the game is over
    /// </summary>
    /// <param name="currentGame">The current state of the game</param>
    /// <param name="winner">The reference to the winner. null if no winner</param>
    bool GameIsOver(Game currentGame, out Player? winner);
}
