using System.ComponentModel.DataAnnotations;

namespace E_Project_.Models
{
    public class login
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "please enter your email")]
        [RegularExpression("^[a-zA-Z0-9_.±]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        [MinLength(5, ErrorMessage = "Wrong Password!!!")]
        public string Password { get; set; }

        public login(int id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}
