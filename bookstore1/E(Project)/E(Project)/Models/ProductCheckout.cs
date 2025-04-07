namespace E_Project_.Models
{
    public class ProductCheckout
    {

        public string Id { get; set; }
        public string First_name { get; set; }

        public string Last_name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string Postal_code { get; set; }

        public string Order_notee { get; set; }

        public ProductCheckout(string id, string first_name, string last_name, string phone, string email, string address, string city, string postal_code, string order_notee)
        {
            Id = id;
            First_name = first_name;
            Last_name = last_name;
            Phone = phone;
            Email = email;
            Address = address;
            City = city;
            Postal_code = postal_code;
            Order_notee = order_notee;
        }
    }
}
