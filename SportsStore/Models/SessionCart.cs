﻿using Newtonsoft.Json;
using SportsStore.Infrastructure;

namespace SportsStore.Models
{
  public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
#pragma warning disable SA1201
        public ISession? Session { get; set; }
#pragma warning restore SA1201

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            this.Session?.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            this.Session?.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            this.Session?.Remove("Cart");
        }
    }
}
