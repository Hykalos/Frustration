namespace Frustration.Web.Controllers;

public sealed class HomeController : Controller
{
    private readonly GameStorage _gameStorage;

    public HomeController(GameStorage gameStorage)
    {
        _gameStorage = gameStorage;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeIndexViewModel
        {
            GameExists = _gameStorage.GameExists(Request)
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}