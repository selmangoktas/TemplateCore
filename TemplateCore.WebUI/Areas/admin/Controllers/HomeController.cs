using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TemplateCore.BL.Repositories;
using TemplateCore.DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TemplateCore.WebUI.Tools;
namespace Divisima.WebUI.Areas.admin.Controllers
{
    [Area("admin"), Route("/admin/[controller]/[action]"), Authorize]
    public class HomeController : Controller
    {
        WebRepository<Admin> adminRepo;
        public HomeController(WebRepository<Admin> _adminRepo)
        {
            adminRepo = _adminRepo;
        }

        [Route("/admin")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) ViewBag.adsoyad = User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Name).Value;
            return View();
        }

        [Route("/admin/login"), AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [Route("/admin/login"), AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Admin model, string ReturnUrl)
        {
            string password = GeneralTool.getMD5(model.Password);
            Admin admin = adminRepo.GetBy(g => g.EmailAddress == model.EmailAddress && g.Password == password) ?? null;
            if (admin != null)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Email, admin.EmailAddress), new Claim(ClaimTypes.PrimarySid, admin.ID.ToString()), new Claim(ClaimTypes.Name, admin.Name + " " + admin.Surname) }, "Divisima");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties() { IsPersistent = true });


                if (!string.IsNullOrEmpty(ReturnUrl)) return Redirect(ReturnUrl); else return RedirectToAction("Index");
            }
            else
            {
                ViewBag.hata = "Geçersiz Kullanıcı Adı veya Şifre...";
                return View(model);
            }
        }

        [Route("/admin/logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
