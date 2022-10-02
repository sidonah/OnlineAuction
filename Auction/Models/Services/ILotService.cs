using Auction.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Models.Services
{
    public interface ILotService : IEntityBaseRepository<Lot>
    {
        public Task<IEnumerable<Lot>> GetAllActiveAsync(string search);
        public Task<IEnumerable<Lot>> GetMyLotsAsync(int? id);
    }
}
