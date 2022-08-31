using AdvertismentTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvertismentTask.Controllers
{
    public class RegistrController : Controller
    {
        private ApplicationContext _db;
        public RegistrController(ApplicationContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Registr(User user)
        {
            if(_db.Users.FirstOrDefault(u => u.Name == user.Name) == null)
            {
                ViewBag.Error = "Пользователь с таким именем уже существует";
                return View();
            }
            _db.Users.Add(user);
            _db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}
