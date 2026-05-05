using OrderFlow.Api.Data;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Environment
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection (DI)
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<OrderService>();

// Banco
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

