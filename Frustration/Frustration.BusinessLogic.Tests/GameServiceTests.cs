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
            Assert.That(game.TrackPoints, Is.True);
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
            Assert.That(game.TrackPoints, Is.False);
        });
    }

    [Test]
    public void CompleteRoundWrongPlayerCountThrowsError()
    {
        Assert.Throws<ArgumentException>(() => _gameService.CompleteRound(_game, new List<PlayerRoundInfo>
        {
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(0).Id,
                CompletedTask = true,
                Score = 45
            }
        }));
    }

    [Test]
    public void CompleteRoundNoCompletedTasksThrowsError()
    {
        var playerRoundInfo = new List<PlayerRoundInfo>
        {
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(0).Id,
                CompletedTask = false,
                Score = 45
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(1).Id,
                CompletedTask = false,
                Score = 45
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(2).Id,
                CompletedTask = false,
                Score = 45
            }
        };


        Assert.Throws<ArgumentException>(() => _gameService.CompleteRound(_game, playerRoundInfo));
    }

    [Test]
    public void CompleteRoundCorrectPersonsCompletedTask()
    {
        var player1CurrentTask = _game.Players.ElementAt(0).CurrentTask;
        var player2CurrentTask = _game.Players.ElementAt(1).CurrentTask;
        var player3CurrentTask = _game.Players.ElementAt(2).CurrentTask;

        var playerRoundInfo = new List<PlayerRoundInfo>
        {
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(0).Id,
                CompletedTask = true,
                Score = 45
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(1).Id,
                CompletedTask = false,
                Score = 45
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(2).Id,
                CompletedTask = true,
                Score = 45
            }
        };


        var game = _gameService.CompleteRound(_game, playerRoundInfo);

        Assert.Multiple(() =>
        {
            Assert.That(game.Players.ElementAt(0).CurrentTask, Is.EqualTo(player1CurrentTask + 1));
            Assert.That(game.Players.ElementAt(1).CurrentTask, Is.EqualTo(player2CurrentTask));
            Assert.That(game.Players.ElementAt(2).CurrentTask, Is.EqualTo(player3CurrentTask + 1));
        });
    }

    [Test]
    public void CompleteRoundCorrectTotalScorePersons()
    {
        var player1CurrentScore = _game.Players.ElementAt(0).CurrentScore;
        var player2CurrentScore = _game.Players.ElementAt(1).CurrentScore;
        var player3CurrentScore = _game.Players.ElementAt(2).CurrentScore;

        const uint player1Score = 155;
        const uint player2Score = 420;
        const uint player3Score = 0;

        var playerRoundInfo = new List<PlayerRoundInfo>
        {
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(0).Id,
                CompletedTask = true,
                Score = player1Score
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(1).Id,
                CompletedTask = false,
                Score = player2Score
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(2).Id,
                CompletedTask = true,
                Score = player3Score
            }
        };


        var newGameState = _gameService.CompleteRound(_game, playerRoundInfo);

        Assert.Multiple(() =>
        {
            Assert.That(newGameState.Players.ElementAt(0).CurrentScore, Is.EqualTo(player1CurrentScore + player1Score));
            Assert.That(newGameState.Players.ElementAt(1).CurrentScore, Is.EqualTo(player2CurrentScore + player2Score));
            Assert.That(newGameState.Players.ElementAt(2).CurrentScore, Is.EqualTo(player3CurrentScore + player3Score));
        });
    }

    [Test]
    public void GameCompletedReturnsTrueOnCompletedGame()
    {
        var playerRoundInfo = new List<PlayerRoundInfo>
        {
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(0).Id,
                CompletedTask = true,
                Score = 0
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(1).Id,
                CompletedTask = false,
                Score = 200
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(2).Id,
                CompletedTask = false,
                Score = 250
            }
        };

        Game gameState = _game;

        for(var i = 0; i < 20; ++i)
        {
            gameState = _gameService.CompleteRound(gameState, playerRoundInfo);
        }


        var gameCompleted = _gameService.GameIsOver(gameState, out var _);
        Assert.That(gameCompleted, Is.True);
    }

    [Test]
    public void GameCompletedReturnsFalseOnNotCompletedGame()
    {
        var playerRoundInfo = new List<PlayerRoundInfo>
        {
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(0).Id,
                CompletedTask = true,
                Score = 0
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(1).Id,
                CompletedTask = false,
                Score = 200
            },
            new PlayerRoundInfo
            {
                PlayerId = _game.Players.ElementAt(2).Id,
                CompletedTask = false,
                Score = 250
            }
        };

        Game gameState = _game;

        for (var i = 0; i < 19; ++i)
        {
            gameState = _gameService.CompleteRound(gameState, playerRoundInfo);
        }


        var gameCompleted = _gameService.GameIsOver(gameState, out var _);
        Assert.That(gameCompleted, Is.False);
    }
}