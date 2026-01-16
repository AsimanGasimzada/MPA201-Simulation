using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MPA201_Simulation.Contexts;
using MPA201_Simulation.Helpers;
using MPA201_Simulation.Models;
using System.Threading.Tasks;

namespace MPA201_Simulation
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });


            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {

            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<DbContextInitalizer>();

            var app = builder.Build();

            var scope = app.Services.CreateScope();

            var initalizer = scope.ServiceProvider.GetRequiredService<DbContextInitalizer>();


            await initalizer.InitDatabaseAsync();


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
