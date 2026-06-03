using OrderFlow.Api.Data;
using Microsoft.EntityFrameworkCore;

using OrderFlow.Api.Services;

using OrderFlow.Api.Services.Interfaces;

using FluentValidation;
using OrderFlow.Api.DTOs.Validators;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Environment
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection (DI)
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Banco
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Validadores
builder.Services.AddValidatorsFromAssemblyContaining<CreateClientRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();

// Relatório de erros
builder.Services.AddProblemDetails();

// Autenticação
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapControllers();

app.Run();

