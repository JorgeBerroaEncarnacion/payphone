using Data;
using Microsoft.Extensions.DependencyInjection;
using Model.Models;
using Services.Interfaces;
using Services.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomDbContext<AppDBContext>(builder.Configuration);
builder.Services.AddScoped<ICustomerService,CustomerService>();
builder.Services.AddScoped<IPedidoService,PedidosService>();
builder.Services.AddScoped<IHistorialService,HistorialService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
await dbContext.Database.EnsureCreatedAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
