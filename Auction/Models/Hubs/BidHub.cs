using Auction.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Models.Hubs
{
    public class BidHub: Hub
    {
        ILotService _service;
        IUserService _userService;
        IPurchaseService _purchaseService;
        private readonly string CLAIM_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        private int lotId = 0;
        private int userId = 0;
        public BidHub(ILotService service, IUserService userService, IPurchaseService purchaseService)
        {
            _service = service;
            _userService = userService;
            _purchaseService = purchaseService;
        }
        public async Task SendMessageToAll(string message, string lotId)
        {
            var currentPrice = Convert.ToInt32(message);
            this.lotId  = Convert.ToInt32(lotId);
            var lot = await _service.GetByIdAsync(this.lotId);
            var email = Context.User.Claims.FirstOrDefault(c => c.Type == CLAIM_TYPE).Value;
            userId = _userService.GetUserId(email);
            Purchase purchase = new Purchase
            {
                UserId = userId,
                LotId = this.lotId
            };
            if (lot.Price < currentPrice)
            {
                lot.Price = currentPrice;
                await _service.UpdateAsync(lot);
                var pur = await _purchaseService.GetByUserId(userId);
                if (pur.Count() == 0)
                    await _purchaseService.AddAsynnc(purchase);
                else
                    await _purchaseService.UpdateAsync(purchase);   
            }
            await  Clients.All.SendAsync("RecieveBestBid", message);
        }
    }
}
