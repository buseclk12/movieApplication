#nullable disable
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace movieApplication.Controllers
{
    [Authorize]
    public class MovieGenresController : MvcController
    {
        // Service injection:
        private readonly IService<MovieGenres, MovieGenresModel> _movieGenresService;
        private readonly IService<Genres, GenresModel> _genresService;
        private readonly IService<Movies, MoviesModel> _moviesService;

        public MovieGenresController(
            IService<MovieGenres, MovieGenresModel> movieGenresService,
            IService<Genres, GenresModel> genresService,
            IService<Movies, MoviesModel> moviesService)
        {
            _movieGenresService = movieGenresService;
            _genresService = genresService;
            _moviesService = moviesService;
        }

        
        // GET: MovieGenres
        [AllowAnonymous]
        public IActionResult Index()
        {
            var list = _movieGenresService.Query().ToList();
            return View(list);
        }

        // GET: MovieGenres/Details/5
        [Authorize(Roles = "Admin,User")]
        public IActionResult Details(int id)
        {
            var item = _movieGenresService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        protected void SetViewData()
        {
            ViewData["GenreId"] = new SelectList(_genresService.Query().ToList(), "Record.Id", "Record.Name");
            ViewData["MovieId"] = new SelectList(_moviesService.Query().ToList(), "Record.Id", "Record.Name");
        }

        // GET: MovieGenres/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: MovieGenres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(MovieGenresModel movieGenres)
        {
            if (ModelState.IsValid)
            {
                var result = _movieGenresService.Create(movieGenres.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movieGenres);
        }

        // GET: MovieGenres/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var item = _movieGenresService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            SetViewData();
            return View(item);
        }

        // POST: MovieGenres/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(MovieGenresModel movieGenres)
        {
            if (ModelState.IsValid)
            {
                var result = _movieGenresService.Update(movieGenres.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movieGenres);
        }

        // GET: MovieGenres/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var item = _movieGenresService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: MovieGenres/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = _movieGenresService.Delete(id);
            if (result.IsSuccessful)
            {
                TempData["Message"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", result.Message);
            return RedirectToAction(nameof(Index));
        }
    }
}
