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
            using (var db = app.Services.CreateScope())
            {
                var context = db.ServiceProvider.GetRequiredService<DataContext>();
                context.Database.EnsureCreated();

                if (!context.Notas.Any())
                {
                    for (global::System.Int32 i = 0; i < 10; i++)
                    {
                        context.Notas.AddRange(
                         new Notas { Id = i, Title = "Nota Nº"+i, Content = "Contenido de la nota "+i, CreatedAt = DateTime.Now, IsFavorite = (i % 2 == 0) }
                     );
                    }
                    context.SaveChanges();
                }
 
            }

            return app;
        }
    }
}
