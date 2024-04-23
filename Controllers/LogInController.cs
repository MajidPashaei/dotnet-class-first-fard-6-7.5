using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using L2.Models;
using project.Models.DataBase;
using project.Models.Context;
using Microsoft.AspNetCore.Authorization;
using project.Models.Models;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace L2.Controllers;

public class LogInController : Controller
{
    private readonly ILogger<LogInController> _logger;
    private readonly Application db;

    public LogInController(ILogger<LogInController> logger, Application _db)
    {
        _logger = logger;
        db = _db;
    }

    public IActionResult login()
    {
        return View();
    }
    public IActionResult check(Vm_User vm)
    {
        if (!db.tbl_users.Any(p=>p.Email == vm.Vm_Email && p.Password == vm.Vm_Password))
        {
            return RedirectToAction("login");
        }
        // یافتن کاربر
        var find = db.tbl_users.SingleOrDefault(p=>p.Email == vm.Vm_Email && p.Password == vm.Vm_Password);
        // احراز کاربر
        var claims = new List<Claim>()
        {
        new Claim (ClaimTypes.NameIdentifier, find.Id.ToString ()),
        new Claim (ClaimTypes.Name, find.Name),
        new Claim (ClaimTypes.Email, find.Email),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var properties = new AuthenticationProperties
        {
            IsPersistent = true
        };
        HttpContext.SignInAsync(principal, properties);
        return RedirectToAction("index", "Profile");
    }
    public IActionResult adduser()
    {
        return View();
    }
    public IActionResult register(Vm_User vm)
    {
        if (db.tbl_users.Any(p=>p.Email == vm.Vm_Email))
        {
            return RedirectToAction("login");
        }
        if (vm.Vm_Password != vm.Vm_RePassword)
        {
            return RedirectToAction("adduser");
        }
        Tbl_User n = new Tbl_User();
        n.Name = vm.Vm_Name;
        n.Phone = vm.Vm_Phone;
        n.Password = vm.Vm_Password;
        n.Email = vm.Vm_Email;
        db.tbl_users.Add(n);
        db.SaveChanges();
        return RedirectToAction("login");
    }
    public IActionResult exit()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("index", "home");
    }
    
    
    
    
}
