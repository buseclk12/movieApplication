#nullable disable
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace movieApplication.Controllers
{
    [Authorize]
    public class GenresController : MvcController
    {
        // Service injection:
        private readonly IService<Genres, GenresModel> _genresService;

        public GenresController(IService<Genres, GenresModel> genresService)
        {
            _genresService = genresService;
        }

        [AllowAnonymous]
        // GET: Genres
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _genresService.Query().ToList();
            return View(list);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: Genres/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _genresService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [Authorize(Roles = "Admin")]
        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Genres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GenresModel genre)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _genresService.Create(genre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = genre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(genre);
        }
        [Authorize(Roles = "Admin")]
        // GET: Genres/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _genresService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [Authorize(Roles = "Admin")]
        // POST: Genres/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GenresModel genre)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _genresService.Update(genre.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = genre.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(genre);
        }
        [Authorize(Roles = "Admin")]
        // GET: Genres/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _genresService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [Authorize(Roles = "Admin")]
        // POST: Genres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _genresService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
