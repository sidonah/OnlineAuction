using Auction.Models.Base;

namespace Auction.Models.Services
{
    public interface IUserService : IEntityBaseRepository<User>
    {
        public int GetUserId(string email);
        public bool UserAleardyExist(string email);
    }
}
