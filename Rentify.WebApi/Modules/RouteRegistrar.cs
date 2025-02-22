namespace Rentify.WebApi.Modules;

public static class RouteRegistrar
{
    public static void RegisterRoutes(this IEndpointRouteBuilder app)
    {
        app.RegisterCategoryRoutes();
        app.RegisterAuthRoutes();
        app.RegisterItemRoutes();
        app.RegisterUserRoutes();
    }
}
