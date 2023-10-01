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

        public ViewResult Index(int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                Products = repository.Products
                               .OrderBy(p => p.ProductId)
                               .Skip((productPage - 1) * PageSize)
                               .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count(),
                },
            });
        }

    }
}
