using Auction.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Models.Services
{
    public interface IPurchaseService: IEntityBaseRepository<Purchase>
    {
        public Task<IEnumerable<Purchase>> GetByUserId(int? id);
    }
}
