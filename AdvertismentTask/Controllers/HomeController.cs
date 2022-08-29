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
        public IActionResult Registr()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registr(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAd(AdvertismentViewModel advViewModel)
        {
            if (advViewModel != null)
            {
                // путь к папке Files
                string path = $@"/images/{advViewModel.Image.FileName}";
                string fullPath = _appEnvironment.WebRootPath + path;

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await advViewModel.Image.CopyToAsync(fileStream);
                }
                Advertisement adv = new Advertisement {
                    Title = advViewModel.Title,
                    Text = advViewModel.Text,
                    Image = path,
                    User = _db.Users.FirstOrDefault(x => x.Name == User.Identity!.Name)!
                };
                _db.Advertisements.Add(adv);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult CreateAd()
        {
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Validate(string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = _db.Users.FirstOrDefault(x => x.Name == username);
            if (user is not null && user.Password == password)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, password));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            TempData["Error"] = "Error. Username or Password is invalid";
            return View("login");
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