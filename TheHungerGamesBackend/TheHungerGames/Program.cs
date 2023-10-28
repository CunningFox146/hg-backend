using Microsoft.EntityFrameworkCore;
using TheHungerGames.Builder;
using TheHungerGames.Data;
using TheHungerGames.Systems.PlayerItems;
using TheHungerGames.Systems.PlayerRating;
using TheHungerGames.Systems.Rollback;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Logging.AddConsole();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.BindAnalytics();

builder.Services.AddScopedSelfAndInterfaces<EloRating>();
builder.Services.AddScoped<IPlayerItemsSystem, PlayerItemsSystem>();
builder.Services.AddScoped<IRollbackable>(s => s.GetService<DataContext>());
builder.Services.AddScoped<IRollbackSystem, RollbackSystem>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseToken(builder.Configuration.GetValue<string>("Token"));

app.UseBuffering();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();