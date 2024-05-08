using Application.Repository;
using Application.ViewModels;
using DataBase.Models;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GenreService
    {
        private readonly GenreRepository _GenreRepository;
        public GenreService(ApplicationContext dbContext)
        {
            _GenreRepository = new(dbContext);
        }
        public async Task Update(GenreViewModel vm)
        {
            Genre genre = new();
            genre.Id = vm.Id;
            genre.Name = vm.Name;

            await _GenreRepository.UpdateAsync(genre);
        }
        public async Task Add(GenreViewModel vm)
        {
            Genre genre = new();
            genre.Name = vm.Name;

            await _GenreRepository.AddAsync(genre);
        }
        public async Task Delete(int id)
        {
            var genre = await _GenreRepository.GetByIdAsync(id);
            await _GenreRepository.DeleteAsync(genre);

        }
      
        public async Task<GenreViewModel> GetByIdSaveViewModel(int id)
        {
            var genre = await _GenreRepository.GetByIdAsync(id);
            GenreViewModel vm = new();
            vm.Id = genre.Id;
            vm.Name = genre.Name;

            return vm;
        }
        public async Task<List<GenreViewModel>> GetAllGenreViewModel()
        {
            var GenreList = await _GenreRepository.GetAllGenreAsync();
            return GenreList.Select(genero => new GenreViewModel
            {
                Id = genero.Id,
                Name = genero.Name,

            }).ToList();
        }
    }
}
