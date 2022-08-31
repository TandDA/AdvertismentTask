using AdvertismentTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AdvertismentTask.Controllers
{
    public class AdvertismentController : Controller
    {
        private ApplicationContext _db;
        private IWebHostEnvironment _appEnvironment;
        public AdvertismentController(ApplicationContext db, IWebHostEnvironment appEnvironment)
        {
            _db = db;
            _appEnvironment = appEnvironment;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delate(int id)
        {
            Advertisement adv = _db.Advertisements.FirstOrDefault(a => a.Id == id)!;
            if (adv != null)
            {
                _db.Advertisements.Remove(adv);
                _db.SaveChanges();
            }
            return RedirectToAction("Index","Home");

        }

        [Authorize(Roles ="Admin")]
        public IActionResult ChangeAvailable(int id)
		{
            Advertisement adv = _db.Advertisements.FirstOrDefault(a => a.Id == id)!;
            adv.IsAvailable = !adv.IsAvailable;
            _db.SaveChanges();
            return RedirectToAction("AdvertismentCard", new { id = id });
		}
        [Authorize]
        public IActionResult AdvertismentCard(int id)
        {
            Advertisement adv = _db.Advertisements.Include(u => u.User).FirstOrDefault(a => a.Id == id)!;
            if (!adv.IsAvailable)
                if(User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)!.Value == "User") // Если User пытается обратится к закрытому объявлению, то не даем доступ
                    return RedirectToAction("Denied","Home");

            ViewBag.Role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
            ViewBag.Name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)!.Value;
            if (adv == null) return NotFound();
            return View(adv);
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAd(AdvertismentViewModel advViewModel)
        {
            if (advViewModel != null)
            {
                string path;
                if (advViewModel.Image != null)
                    path = $@"/images/{advViewModel.Image.FileName}";
                else
                    path = @"/images/standartImage.jpg"; // устанавливаем стандартную картинку
                string fullPath = _appEnvironment.WebRootPath + path;

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
                if (advViewModel.Image != null)
                {
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await advViewModel.Image.CopyToAsync(fileStream);
                    }
                }

                Advertisement adv = new Advertisement
                {
                    Title = advViewModel.Title,
                    Text = advViewModel.Text,
                    Image = path,
                    CreationDate = DateTime.Today,
                    User = _db.Users.FirstOrDefault(x => x.Name == User.Identity!.Name)!
                };
                _db.Advertisements.Add(adv);
                _db.SaveChanges();
            }

            return RedirectToAction("Index","Home");
        }
    }
}
