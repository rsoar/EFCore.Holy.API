namespace EFCore.Holy.Business.Services
{
    public static class GenerateService
    {
        private static readonly Random _random = new Random();
        public static string Str(int length)
        {
            string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789@#$&!";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static int Num(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
