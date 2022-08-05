namespace Frustration.Domain;

public class Game
{
    public Game(IEnumerable<Player> players)
    {
        Players = players;
        Rounds = new Stack<Round>();
    }

    public IEnumerable<Player> Players { get; set; }

    public Stack<Round> Rounds { get; set; }
}
