using AdvertismentTask.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdvertismentTask.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationContext _db;
        public LoginController(ApplicationContext db)
        {
            _db = db;
        }
        public IActionResult Index(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Validate(string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = _db.Users.FirstOrDefault(x => x.Name == username);
            if (user is not null && user.Password == password)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, username));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, password));

                if (username == "Admin")
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                else
                    claims.Add(new Claim(ClaimTypes.Role, "User"));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
                return Redirect(returnUrl);
            }
            TempData["Error"] = "Ошибка. Имя или пароль неправильны";
            return View("login");
        }
    }
}
