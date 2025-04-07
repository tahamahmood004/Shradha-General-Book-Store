namespace E_Project_.Models
{
    public class CartItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }

        public CartItem(string productId, int quantity, Product product)
        {
            ProductId = productId;
            Quantity = quantity;
            Product = product;
        }
    }
}
