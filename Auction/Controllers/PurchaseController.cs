using Auction.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    public class PurchaseController : Controller
    {
        IPurchaseService _service;
        IUserService _userService;
        private readonly string CLAIM_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public PurchaseController(IPurchaseService  service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CLAIM_TYPE).Value;
            var id = _userService.GetUserId(email);
            var purchaseHistory = await _service.GetByUserId(id);
            return View(purchaseHistory);
        }
    }
}
