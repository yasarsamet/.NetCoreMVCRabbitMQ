using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQWeb.ExcelCreate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQWeb.ExcelCreate
{
    public class Program
    { 
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope()) // program aya�a kalkarken kendimiz migration yapmak yerine kendi otomatik bu kod sayesinde yapar  yani kendimiz olustururuz migrationu
                                                            // ama bu kod sayesinde update-database yapmam�z gerek kalmaz kendi bu kod ile yapar
            {
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                appDbContext.Database.Migrate();
                 if (!appDbContext.Users.Any())
                {
                    userManager.CreateAsync(new IdentityUser { UserName= "deneme",Email="deneme@gmail.com"},"Ysa0037178.").Wait();
                    userManager.CreateAsync(new IdentityUser { UserName = "deneme2", Email = "deneme2@gmail.com" }, "Ysa0037178.").Wait();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
