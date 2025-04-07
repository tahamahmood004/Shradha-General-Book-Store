namespace E_Project_.Models
{
    public class CombinedModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public CombinedModel()
        {
            Categories = new List<Category>();
            Products = new List<Product>();
        }
    }
}
