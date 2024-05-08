using Application.Services;
using Application.ViewModels;
using DataBase;
using Microsoft.AspNetCore.Mvc;

namespace AppStreaming.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ProducerService _ProducerService;

        public ProducerController(ApplicationContext dbcontext)
        {
            _ProducerService = new(dbcontext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _ProducerService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            return View("SaveProducer", new ProducerViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProducerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProducer", vm);
            }
            await _ProducerService.Add(vm);
            return RedirectToAction("Index", "Producer");
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View("SaveProducer", await _ProducerService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProducerViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveProducer", vm);
            }
            await _ProducerService.Update(vm);
            return RedirectToAction("Index", "Producer");
        }
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _ProducerService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _ProducerService.Delete(id);
            return RedirectToAction("Index", "Producer");
        }
    }
}
