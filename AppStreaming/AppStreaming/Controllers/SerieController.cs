using Application.Services;
using Application.ViewModels;
using DataBase;
using DataBase.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppStreaming.Controllers
{
    public class SerieController : Controller
    {
        private readonly SerieService _serieService;
        private readonly ProducerService _producerService;
        private readonly GenreService _genreService;
        private readonly SerieGeneroService _serieGeneroService;

        public SerieController(ApplicationContext dbcontext)
        {
            _serieService = new SerieService(dbcontext);
            _serieGeneroService = new SerieGeneroService(dbcontext);
            _producerService =  new ProducerService(dbcontext);
            _genreService =  new GenreService(dbcontext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _serieService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new SaveSerieViewModel
            {
                Productoras = await _producerService.GetAllViewModel(),
                Generos = await _genreService.GetAllGenreViewModel()
            };
            return View("SaveSerie", viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveSerieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Productoras = await _producerService.GetAllViewModel();
                vm.Generos = await _genreService.GetAllGenreViewModel();
                return View("SaveSerie", vm);
            }
            vm.Id = await _serieService.Add(vm);

            await _serieGeneroService.Add(new SerieGenreViewModel
            {
                SerieId = vm.Id,
                GeneroId = (int)vm.PrimaryGenreId,
                Tipo = "Primario"
            });

            foreach (var genreId in vm.SecondaryGenreIds)
            {
                await _serieGeneroService.Add(new SerieGenreViewModel
                {
                    SerieId = vm.Id,
                    GeneroId = genreId,
                    Tipo = "Secundario"
                });
            }


            return RedirectToAction("Index", "Serie");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _serieService.GetByIdSaveViewModel(id);
            viewModel.Productoras = await _producerService.GetAllViewModel();
            viewModel.Generos = await _genreService.GetAllGenreViewModel();
            return View("SaveSerie", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveSerieViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Productoras = await _producerService.GetAllViewModel();
                vm.Generos = await _genreService.GetAllGenreViewModel();
                return View("SaveSerie", vm);
            }
            await _serieService.Update(vm);
            await _serieService.UpdateWithGenresAsync(vm);
            return RedirectToAction("Index", "Serie");
        }
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _serieService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _serieService.Delete(id);
            return RedirectToAction("Index", "Serie");
        }

    }
}
