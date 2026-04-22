using GamesCrudApi.Data;
using GamesCrudApi.Models;
using GamesCrudApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// PostgreSQL + EF Core
var connString = builder.Configuration.GetConnectionString("Postgres");
var useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

if (useInMemoryDatabase || string.IsNullOrWhiteSpace(connString))
{
    builder.Services.AddDbContext<GameDbContext>(options =>
        options.UseInMemoryDatabase("GamesDb"));
}
else
{
    builder.Services.AddDbContext<GameDbContext>(options =>
        options.UseNpgsql(connString));
}

// Dependency injection (Services layer)
builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameDbContext>();

    if (!dbContext.Database.IsInMemory())
    {
        await dbContext.Database.MigrateAsync();
    }

    if (!await dbContext.Games.AnyAsync())
    {
        dbContext.Games.AddRange(
            new Game
            {
                Name = "The Legend of Zelda: Breath of the Wild",
                Genre = "Action-Adventure",
                ReleaseDate = new DateTime(2017, 3, 3),
                Description = "Open-world adventure in Hyrule."
            },
            new Game
            {
                Name = "Minecraft",
                Genre = "Sandbox",
                ReleaseDate = new DateTime(2011, 11, 18),
                Description = "Creative building and survival gameplay."
            },
            new Game
            {
                Name = "Elden Ring",
                Genre = "Action RPG",
                ReleaseDate = new DateTime(2022, 2, 25),
                Description = "Dark fantasy RPG with open-world exploration."
            });

        await dbContext.SaveChangesAsync();
    }
}

// Ensure routing is enabled for middleware that depends on it
app.UseRouting();

// Enable Swagger in all environments (change if you want it only in Development)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Serve the Swagger UI at the app's root (http://<host>/)
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Games API V1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
