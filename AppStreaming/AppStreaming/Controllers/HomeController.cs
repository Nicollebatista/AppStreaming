using Application.Services;
using Application.ViewModels;
using AppStreaming.Models;
using DataBase;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppStreaming.Controllers
{
    public class HomeController : Controller
    {
        private readonly SerieService _serieService;
        private readonly ProducerService _producerService;
        private readonly GenreService _genreService;

        public HomeController(ApplicationContext dbcontext)
        {
            _serieService = new(dbcontext);
            _producerService = new(dbcontext);
            _genreService = new(dbcontext);
        }

        public async Task<IActionResult> Index(FilterProducerViewModel id)
        {
            ViewBag.Productora = await _producerService.GetAllViewModel();
            ViewBag.Generos = await _genreService.GetAllGenreViewModel();
            return View(await _serieService.GetAllViewModelWithFilters(id));
        }
        public async Task<IActionResult> GetProducerName(int id)
        {
            var producerName = await _serieService.GetProducerName(id);
            return Content(producerName);
        }

        public async Task<IActionResult> Detalle(int id)
        {
            return View(await _serieService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DetallePost(int id)
        {
            return RedirectToAction("Index", "Serie");
        }


    }
}