using Application.Repository;
using Application.ViewModels;
using DataBase;
using DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SerieGeneroService
    {
        private readonly SerieGenreRepository _serieGenreRepository;
        public SerieGeneroService(ApplicationContext dbContext)
        {
            _serieGenreRepository = new(dbContext);
        }
        public async Task Add(SerieGenreViewModel vm)
        {
            SerieGenero serieGenero = new();
            serieGenero.Tipo = vm.Tipo;
            serieGenero.SerieId = vm.SerieId;
            serieGenero.GeneroId = vm.GeneroId;

            await _serieGenreRepository.AddAsync(serieGenero);

        }
        public async Task<List<SerieGenreViewModel>> GetAllViewModel()
        {
            var seriegeneroList = await _serieGenreRepository.GetAllAsync();
            return seriegeneroList.Select(seriegenero => new SerieGenreViewModel
            {
                SerieId = seriegenero.SerieId,
                GeneroId = seriegenero.GeneroId,
                Tipo = seriegenero.Tipo

            }).ToList();
        }

    }
}
