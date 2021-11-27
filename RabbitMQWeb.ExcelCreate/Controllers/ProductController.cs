using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RabbitMQWeb.ExcelCreate.Models;
using RabbitMQWeb.ExcelCreate.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQWeb.ExcelCreate.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        public ProductController(AppDbContext context, UserManager<IdentityUser> userManager, RabbitMQPublisher rabbitMQPublisher)
        {
            _context = context;
            _userManager = userManager;
            _rabbitMQPublisher = rabbitMQPublisher;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CreateProductExcel()
        {  
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var fileName = $"product-excel-{Guid.NewGuid().ToString().Substring(1, 10)}";
            UserFile userFile = new UserFile()
            {
                UserId =  user.Id,
                FileName = fileName,
                FileStatus = FileStatus.Creating
            };
            await _context.userFiles.AddAsync(userFile);
            await _context.SaveChangesAsync();
            //RabbitMQ ya mesaj gönder 
            _rabbitMQPublisher.Publish(new Shared.CreateExcelMessage() { FileId = userFile.Id });
            TempData["StartCreatingExcel"] = true; // tempdata requestler arası bilgi aktarımı yapar Viewdata veya viewbag view kısmına yapar
            return RedirectToAction("Files");
        }
        public async Task<IActionResult> Files()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            var list = await _context.userFiles.Where(x => x.UserId == user.Id).OrderByDescending(x=>x.Id).ToListAsync();
            return View(list);
        }
    }
}
