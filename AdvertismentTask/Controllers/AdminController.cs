﻿using AdvertismentTask.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckAdvertisment(int page = 1, SortState sortOrder = SortState.AvailableAsc)
        {
            int pageSize = 18;

            IQueryable<Advertisement> users = _db.Advertisements.AsQueryable();

            ViewData["TitleSort"] = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            ViewData["TextSort"] = sortOrder == SortState.TextAsc ? SortState.TextDesc : SortState.TextAsc;
            ViewData["DateSort"] = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            ViewData["AvailableSort"] = sortOrder == SortState.AvailableAsc ? SortState.AvailableDesc : SortState.AvailableAsc;

            users = sortOrder switch
            {
                SortState.TitleAsc => users.OrderBy(s => s.Title),
                SortState.TitleDesc => users.OrderByDescending(s => s.Title),
                SortState.TextAsc => users.OrderBy(s => s.Text!.Length),
                SortState.TextDesc => users.OrderByDescending(s => s.Text!.Length),
                SortState.AvailableDesc => users.OrderByDescending(s => s.IsAvailable),
                SortState.DateAsc => users.OrderBy(s => s.CreationDate),
                SortState.DateDesc => users.OrderByDescending(s => s.CreationDate),
                _ => users.OrderBy(s => s.IsAvailable),
            };

            var count = await users.CountAsync();
            var items = await users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            IndexViewModel viewModel = new IndexViewModel(
                items,
                new PageViewModel(count, page, pageSize),
                new SortViewModel(sortOrder)
            );
            return View(viewModel);
        }
    }
}
