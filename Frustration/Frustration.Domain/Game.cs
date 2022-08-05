namespace Frustration.Domain;

public class Game
{
    public Game(IEnumerable<Player> players, bool trackPoints)
    {
        Players = players;
        Rounds = new Stack<Round>();
        TrackPoints = trackPoints;
    }

    public IEnumerable<Player> Players { get; set; }

    public Stack<Round> Rounds { get; set; }

    public bool TrackPoints { get; set; }
}
