namespace SalesWebMvc.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SeedingService _ss;
    public HomeController(ILogger<HomeController> logger, SeedingService ss)
    {
        _logger = logger;
        _ss = ss;
    }

    public IActionResult Index()
    {
        //_ss.Seed();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
