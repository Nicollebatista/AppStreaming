using DataBase.Models;
using DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class ProducerRepository
    {
        private readonly ApplicationContext _dbcontext;
        public ProducerRepository(ApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task AddAsync(Producer producer)
        {
            await _dbcontext.Producers.AddAsync(producer);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Producer producer)
        {
            _dbcontext.Entry(producer).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Producer producer)
        {
            _dbcontext.Set<Producer>().Remove(producer);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<Producer>> GetAllAsync()
        {
            return await _dbcontext.Set<Producer>().ToListAsync();
        }
        public async Task<Producer> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<Producer>().FindAsync(id);
        }
      
    }
}
