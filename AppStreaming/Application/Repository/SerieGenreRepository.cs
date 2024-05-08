using DataBase.Models;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class SerieGenreRepository
    {
        private readonly ApplicationContext _dbcontext;
        public SerieGenreRepository(ApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task AddAsync(SerieGenero seriegenero)
        {
            await _dbcontext.SerieGeneros.AddAsync(seriegenero);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<SerieGenero>> GetBySerieIdAsync(int serieId)
        {
            return await _dbcontext.SerieGeneros.Where(sg => sg.SerieId == serieId).ToListAsync();
        }
        public async Task<List<SerieGenero>> GetAllAsync()
        {
            return await _dbcontext.Set<SerieGenero>().ToListAsync();
        }
        public async Task DeleteAsync(SerieGenero serieGenero)
        {
            _dbcontext.SerieGeneros.Remove(serieGenero);
            await _dbcontext.SaveChangesAsync();
        }


    }
}
