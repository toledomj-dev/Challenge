using Microsoft.EntityFrameworkCore;
using Challenge.WebApi.Data;

namespace Challenge.WebApi.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Aplica las migraciones pendientes automáticamente
            context.Database.Migrate();
        }
    }
}