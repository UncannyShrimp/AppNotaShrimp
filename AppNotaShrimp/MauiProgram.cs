using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AppNotaShrimp.Data;
using AppNotaShrimp.Models;


namespace AppNotaShrimp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Configuración de la base de datos SQLite y la inyección de dependencias para el DataContext
            builder.Services.AddDbContext<Data.DataContext>(options =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "notas.db");
                options.UseSqlite($"Data Source={dbPath}");
            });

#if DEBUG
            builder.Logging.AddDebug();

            var app = builder.Build();
#endif
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.EnsureCreated();

                if (!context.Notas.Any())
                {

                    context.Notas.AddRange
                        (
                       new Notas { Id = 1, Title = "Nota Nº", Content = "Contenido de la nota ", CreatedAt = DateTime.Now, IsFavorite = true }
                     );
                    context.SaveChanges();


                }

            }

            return app;
        }
    }
}
