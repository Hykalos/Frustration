namespace Frustration.BusinessLogic.Tests;

public class GameServiceTests
{
    private GameService _gameService;
    private Game _game;


    [SetUp]
    public void Setup()
    {
        _gameService = new GameService();

        var players = new List<Player>
        {
            new Player("Player 1"),
            new Player("Player 2"),
            new Player("Player 3")
        };

        _game = new Game(players, true);
    }

    [Test]
    public void StartNewGameTrackPointsTrue()
    {
        var game = _gameService.StartNewGame(new List<string>
        {
            "Player 1",
            "Player 2",
            "Player 3"
        }, true);

        Assert.Multiple(() =>
        {
            Assert.That(game.Players.Count(), Is.EqualTo(3));
            Assert.IsTrue(game.TrackPoints);
        });
    }

    [Test]
    public void StartNewGameTrackPointsFalse()
    {
        var game = _gameService.StartNewGame(new List<string>
        {
            "Player 1",
            "Player 2",
            "Player 3"
        }, false);

        Assert.Multiple(() =>
        {
            Assert.That(game.Players.Count(), Is.EqualTo(3));
            Assert.IsFalse(game.TrackPoints);
        });
    }

    [Test]
    public void CompleteRoundWrongCountPlayerPointsThrowsError()
    {
        Assert.Throws<ArgumentException>(() => _gameService.CompleteRound(_game, _game.Players.Select(p => p.Id), new List<(Guid, uint)>
        {
            (_game.Players.First().Id, 420)
        }));
    }

    [Test]
    public void CompleteRoundNoCompletedTasksThrowsError()
    {
        Assert.Throws<ArgumentException>(() => _gameService.CompleteRound(_game, Enumerable.Empty<Guid>(), ))
    }

    private static IEnumerable<(Guid, uint)> GenerateValidPoints(Game game)
    {
        var points = new List<(Guid, uint)>();

        foreach(var player in game.Players)
        {

        }

        return points;
    }
}