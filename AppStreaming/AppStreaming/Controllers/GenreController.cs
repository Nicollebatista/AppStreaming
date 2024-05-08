using Application.Services;
using Application.ViewModels;
using DataBase;
using Microsoft.AspNetCore.Mvc;

namespace AppStreaming.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenreService _GenreService;

        public GenreController(ApplicationContext dbcontext)
        {
            _GenreService = new(dbcontext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _GenreService.GetAllGenreViewModel());
        }

        public IActionResult Create()
        {
            return View("SaveGenre", new GenreViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(GenreViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenre", vm);
            }
            await _GenreService.Add(vm);
            return RedirectToAction("Index", "Genre");
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveGenre", await _GenreService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GenreViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveGenre", vm);
            }
            await _GenreService.Update(vm);
            return RedirectToAction("Index", "Genre");
        }
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _GenreService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _GenreService.Delete(id);
            return RedirectToAction("Index", "Genre");
        }
    }
}
