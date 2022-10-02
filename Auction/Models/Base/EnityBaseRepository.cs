using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Models.Base
{
    public class EnityBaseRepository<T> : IEntityBaseRepository<T> where T : class
    {
        protected AuctionDbContext _context;
        public EnityBaseRepository(AuctionDbContext context)
        {
            _context = context;
        }
        public async Task AddAsynnc(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            EntityEntry itemToBeDeleted = _context.Entry<T>(entity);
            itemToBeDeleted.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            EntityEntry itemToBeUpdated = _context.Entry<T>(entity);
            itemToBeUpdated.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
