using System.Text;
using dotenv.net;
using RPSSL.Api.Common.Extensions;
using Serilog;

DotEnv.Fluent()
    .WithoutExceptions()
    .WithTrimValues()
    .WithEncoding(Encoding.UTF8)
    .WithoutOverwriteExistingVars()
    .Load();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.ConfigureServices();

var app = builder.Build();

app.Configure();
app.Run();