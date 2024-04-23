using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using L2.Models;
using project.Models.DataBase;
using project.Models.Context;

namespace L2.Controllers;

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
    public IActionResult gotoaddstudent()
    {
        return View();
    }
    public IActionResult addstudent(project.Models.Models.Vm_Student st)
    {
        Tbl_Student one = new Tbl_Student();
        one.Phone = st.Vm_Phone;
        one.Name = st.Vm_Name;
        one.Family = st.Vm_Family;
        one.ClassCode = st.Vm_ClassCode;
        db.tbl_student.Add(one);
        db.SaveChanges();
        return View();
    }
    public IActionResult ShowStudent()
    {
        ViewBag.st = db.tbl_student.ToList();
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
