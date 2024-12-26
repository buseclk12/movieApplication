#nullable disable
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PageModel = BLL.Models.PageModel;

// Generated from Custom Template.

namespace movieApplication.Controllers
{
    [Authorize]
    public class MoviesController : MvcController
    {
        // Service injections:
        private readonly IService<Movies, MoviesModel> _movieService;
        private readonly IService<Directors, DirectorsModel> _directorsService;

        public MoviesController(IService<Movies, MoviesModel> movieService, IService<Directors, DirectorsModel> directorsService)
        {
            _movieService = movieService;
            _directorsService = directorsService;

        }

        protected void SetViewData(int? currentDirectorId = null)
        {
            // Retrieve all directors and their names
            var directors = _directorsService.Query().Select(d => new 
            { 
                d.Record.Id, 
                FullName = (d.Record.Name ?? "Unknown") + " " + (d.Record.Surname ?? "Unknown")
            }).ToList();

            // Ensure the current director is included in the dropdown if it is not already present
            if (currentDirectorId.HasValue && !directors.Any(d => d.Id == currentDirectorId.Value))
            {
                var currentDirector = _directorsService.Query()
                    .Where(d => d.Record.Id == currentDirectorId.Value)
                    .Select(d => new 
                    { 
                        d.Record.Id, 
                        FullName = (d.Record.Name ?? "Unknown") + " " + (d.Record.Surname ?? "Unknown") 
                    })
                    .SingleOrDefault();

                if (currentDirector != null)
                {
                    directors.Add(currentDirector);
                }
            }

            // Set the ViewBag with the list of directors
            ViewBag.DirectorIds = new SelectList(directors, "Id", "FullName", currentDirectorId);
        }

        // GET: Movies - Allow all authenticated users to view the list
        [AllowAnonymous]
        public IActionResult Index(PageModel pageModel)
        {
            // Get collection service logic:
            var list = _movieService.Query().ToList();
            ViewBag.PageModel = pageModel;
            return View(list);
        }

        // GET: Movies/Details/5 - Only authenticated users can see details
        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            var item = _movieService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
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
            // Retrieve the movie to edit
            var item = _movieService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            // Pass the current director's ID to ensure it is included in the dropdown
            SetViewData(item.Record.DirectorId);

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
