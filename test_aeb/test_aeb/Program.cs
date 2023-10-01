global using Microsoft.EntityFrameworkCore;
global using test_aeb;
global using test_aeb.Context;
global using AutoMapper;
global using test_aeb.Mapper;
using FluentValidation.AspNetCore;
using test_aeb.Models;
using test_aeb.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddFluentValidation(x =>
    {
        x.ImplicitlyValidateChildProperties = true;
    });

builder.Services.AddTransient<IValidator<ToDo_model>, TaskValidator>();

builder.Services.AddDbContext<ToDo_Context>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ToDoConnection"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
