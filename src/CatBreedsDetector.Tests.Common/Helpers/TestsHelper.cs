namespace CatBreedsDetector.Tests.Common.Helpers
{
    using System;
    using System.Linq;

    public static class TestsHelper
    {
        /// <summary>
        /// Use this method to generate a random string.
        /// </summary>
        /// <param name="length">The desired length of the randomly generated string. By default it will be with length equal to 20 symbols.</param>
        /// <returns>The randomly generated string.</returns>
        public static string GenerateRandomString(int length = 20)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Use this method to generate a random integer.
        /// </summary>
        /// <param name="lowerBound">The lower bound that should be used during the process.</param>
        /// <param name="upperBound">The upper bound that should be used during the process.</param>
        /// <returns>The randomly generated integer.</returns>
        public static int GenerateRandomInteger(int lowerBound = 1, int upperBound = 10)
        {
            var random = new Random();

            return random.Next(lowerBound, upperBound);
        }
    }
}
