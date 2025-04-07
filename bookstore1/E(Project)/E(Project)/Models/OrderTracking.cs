namespace E_Project_.Models
{
    public class OrderTracking
    {
        public string OrderId { get; set; }
        public string Email { get; set; }

        public OrderTracking(string orderId, string email)
        {
            OrderId = orderId;
            Email = email;
        }
    }
}
