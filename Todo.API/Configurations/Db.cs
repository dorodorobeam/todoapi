using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Persistence.Context;

namespace Todo.API.Configurations
{
    public static class Db
    {
        internal static void RegisterDB(WebApplicationBuilder builder)
        {
            // DATABASE
            builder.Services.AddDbContext<DatabaseContext>(opts =>
                opts.UseSqlServer(builder.Configuration.GetConnectionString("TodoDb")));

            // IDENTITY
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
            })
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();
        }

        internal static void ConfigureDB(WebApplication app)
        {
            using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();

            // MIGRATIONS
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
        }
    }
}
