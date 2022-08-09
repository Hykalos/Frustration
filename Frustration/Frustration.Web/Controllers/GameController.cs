namespace Frustration.Web.Controllers;

public sealed class GameController : Controller
{
    private readonly IGameService _gameService;
    private readonly GameStorage _gameStorage;

    public GameController(IGameService gameService, GameStorage gameStorage)
    {
        _gameService = gameService;
        _gameStorage = gameStorage;
    }

    public IActionResult NewGame()
    {
        return View();
    }

    [HttpPost]
    public IActionResult StartNewGame([FromForm] IEnumerable<string> playerNames, [FromForm] string trackPoints)
    {
        var game = _gameService.StartNewGame(playerNames, trackPoints?.Equals("on", StringComparison.InvariantCultureIgnoreCase) ?? false);

        _gameStorage.StoreGame(game, Response.Cookies);
        
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Index()
    {
        var game = _gameStorage.LoadGame(Request);

        return View(game);
    }

    [HttpPost]
    public IActionResult CompleteRound([FromForm] IEnumerable<PlayerRoundInfoDto> roundInfo)
    {
        var game = _gameStorage.LoadGame(Request);

        if (game == null)
            return Redirect(nameof(NewGame));

        var newGameState = _gameService.CompleteRound(game, roundInfo.Select(ri => new PlayerRoundInfo
        {
            CompletedTask = ri.CompletedTask,
            PlayerId = ri.PlayerId,
            Score = ri.Score
        }));

        _gameStorage.StoreGame(newGameState, Response.Cookies);

        return Redirect(nameof(Index));
    }
}
