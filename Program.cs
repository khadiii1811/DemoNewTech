using Microsoft.EntityFrameworkCore;
using CustomerManagement.Data;
using CustomerManagement.Services;
using CustomerManagement.Repositories;
using Npgsql.EntityFrameworkCore.PostgreSQL; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<YourDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    
// Add Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Add Services
builder.Services.AddScoped<ICustomerService, CustomerService>();

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
