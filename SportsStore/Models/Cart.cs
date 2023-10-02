namespace SportsStore.Models
{
    public class Cart
    {
#pragma warning disable IDE0090
        private readonly List<CartLine> lines = new List<CartLine>();
#pragma warning restore IDE0090

        public IReadOnlyList<CartLine> Lines
        {
            get { return this.lines; }
        }

        public virtual void AddItem(Product product, int quantity)
        {
#pragma warning disable S2971
            CartLine? line = this.lines.
                Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();
#pragma warning restore S2971

            if (line is null)
            {
                this.lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product)
            => this.lines.RemoveAll(l => l.Product.ProductId == product.ProductId);

        public decimal ComputeTotalValue()
            => this.lines.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => this.lines.Clear();
    }
}
