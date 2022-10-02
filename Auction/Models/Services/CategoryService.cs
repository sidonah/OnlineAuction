using Auction.Models.Base;

namespace Auction.Models.Services
{
    public class CategoryService : EnityBaseRepository<Category>, ICategoryService
    {
        public CategoryService(AuctionDbContext context) : base(context)
        {
        }
    }
}
