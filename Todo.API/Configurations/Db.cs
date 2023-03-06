using Microsoft.EntityFrameworkCore;
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
