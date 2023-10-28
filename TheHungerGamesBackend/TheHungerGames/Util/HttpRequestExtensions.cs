using System.Text;

namespace TheHungerGames.Util;

public static class HttpRequestExtensions
{
    public static async Task<string> ReadBodyAsync(this HttpRequest request)
    {
        using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return body;
    }
}