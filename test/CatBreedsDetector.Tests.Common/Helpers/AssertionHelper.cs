using System.Net;

namespace CatBreedsDetector.Tests.Common.Helpers;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public static class AssertionHelper
{
    public static void AssertSuccessStatusCode(this HttpResponseMessage response)
    {
        Assert.NotNull(response);
        response.EnsureSuccessStatusCode();
    }

    public static async Task<TResult> AssertSuccessStatusCodeAndResult<TResult>(this HttpResponseMessage response)
    {
        response.AssertSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<TResult>();
        Assert.NotNull(result);

        return result;
    }

    public static void AssertUnsuccessfulStatusCode(this HttpResponseMessage response)
    {
        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}