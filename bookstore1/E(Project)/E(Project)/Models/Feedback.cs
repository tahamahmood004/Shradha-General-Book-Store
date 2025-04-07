namespace E_Project_.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone_number { get; set; }

        public string Review { get; set; }

        public Feedback(int id, string name, string email, string phone_number, string review)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone_number = phone_number;
            Review = review;
        }
    }
}
