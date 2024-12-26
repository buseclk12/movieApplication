#nullable disable
using BLL.Controllers.Bases;
using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Controllers
{
    [Authorize]
    public class DirectorsController : MvcController
    {
        // Service injections:
        private readonly IService<Directors, DirectorsModel> _directorsService;

        public DirectorsController(IService<Directors, DirectorsModel> directorsService)
        {
            _directorsService = directorsService;
        }
        protected void SetViewData()
        {
            ViewData["DirectorID"] = new SelectList(_directorsService.Query().ToList(), "Record.Id", "Name");

            ViewBag.StoreIds = new MultiSelectList(_directorsService.Query().ToList(), "Record.Id", "Name");
        }
        [AllowAnonymous]
        // GET: Directors
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _directorsService.Query().ToList();
            return View(list);
        }
        [Authorize(Roles = "Admin,User")]
        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _directorsService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [Authorize(Roles = "Admin")]
        // GET: Directors/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectorsModel director)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _directorsService.Create(director.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = director.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(director);
        }
        [Authorize(Roles = "Admin")]
        // GET: Directors/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _directorsService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [Authorize(Roles = "Admin")]
        // POST: Directors/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DirectorsModel director)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _directorsService.Update(director.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = director.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(director);
        }
        [Authorize(Roles = "Admin")]
        // GET: Directors/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _directorsService.Query().SingleOrDefault(q => q.Record.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [Authorize(Roles = "Admin")]
        // POST: Directors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _directorsService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
