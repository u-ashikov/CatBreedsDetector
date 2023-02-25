namespace CatBreedsDetector.Tests.Common.Helpers
{
    using System.IO;
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

        /// <summary>
        /// Use this method to generate random probability between 0 and 1.
        /// </summary>
        /// <returns>The randomly generated probability.</returns>
        public static double GenerateRandomProbability()
        {
            var random = new Random();

            return random.NextDouble();
        }
        
        /// <summary>
        /// Use this method to create a file in a directory.
        /// </summary>
        /// <param name="filePath">The path of the file.</param>
        /// <param name="fileName">The name of the file that should be created.</param>
        public static void CreateFileInDirectory(string filePath, string fileName)
        {
            if (Directory.Exists(filePath) == false)
                Directory.CreateDirectory(filePath);

            FileStream fileStream = null;

            try
            {
                fileStream = File.Create(Path.Combine(fileName));
            }
            finally
            {
                fileStream?.Dispose();
            }
        }

        /// <summary>
        /// Use this method to generate a string of whitespace characters.
        /// </summary>
        /// <param name="charactersCount">The count of whitespace characters to be generated.</param>
        /// <returns>The generated string.</returns>
        public static string GenerateWhiteSpaces(int charactersCount = 1)
        {
            if (charactersCount <= 0)
                return new string(' ', 1);

            return new string(' ', charactersCount);
        }
    }
}
