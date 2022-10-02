using Auction.Models;
using Auction.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Authorize]
    public class LotController : Controller
    {
        IWebHostEnvironment _webHostEnviroment;
        ILotService _service;
        IUserService _userService;
        ICategoryService _categoryService;
        private readonly string CLAIM_TYPE = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public LotController(ICategoryService categoryService, IWebHostEnvironment webHostEnviroment, ILotService service, IUserService userService) 
        {
            _webHostEnviroment = webHostEnviroment;
            _service = service;
            _userService = userService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CLAIM_TYPE).Value;
            var id = _userService.GetUserId(email);
            var lots = await _service.GetMyLotsAsync(id);
            return View(lots);
        }
        public async Task<IActionResult> DetailOfMyLot(int? id)
        {
            var lot = await _service.GetByIdAsync(id);
            return View(lot);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            var lot = await _service.GetByIdAsync(id);
            
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CLAIM_TYPE).Value;
            var userId = _userService.GetUserId(email);
            ViewBag.show = 1;
            if (lot!=null&&userId == lot.UserId)
                ViewBag.show = 0;
            return View(lot);
        }
        
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _categoryService.GetAllAsync();
            return await Task.FromResult(View());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Lot lot)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == CLAIM_TYPE).Value;
            if (lot.Picture != null)
            {
                string folder = "Images/" + Guid.NewGuid().ToString() + lot.Picture.FileName;//defining the name of the file uniquely
                string serverFolder = Path.Combine(_webHostEnviroment.WebRootPath, folder);//path of the project with the folder

                await lot.Picture.CopyToAsync(new FileStream(serverFolder, FileMode.Create));//copying file into the wwwroot folder

                lot.PictureUrl = folder;
            }
            lot.UserId = _userService.GetUserId(email);
            await _service.AddAsynnc(lot);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> update(int? id)
        {
            var lot = await _service.GetByIdAsync(id);
            lot.AvailabilityStatus = 1;
            await _service.UpdateAsync(lot);

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var lot = await _service.GetByIdAsync(id);

            var pathofImageToDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", lot.PictureUrl)
                                          .Replace('/', '\\');

            await _service.DeleteAsync(id);
            //System.IO.File.Delete(pathofImageToDelete);
            return RedirectToAction("index");
        }
    }
}
