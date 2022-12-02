using ShortDev.JLab.Host;
using ShortDev.JLab.Services;

if (args.Length == 1 && args[0] == HostBootstrap.CmdArgumentName)
{
    var host = HostBootstrap.InitializeHost();
    host.Run();
    return;
}

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
