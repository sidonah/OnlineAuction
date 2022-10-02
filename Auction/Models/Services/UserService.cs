using Auction.Models.Base;
using System.Linq;

namespace Auction.Models.Services
{
    public class UserService : EnityBaseRepository<User>, IUserService
    {
        public UserService(AuctionDbContext context) : base(context)
        {

        }

        public int GetUserId(string email)
        {
            var user = _context.Users.FirstOrDefault(temp => temp.Email == email);
            return user.Id;
        }
        public bool UserAleardyExist(string email)
        {
            var user = _context.Users.FirstOrDefault(temp => temp.Email == email);
            return user != null;
        }
    }
}
