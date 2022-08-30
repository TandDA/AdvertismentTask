using AdvertismentTask.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace AdvertismentTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationContext _db;
        private IWebHostEnvironment _appEnvironment;
        public HomeController(ILogger<HomeController> logger, ApplicationContext db, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _db = db;
            _appEnvironment = appEnvironment;
        }
        public IActionResult Index()
        {
            ViewBag.HostPath = _appEnvironment.WebRootPath;
            return View(_db.Advertisements.ToList());
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}