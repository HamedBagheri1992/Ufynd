using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ufynd.Task2.Application.Middlewares;
using Ufynd.Task3.WebApi.Services;
using Ufynd.Task3.WebApi.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHotelService, HotelService>();

var app = builder.Build();
app.UseCustomExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
