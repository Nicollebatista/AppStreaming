using Application.Repository;
using Application.ViewModels;
using DataBase;
using DataBase.Models;


namespace Application.Services
{
    public class SerieService
    {
        private readonly SerieRepository _serieRepository;
        private readonly SerieGenreRepository _serieGenreRepository;
        private readonly SerieGeneroService _serieGeneroService;
        private readonly GenreService _genreService;
        public SerieService(ApplicationContext dbContext)
        {
            _serieGeneroService = new SerieGeneroService(dbContext);
            _serieGenreRepository = new(dbContext);
            _serieRepository = new(dbContext);
            _genreService = new(dbContext);
        }
        public async Task Update(SaveSerieViewModel vm)
        {
            Serie serie = new();
            serie.Id = vm.Id;
            serie.Name = vm.Name;
            serie.Description = vm.Description;
            serie.ImagenPort = vm.ImagenPort;
            serie.VideoYout = vm.VideoYout;
            serie.ProductoraId = vm.ProductoraId;
            serie.ProductoraId = vm.ProductoraId;
            await _serieRepository.UpdateAsync(serie);
        }
        public async Task<int> Add(SaveSerieViewModel vm)
        {
            Serie serie = new();
            serie.Name = vm.Name;
            serie.Description = vm.Description;
            serie.ImagenPort = vm.ImagenPort;
            serie.VideoYout = vm.VideoYout;
            serie.ProductoraId = vm.ProductoraId;
            await _serieRepository.AddAsync(serie);
            return serie.Id;

        }
        public async Task Delete(int id)
        {
            var serie = await _serieRepository.GetByIdAsync(id);
            await _serieRepository.DeleteAsync(serie);

        }
        public async Task<List<SerieViewModel>> GetAllViewModel()
        {
            var SerieList = await _serieRepository.GetAllAsync();
            var viewModelList = new List<SerieViewModel>();
            foreach (var serie in SerieList)
            {
                var productoraName = await GetProducerName(serie.ProductoraId);

                viewModelList.Add(new SerieViewModel
                {
                    Id = serie.Id,
                    Name = serie.Name,
                    Description = serie.Description,
                    ImagenPort = serie.ImagenPort,
                    VideoYout = serie.VideoYout,
                    ProductoraId = serie.ProductoraId,
                    ProductoraName = productoraName,
                    nombresgeneros = await GetGenreNamesAndTypesBySerieIdAsync(serie.Id),
                    Generos = await _serieGeneroService.GetAllViewModel()
                });
            }

            return viewModelList;
        }
        public async Task<List<SerieViewModel>> GetAllViewModelWithFilters(FilterProducerViewModel filters)
        {
            var SerieList = await _serieRepository.GetAllAsync();
            var viewModelList = new List<SerieViewModel>();

            foreach (var serie in SerieList)
            {
                var productoraName = await GetProducerName(serie.ProductoraId);

                viewModelList.Add(new SerieViewModel
                {
                    Id = serie.Id,
                    Name = serie.Name,
                    Description = serie.Description,
                    ImagenPort = serie.ImagenPort,
                    VideoYout = serie.VideoYout,
                    ProductoraId = serie.ProductoraId,
                    ProductoraName = productoraName,
                    nombresgeneros = await GetGenreNamesAndTypesBySerieIdAsync(serie.Id),
                    Generos = await GetGenreIdAndTypesBySerieIdAsync(serie.Id)
                });
            }
            if (filters.ProductoraId != null)
            {
                viewModelList = viewModelList.Where(serie => serie.ProductoraId == filters.ProductoraId.Value).ToList();
            }
            if (filters.GeneroId != null)
            {
                viewModelList = viewModelList.Where(serie => serie.Generos.Any(g => g.GeneroId == filters.GeneroId.Value)).ToList();
            }
            if (filters.SerieName != null)
            {
                viewModelList = viewModelList.Where(serie => serie.Name == filters.SerieName).ToList();
            }
            return viewModelList;
        }

        public async Task<string> GetProducerName(int id)
        {
            var ProducerName = await _serieRepository.GetProducerName(id);
            return ProducerName;
        }
        public async Task<SaveSerieViewModel> GetByIdSaveViewModel(int id)
        {
            var serie = await _serieRepository.GetByIdWithGenresAsync(id);
            
            var viewModel = new SaveSerieViewModel
            {
                Id = serie.Id,
                Name = serie.Name,
                Description = serie.Description,
                ImagenPort = serie.ImagenPort,
                VideoYout = serie.VideoYout,
                ProductoraId = serie.ProductoraId,

               PrimaryGenreId = serie.SerieGeneros.FirstOrDefault(sg => sg.Tipo == "Primario")?.GeneroId,
                SecondaryGenreIds = serie.SerieGeneros.Where(sg => sg.Tipo == "Secundario").Select(sg => sg.GeneroId).ToList(),
            };

            return viewModel;
        }
        public async Task UpdateWithGenresAsync(SaveSerieViewModel vm)
        {
            var existingGenres = await _serieGenreRepository.GetBySerieIdAsync(vm.Id);

            var genresToDelete = existingGenres
                .Where(eg => !vm.SecondaryGenreIds.Contains(eg.GeneroId) && eg.GeneroId != vm.PrimaryGenreId)
                .ToList();

            foreach (var genre in genresToDelete)
            {
                await _serieGenreRepository.DeleteAsync(genre);
            }

            if (vm.PrimaryGenreId.HasValue && !existingGenres.Any(eg => eg.GeneroId == vm.PrimaryGenreId))
            {
                await _serieGeneroService.Add(new SerieGenreViewModel
                {
                    SerieId = vm.Id,
                    GeneroId = vm.PrimaryGenreId.Value,
                    Tipo = "Primario"
                });
            }

                var newGenreIds = vm.SecondaryGenreIds.Except(existingGenres.Select(eg => eg.GeneroId)).ToList();

            foreach (var genreId in newGenreIds)
            {
                await _serieGeneroService.Add(new SerieGenreViewModel
                {
                    SerieId = vm.Id,
                    GeneroId = genreId,
                    Tipo = "Secundario"
                });
            }
        }
        public async Task<List<SaveGenreViewModel>> GetGenreNamesAndTypesBySerieIdAsync(int serieId)
        {
            var serie = await _serieRepository.GetByIdWithGenresAsync(serieId);
            if (serie != null)
            {
                return serie.SerieGeneros.Select(sg => new SaveGenreViewModel
                {
                    Name = sg.Genero.Name,
                    Type = sg.Tipo
                }).ToList();
            }
            return new List<SaveGenreViewModel>();
        }
        public async Task<List<SerieGenreViewModel>> GetGenreIdAndTypesBySerieIdAsync(int serieId)
        {
            var serie = await _serieRepository.GetByIdWithGenresAsync(serieId);
            if (serie != null)
            {
                return serie.SerieGeneros.Select(sg => new SerieGenreViewModel
                {
                    GeneroId = sg.GeneroId,
                    SerieId = sg.SerieId,
                    Tipo = sg.Tipo
                }).ToList();
            }
            return new List<SerieGenreViewModel>();
        }




    }

}
