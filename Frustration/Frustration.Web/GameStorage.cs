namespace Frustration.Web;

public sealed class GameStorage
{
    private const string GameKey = "FrustrationGame";

    public void StoreGame(Game game, IResponseCookies cookies)
    {
        var json = JsonSerializer.Serialize(game);

        var bytes = Encoding.UTF8.GetBytes(json);
        var base64 = Convert.ToBase64String(bytes);

        cookies.Append(GameKey, base64, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(2),
            HttpOnly = true,
            Secure = true
        });
    }

    public Game? LoadGame(HttpRequest request)
    {
        var cookie = request.Cookies[GameKey];

        if (cookie == null)
            return null;

        var bytes = Convert.FromBase64String(cookie);

        var json = Encoding.UTF8.GetString(bytes);

        var game = JsonSerializer.Deserialize<Game>(json);

        return game;
    }
}
