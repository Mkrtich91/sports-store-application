using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.Models.Repository;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IStoreRepository repository;

        public CartController(IStoreRepository repository, Cart cart)
        {
            this.repository = repository;
            this.Cart = cart;
        }

        public Cart Cart { get; set; }

        [HttpGet]
#pragma warning disable S4136
        public IActionResult Index(string returnUrl)
#pragma warning restore S4136
        {
            return this.View(new CartViewModel
            {
                ReturnUrl = returnUrl ?? "/",
                Cart = this.Cart,
            });
        }

        [HttpPost]
        [Route("Cart/Remove")]
        public IActionResult Remove(long productId, string returnUrl)
        {
            this.Cart.RemoveLine(this.Cart.Lines.First(cl => cl.Product.ProductId == productId).Product);
#pragma warning disable SA1413
            return this.View("Index", new CartViewModel
            {
                Cart = this.Cart,
                ReturnUrl = returnUrl ?? "/"
            });
#pragma warning restore SA1413
        }

        [HttpPost]
        public IActionResult Index(long productId, string returnUrl)
        {
            Product? product = this.repository.Products.FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                this.Cart.AddItem(product, 1);

                return this.View(new CartViewModel
                {
                    Cart = this.Cart,
                    ReturnUrl = returnUrl,
                });
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
