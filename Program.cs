using API_.NET_CRUD_Minimal_E.F_ORM.Data;
using API_.NET_CRUD_Minimal_E.F_ORM.DTOs.Student.Validation;
using API_.NET_CRUD_Minimal_E.F_ORM.Repositories;
using API_.NET_CRUD_Minimal_E.F_ORM.Repositories.Interfaces;
using API_.NET_CRUD_Minimal_E.F_ORM.Services;
using API_.NET_CRUD_Minimal_E.F_ORM.Services.Interfaces;
using API_.NET_CRUD_Minimal_E.F_ORM.Utils;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona os validadores do FluentValidation
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<CreateStudentDTOValidator>();
builder.Services.AddScoped<IValidator<int>, IdValidator>();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
