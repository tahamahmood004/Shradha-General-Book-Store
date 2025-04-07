namespace E_Project_.Models
{
    public class CategoryId
    {
        private static Random random = new Random();

        public static string GenerateCategoryId()
        {
            // Generate one random letter (A-Z)
            char letter = (char)random.Next('A', 'Z' + 1);

            // Generate one random digit (0-9)
            int digit = random.Next(0, 10); // Generates a number between 0 and 9

            return $"{letter}{digit}"; // Concatenate letter and digit
        }
    }
}
