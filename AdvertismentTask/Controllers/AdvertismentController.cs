using AdvertismentTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult AdvertismentCard(int id)
        {
            Advertisement adv = _db.Advertisements.FirstOrDefault(a => a.Id == id);
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
                // путь к папке Files
                string path = $@"/images/{advViewModel.Image.FileName}";
                string fullPath = _appEnvironment.WebRootPath + path;

                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await advViewModel.Image.CopyToAsync(fileStream);
                }
                Advertisement adv = new Advertisement
                {
                    Title = advViewModel.Title,
                    Text = advViewModel.Text,
                    Image = path,
                    User = _db.Users.FirstOrDefault(x => x.Name == User.Identity!.Name)!
                };
                _db.Advertisements.Add(adv);
                _db.SaveChanges();
            }

            return RedirectToAction("Index","Home");
        }
    }
}
