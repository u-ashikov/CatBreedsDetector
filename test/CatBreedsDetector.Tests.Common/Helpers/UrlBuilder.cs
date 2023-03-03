namespace CatBreedsDetector.Tests.Common.Helpers;

using System.Text;
using CatBreedsDetector.Common.Extensions;

public static class UrlBuilder
{
    public static string Build(params string[] urlParts)
    {
        if (urlParts.IsNullOrEmpty())
            return null;

        var stringBuilder = new StringBuilder();
        foreach (var urlPart in urlParts)
            stringBuilder.Append($"/{urlPart}");

        return stringBuilder.ToString().Trim();
    }
}