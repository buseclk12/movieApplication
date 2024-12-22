#nullable disable
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// Generated from Custom Template.

namespace movieApplication.Controllers
{
    [Authorize]
    public class MoviesController : MvcController
    {
        // Service injections:
        private readonly IService<Movies, MoviesModel> _movieService;

        public MoviesController(IService<Movies, MoviesModel> movieService)
        {
            _movieService = movieService;
        }

        // GET: Movies - Allow all authenticated users to view the list
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _movieService.Query().ToList();
            return View(list);
        }

        // GET: Movies/Details/5 - Only authenticated users can see details
        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (e.g., Director dropdown):
            ViewBag.DirectorIds = new SelectList(_movieService.Query().Select(m => m.Record.Director), "Id", "Name");
        }

        // GET: Movies/Create - Only Admin can create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(MoviesModel movie)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _movieService.Create(movie.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movie.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movie);
        }

        // GET: Movies/Edit/5 - Only Admin can edit
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            SetViewData();
            return View(item);
        }

        // POST: Movies/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(MoviesModel movie)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _movieService.Update(movie.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movie.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movie);
        }

        // GET: Movies/Delete/5 - Only Admin can delete
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _movieService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
