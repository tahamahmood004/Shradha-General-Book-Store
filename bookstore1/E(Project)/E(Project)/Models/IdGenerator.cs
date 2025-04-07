namespace E_Project_.Models
{
    public class IdGenerator
    {
        private static Random random = new Random();

        public static string GenerateAlphanumericId()
        {
            // Generate two random letters
            var letters = new string(Enumerable.Range(0, 2)
                .Select(_ => (char)random.Next('A', 'Z' + 1))
                .ToArray());

            // Generate three random digits
            var numbers = random.Next(100, 1000); // Generates a number between 100 and 999

            return $"{letters}{numbers}";
        }
    }
}
