namespace CatBreedsDetector.Tests.Common.Helpers
{
    using System;
    using System.Linq;

    public static class TestsHelpers
    {
        public static string GenerateRandomString(int length = 20)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
