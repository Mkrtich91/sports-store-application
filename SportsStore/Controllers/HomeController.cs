using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Repository;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository repository;

        public HomeController(IStoreRepository repository)
        {
            this.repository = repository;
        }

#pragma warning disable CA1051
#pragma warning disable SA1201
#pragma warning disable SA1401
#pragma warning disable S1104
        public int PageSize = 4;
#pragma warning restore S1104
#pragma warning restore SA1401
#pragma warning restore SA1201
#pragma warning restore CA1051

        public ViewResult Index(string? category, int productPage = 1)
     => this.View(new ProductsListViewModel
     {
         Products = this.repository.Products
         .Where(p => category == null || p.Category == category)
         .OrderBy(p => p.ProductId)
         .Skip((productPage - 1) * this.PageSize)
         .Take(this.PageSize),
         PagingInfo = new PagingInfo
         {
             CurrentPage = productPage,
             ItemsPerPage = this.PageSize,
             TotalItems = category == null ? this.repository.Products.Count() : this.repository.Products.Where(e => e.Category == category).Count(),
         },
 
         CurrentCategory = category,
     });
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

    }
}
