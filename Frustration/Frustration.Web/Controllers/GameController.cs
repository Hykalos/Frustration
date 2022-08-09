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
    public IActionResult CompleteRound([FromForm] IEnumerable<Guid> completedRounds, [FromForm] IEnumerable<PlayerRoundInfo> roundInfo)
    {


        return Redirect(nameof(Index));
    }
}
