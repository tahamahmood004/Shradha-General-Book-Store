namespace E_Project_.Models
{
    public class CheckoutId
    {
        private static Random random = new Random();

        public static string GenerateCheckoutId()
        {
            int number = random.Next(0, 100000000); // 0 to 99,999,999

            return $"{number}";
        }
    }
}
