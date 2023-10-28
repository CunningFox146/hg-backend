namespace TheHungerGames.Middleware;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _token;

    public TokenMiddleware(RequestDelegate next, string token)
    {
        _next = next;
        _token = token;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Query["token"];
        if (token != _token)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Token is invalid");
            return;
        }

        await _next.Invoke(context);
    }
}