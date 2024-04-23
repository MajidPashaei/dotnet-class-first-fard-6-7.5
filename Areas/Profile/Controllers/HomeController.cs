using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using L2.Models;
using project.Models.DataBase;
using project.Models.Context;

namespace L2.Areas.Profile.Controllers;
[Area("Profile")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Application db;

    public HomeController(ILogger<HomeController> logger, Application _db)
    {
        _logger = logger;
        db = _db;
    }

    public IActionResult Index()
    {
        return View();
    }
   
}
