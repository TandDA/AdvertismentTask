using AdvertismentTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvertismentTask.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationContext _db;
        public AdminController(ApplicationContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> CheckAdvertisment(SortState sortOrder = SortState.TitleAsc)
        {
            IQueryable<Advertisement> users = _db.Advertisements.AsQueryable();

            ViewData["TitleSort"] = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            ViewData["TextSort"] = sortOrder == SortState.TextAsc ? SortState.TextDesc : SortState.TextAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;

            users = sortOrder switch
            {
                SortState.TitleDesc => users.OrderByDescending(s => s.Title),
                SortState.TextAsc => users.OrderBy(s => s.Text.Length),
                SortState.TextDesc => users.OrderByDescending(s => s.Text.Length),
                SortState.DateAsc => users.OrderBy(s => s.CreationDate),
                SortState.DateDesc => users.OrderByDescending(s => s.CreationDate),
                _ => users.OrderBy(s => s.Title),
            };
            return View(await users.AsNoTracking().ToListAsync());
        }
    }
}
