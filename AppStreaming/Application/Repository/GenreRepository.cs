using DataBase.Models;
using DataBase;
using Microsoft.EntityFrameworkCore;


namespace Application.Repository
{
    public class GenreRepository
    {
        private readonly ApplicationContext _dbcontext;
        public GenreRepository(ApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task AddAsync(Genre genre)
        {
            await _dbcontext.Genres.AddAsync(genre);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Genre genre)
        {
            _dbcontext.Entry(genre).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Genre genre)
        {
            _dbcontext.Set<Genre>().Remove(genre);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task<List<Genre>> GetAllAsync()
        {
            return await _dbcontext.Set<Genre>().ToListAsync();
        }
        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<Genre>().FindAsync(id);
        }
        public async Task<List<Genre>> GetAllGenreAsync()
        {
            return await _dbcontext.Set<Genre>().ToListAsync();
        }
    }
}
