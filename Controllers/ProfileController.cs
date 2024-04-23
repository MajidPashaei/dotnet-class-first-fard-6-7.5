using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using L2.Models;
using project.Models.DataBase;
using project.Models.Context;
using Microsoft.AspNetCore.Authorization;
namespace L2.Controllers;
[Authorize]
public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly Application db;

    public ProfileController(ILogger<ProfileController> logger, Application _db)
    {
        _logger = logger;
        db = _db;
    }

    public IActionResult Index()
    {
        return View();
    }
    
}
