using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MPA201_Simulation.Contexts;
using MPA201_Simulation.Models;
using MPA201_Simulation.ViewModels.ProductViewModels;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MPA201_Simulation.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Select(product => new ProductGetVM()
            {

                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                CategoryName = product.Category.Name,
                ImagePath = product.ImagePath,
                Price = product.Price,
                Rating = product.Rating

            }).ToListAsync();
            return View(products);
        }

    }
}
