using Microsoft.AspNetCore.Mvc;

namespace LearnWords.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}

