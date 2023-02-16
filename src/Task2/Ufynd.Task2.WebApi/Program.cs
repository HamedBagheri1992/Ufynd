using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ufynd.Task2.Application;
using Ufynd.Task2.Application.Middlewares;
using Ufynd.Task2.Infrastructure;
using Ufynd.Task2.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApiVersioning();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Services.DbContextInitializer();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomExceptionHandler();
app.MapControllers();

app.Run();
