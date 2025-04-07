using System.ComponentModel.DataAnnotations;

namespace E_Project_.Models
{
    public class Category
    {
       public string Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }

        public Category(string id, string name, string image)
        {
            Id = id;
            Name = name;
            Image = image;
        }
    }
}
