using System.ComponentModel.DataAnnotations;

namespace E_Project_.Models
{
    public class Payment_method
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Card_name is required")]
        public string Card_name { get; set; }

        [Required(ErrorMessage = " Card_number is required")]
        public string Card_number { get; set; }

        [Required(ErrorMessage = " Exp month is required")]
        public string Exp_month { get; set; }

        [Required(ErrorMessage = " Exp year is required")]
        public string Exp_year { get; set; }

        [Required(ErrorMessage = " Cvv is required")]
        public string Cvv { get; set; }

        public Payment_method(int id, string card_name, string card_number, string exp_month, string exp_year, string cvv)
        {
            Id = id;
            Card_name = card_name;
            Card_number = card_number;
            Exp_month = exp_month;
            Exp_year = exp_year;
            Cvv = cvv;
        }
    }
}
