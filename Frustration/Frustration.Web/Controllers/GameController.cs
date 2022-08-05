namespace Frustration.Web.Controllers;

public sealed class GameController : Controller
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    public IActionResult NewGame()
    {
        return View();
    }

    [HttpPost]
    public IActionResult StartNewGame([FromForm] IEnumerable<string> playerNames, [FromForm] string trackPoints)
    {
        var game = _gameService.StartNewGame(playerNames, trackPoints?.Equals("on", StringComparison.InvariantCultureIgnoreCase) ?? false);

        //TODO: store game state
        
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Index()
    {
        return View();
    }
}
