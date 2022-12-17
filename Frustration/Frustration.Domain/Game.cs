namespace Frustration.Domain;

public class Game
{
    private Game() { }

    public Game(IEnumerable<Player> players, bool trackPoints)
    {
        Players = players;
        TrackPoints = trackPoints;
    }

    public IEnumerable<Player> Players { get; set; } = Enumerable.Empty<Player>();

    public bool TrackPoints { get; set; }

    public static Game Empty => new Game();
}
