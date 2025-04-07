
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Project_.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string P_Id { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Writter_Name is required")]
        public string Writter_Name { get; set; }

        [Required(ErrorMessage = "Book_Name is required")]
        public string Book_Name { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        // Foreign key property
        public string CategoryId { get; set; }

        // Navigation property
        public Category Category { get; set; }

        public ICollection<ProductCheckout> ProductCheckout { get; set; }

        // Parameterless constructor
        public Product()
        {
        }

        // Parameterized constructor
        public Product(string p_id, string image, string writter_Name, string book_Name, int stock, int price, string categoryId)
        {
            P_Id = p_id;
            Image = image;
            Writter_Name = writter_Name;
            Book_Name = book_Name;
            Stock = stock;
            Price = price;
            CategoryId = categoryId;
        }

        [NotMapped]
        public int Quantity { get; set; }
    }
}

