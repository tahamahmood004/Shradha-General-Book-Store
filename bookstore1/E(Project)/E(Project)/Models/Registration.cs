using System.ComponentModel.DataAnnotations;

namespace E_Project_.Models
{
    public class Registration
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter your name")]

        public string Name { get; set; }
        [Required(ErrorMessage = "please enter your email")]
        [RegularExpression("^[a-zA-Z0-9_.±]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [MinLength(5, ErrorMessage = "Wrong Password!!!")]
        public string Password { get; set; }

        public Registration(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
