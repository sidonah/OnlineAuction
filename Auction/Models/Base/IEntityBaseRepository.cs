using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Models.Base
{
    public interface IEntityBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task AddAsynnc(T entity);
        Task DeleteAsync(int? id);
        Task UpdateAsync(T entity);
    }
}
