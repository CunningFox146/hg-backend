using TheHungerGames.Middleware;

namespace TheHungerGames.Builder;

public static class TokenExtensions
{
    public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string pattern)
    {
        return builder.UseMiddleware<TokenMiddleware>(pattern);
    }
}