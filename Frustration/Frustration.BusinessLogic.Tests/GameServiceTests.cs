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
        Assert.Throws<ArgumentException>(() => _gameService.CompleteRound(_game, Enumerable.Empty<Guid>(), GenerateValidPoints(_game)));
    }

    [Test]
    public void CompleteRoundCorrectPersonsCompletedTask()
    {
        var player1CurrentTask = _game.Players.ElementAt(0).CurrentTask;
        var player2CurrentTask = _game.Players.ElementAt(1).CurrentTask;
        var player3CurrentTask = _game.Players.ElementAt(2).CurrentTask;

        var completedRound = new List<Guid>
        {
            _game.Players.ElementAt(0).Id,
            _game.Players.ElementAt(2).Id
        };


        var newGameState = _gameService.CompleteRound(_game, completedRound, GenerateValidPoints(_game));

        Assert.Multiple(() =>
        {
            Assert.That(_game.Players.ElementAt(0).CurrentTask, Is.EqualTo(player1CurrentTask + 1));
            Assert.That(_game.Players.ElementAt(1).CurrentTask, Is.EqualTo(player2CurrentTask));
            Assert.That(_game.Players.ElementAt(2).CurrentTask, Is.EqualTo(player3CurrentTask + 1));
        });
    }

    [Test]
    public void CompleteRoundCorrectTotalScorePersons()
    {
        var player1CurrentScore = _game.Players.ElementAt(0).CurrentScore;
        var player2CurrentScore = _game.Players.ElementAt(1).CurrentScore;
        var player3CurrentScore = _game.Players.ElementAt(2).CurrentScore;

        var completedRound = new List<Guid>
        {
            _game.Players.ElementAt(0).Id,
            _game.Players.ElementAt(2).Id
        };

        const uint player1Score = 155;
        const uint player2Score = 155;
        const uint player3Score = 155;
        var scores = new List<(Guid, uint)>
        {
            (_game.Players.ElementAt(0).Id, player1Score),
            (_game.Players.ElementAt(1).Id, player2Score),
            (_game.Players.ElementAt(2).Id, player3Score)
        };


        var newGameState = _gameService.CompleteRound(_game, completedRound, scores);

        Assert.Multiple(() =>
        {
            Assert.That(_game.Players.ElementAt(0).CurrentScore, Is.EqualTo(player1CurrentScore + player1Score));
            Assert.That(_game.Players.ElementAt(1).CurrentScore, Is.EqualTo(player2CurrentScore + player2Score));
            Assert.That(_game.Players.ElementAt(2).CurrentScore, Is.EqualTo(player3CurrentScore + player3Score));
        });
    }

    private static IEnumerable<(Guid, uint)> GenerateValidPoints(Game game)
    {
        var points = new List<(Guid, uint)>();

        foreach(var player in game.Players)
        {
            points.Add((player.Id, 45));
        }

        return points;
    }
}