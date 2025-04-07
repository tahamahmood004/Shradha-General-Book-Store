using System.ComponentModel.DataAnnotations;

namespace E_Project_.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }


        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_.±]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        public Contact(int id, string message, string name, string email, string subject)
        {
            Id = id;
            Message = message;
            Name = name;
            Email = email;
            Subject = subject;
        }
    }
}
