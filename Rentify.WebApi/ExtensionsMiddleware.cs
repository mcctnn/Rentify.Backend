using Microsoft.AspNetCore.Identity;
using Rentify.Domain.Users;

namespace Rentify.WebApi;

public static class ExtensionsMiddleware
{
    public static void CreateFirstUser(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                User user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    FirstName = "Mehmet Can",
                    LastName = "Çetin",
                    EmailConfirmed = true,
                    Location="Ornek sehir"
                };

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }
}
