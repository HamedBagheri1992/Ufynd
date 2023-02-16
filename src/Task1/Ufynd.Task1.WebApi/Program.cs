using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ufynd.Task1.WebApi.Common.Settings;
using Ufynd.Task1.WebApi.Middlewares;
using Ufynd.Task1.WebApi.Services;
using Ufynd.Task1.WebApi.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.Configure<OnlineXPathStringsConfigurationModel>(builder.Configuration.GetSection(OnlineXPathStringsConfigurationModel.NAME));
builder.Services.Configure<OfflineXPathStringsConfigurationModel>(builder.Configuration.GetSection(OfflineXPathStringsConfigurationModel.NAME));

builder.Services.AddScoped<IDataExtractorService, DataExtractorService>();
builder.Services.AddScoped<IOnlineHtmlAgilityService, OnlineHtmlAgilityService>();
builder.Services.AddScoped<IOfflineHtmlAgilityService, OfflineHtmlAgilityService>();

var app = builder.Build();

app.UseCustomExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();


app.MapControllers();

app.Run();
