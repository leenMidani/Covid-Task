using COVID.Domain.Context;
using COVID.Mapper;
using COVID.Resources;
using COVID.Services;
using COVID.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ServicesLayer.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(c =>
 c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddValidatorsFromAssemblyContaining<PatientValidator>();

//builder.Services.AddScoped<IValidator<PatientDto>, PatientValidator>();
//   
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PatientContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Conn")));
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IPatientHistoryService, PatientHistoryService>();
builder.Services.AddAutoMapper(typeof(ResourceMapper));


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
