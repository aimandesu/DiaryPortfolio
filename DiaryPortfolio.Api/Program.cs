using DiaryPortfolio.Api.Environment;
using DiaryPortfolio.Api.Extensions;
using DiaryPortfolio.Application;
using DiaryPortfolio.Domain.Entities;
using DiaryPortfolio.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.ConfigureJWTPolicy(builder.Configuration);
builder.Services.ConfigureIdentityPolicy();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });


var app = builder.Build();

//app.EnsureSeed();
app.MapIdentityApi<UserModel>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
