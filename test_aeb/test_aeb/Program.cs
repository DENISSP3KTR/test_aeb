global using Microsoft.EntityFrameworkCore;
global using test_aeb;
global using test_aeb.Context;
global using AutoMapper;
global using test_aeb.Mapper;
using FluentValidation.AspNetCore;
using test_aeb.Models;
using test_aeb.Validators;
using FluentValidation;
using System.Reflection;

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
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options=>options.AddPolicy("CorsPolicy",
    builder => {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    }
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("swagger/v1/swagger.json", "ToDo API");
    });
}
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
