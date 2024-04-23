using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using L2.Models;
using project.Models.DataBase;
using project.Models.Context;
using project.Models.Models;
using L2.Models.Tools;
using WebCore.Tools;

namespace L2.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly Application db;
    private readonly IWebHostEnvironment en;

    public ProductController(ILogger<ProductController> logger, Application _db, IWebHostEnvironment _en)
    {
        _logger = logger;
        db = _db;
        en = _en;
    }
    public FileStreamResult GetCaptchaImage()
    {
        int width = 100;
        int height = 50;
        var captchaCode = Captcha.GenerateCaptchaCode();
        var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
        HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);
        string b = HttpContext.Session.GetString("CaptchaCode");
        Stream s = new MemoryStream(result.CaptchaByteData);
        return new FileStreamResult(s, "image/png");
    }
    public IActionResult gotoaddproduct()
    {
        return View();
    }
    public IActionResult addproduct(Vm_Product vm)
    {
        if (!Captcha.ValidateCaptchaCode(vm.Vm_CaptchaCode, HttpContext))
        {
            return RedirectToAction("gotoaddproduct");
        }
        Tbl_Product product = new Tbl_Product();
        product.Name = vm.Vm_Name;
        product.Color = vm.Vm_Color;
        product.Count = vm.Vm_Count;
        product.Price = vm.Vm_Price;
        product.Status = vm.Vm_Status;
        if (vm.Vm_IMG != null)
        {
            Upload up = new Upload(en);
            product.Image = up.Upload_Webp_Orginal(vm.Vm_IMG, "Product").Result;
        }
        else
        {
            product.Image = "/product/avatarproduct.png";
        }
        db.tbl_Products.Add(product);
        db.SaveChanges();

        return RedirectToAction("index");
    }
    public IActionResult index()
    {
        ViewBag.p = db.tbl_Products.ToList();
        return View();
    }
    public IActionResult delete(int id)
    {
        var find = db.tbl_Products.SingleOrDefault(p => p.Id == id);
        if (find.Image != "/product/avatarproduct.png")
        {
            Delete delete = new Delete(en);
            delete.Delete_Image(find.Image);
        }
        db.tbl_Products.Remove(find);
        db.SaveChanges();
        return RedirectToAction("index");
    }
    public IActionResult gotoupdate(int id)
    {
        var find = db.tbl_Products.SingleOrDefault(p => p.Id == id);
        Vm_Product product = new Vm_Product();
        product.Vm_Id = find.Id;
        product.Vm_Name = find.Name;
        product.Vm_Price = find.Price;
        product.Vm_Status = find.Status;
        product.Vm_Color = find.Color;
        product.Vm_Count = find.Count;
        product.Vm_Image = find.Image;
        return View(product);
    }
    public IActionResult update(Vm_Product vm)
    {
        var find = db.tbl_Products.SingleOrDefault(p => p.Id == vm.Vm_Id);
        find.Name = vm.Vm_Name;
        find.Color = vm.Vm_Color;
        find.Count = vm.Vm_Count;
        find.Price = vm.Vm_Price;
        find.Status = vm.Vm_Status;
        if (vm.Vm_IMG != null)
        {
            if (find.Image != "/product/avatarproduct.png")
            {
                Delete delete = new Delete(en);
                delete.Delete_Image(find.Image);
            }
            Upload up = new Upload(en);
            find.Image = up.Upload_Webp_Orginal(vm.Vm_IMG, "Product").Result;
        }
        db.tbl_Products.Update(find);
        db.SaveChanges();
        return RedirectToAction("index");
    }
    public IActionResult status(int id)
    {
        var find = db.tbl_Products.SingleOrDefault(p => p.Id == id);
        find.Status = !find.Status;
        db.tbl_Products.Update(find);
        db.SaveChanges();
        return RedirectToAction("index");
    }






}
