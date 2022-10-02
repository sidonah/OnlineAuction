using Auction.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Models.Services
{
    public class PurchaseService : EnityBaseRepository<Purchase>, IPurchaseService
    {
        public PurchaseService(AuctionDbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Purchase>> GetByUserId(int? id)
        {
            return await _context.purchases.Where(temp => temp.UserId == id)
                                            .Include(Lot => Lot.Lot)
                                            .Include(category => category.Lot.Category)
                                            .Include(user => user.User)
                                            .ToListAsync();
        }
    }
}
