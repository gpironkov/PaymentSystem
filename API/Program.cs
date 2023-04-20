using API.Middleware;
using API.PaymentSystem.Data;
using API.PaymentSystem.Repository;
using API.PaymentSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<PaymentDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseMiddleware<ServerErrorException>();

//app.UseHttpsRedirection();
//app.UseAuthorization();

app.MapControllers();

app.Run();
