namespace Frustration.Web;

internal static class DI
{
    public static void SetupDependencies(this IServiceCollection services)
    {
        services.AddTransient<IGameService, GameService>();
        services.AddSingleton<GameStorage>();
    }
}
