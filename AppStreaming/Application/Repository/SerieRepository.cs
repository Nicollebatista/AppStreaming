using Application.ViewModels;
using DataBase;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class SerieRepository
    {
        private readonly ApplicationContext _dbcontext;
        public SerieRepository(ApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task AddAsync(Serie serie)
        {
            await _dbcontext.Series.AddAsync(serie);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Serie serie)
        {
            _dbcontext.Entry(serie).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Serie serie)
        {
             _dbcontext.Set<Serie>().Remove(serie);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<Serie>> GetAllAsync()
        {
           return await _dbcontext.Set<Serie>().ToListAsync();
        }
        public async Task<Serie> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<Serie>().FindAsync(id);
        }
        public async Task<string> GetProducerName(int id)
        {
            var producer = await _dbcontext.Set<Producer>().FindAsync(id);
            return producer.Name;
        }

        public async Task<Serie> GetByIdWithGenresAsync(int id)
        {
            return await _dbcontext.Series
                .Include(s => s.SerieGeneros)
                .ThenInclude(sg => sg.Genero)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task<List<SerieGenero>> GetBySerieIdAsync(int serieId)
        {
            return await _dbcontext.SerieGeneros.Where(sg => sg.SerieId == serieId).ToListAsync();
        }

      
    }
}
