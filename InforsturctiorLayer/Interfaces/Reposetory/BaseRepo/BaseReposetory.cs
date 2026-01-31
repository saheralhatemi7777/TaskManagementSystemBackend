using ApplicationLayer.Interfaces.Reposetory.BaseRepo;
using InforsturctiorLayer.DbContextFolder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InforsturctiorLayer.Interfaces.Reposetory.BaseRepo
{
    public class BaseReposetory<T>:IBaseReposetory<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        protected readonly DbSet<T> _DbSet;
        public BaseReposetory(ApplicationDbContext context)
        {
            _context = context;
            _DbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _DbSet.FindAsync(id);
        }


        public async Task<T> AddAsync(T item)
        {
                await _DbSet.AddAsync(item);
            return item;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
             _DbSet.Update(entity); 
            return true;
        }

        public async Task<bool> DeleteAsync(T id)
        {
             _DbSet.Remove(id);
            return true;
        }

        public async Task<int>SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
