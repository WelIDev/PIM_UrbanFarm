using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class MenuController : Controller
{
    private readonly ILogger<MenuController> _logger;

    public MenuController(ILogger<MenuController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}
