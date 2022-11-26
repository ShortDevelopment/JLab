using ShortDev.JLab.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton(JvmService.Create());

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseRouting();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
