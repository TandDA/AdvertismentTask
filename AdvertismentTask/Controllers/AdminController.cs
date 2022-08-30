using Microsoft.AspNetCore.Mvc;

namespace AdvertismentTask.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult CheckAdvertisment()
        {
            return View();
        }
    }
}
