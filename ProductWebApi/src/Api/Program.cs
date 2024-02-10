using Carter;
using Microsoft.AspNetCore.Builder;
using ProductWebApi.Api;
using ProductWebApi.Api.Extensions;
using ProductWebApi.Application;
using ProductWebApi.Application.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog();

builder.Services.AddWebApiConfig();
builder.Services.AddApplicationCore();
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

app.UseCors(AppConstants.CorsPolicy);
app.UseStaticFiles();

app.MapSwagger();
app.MapCarter();
app.Run();