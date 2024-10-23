using System.Text;
using dotenv.net;
using RPSSL.Api.Extensions;

DotEnv.Fluent()
    .WithoutExceptions()
    .WithTrimValues()
    .WithEncoding(Encoding.UTF8)
    .WithoutOverwriteExistingVars()
    .Load();

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.Build();

app.Configure();
app.Run();