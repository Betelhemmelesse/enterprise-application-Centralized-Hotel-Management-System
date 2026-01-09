using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

using BuildingBlocks.Infrastructure;

using Modules.ReservationPricing.Application;
using Modules.ReservationPricing.Infrastructure;
using Modules.ReservationPricing.Api;

using Modules.FoodBeverage.Application;
using Modules.FoodBeverage.Infrastructure;

using Modules.CustomerManagement.Application;
using Modules.CustomerManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default")
                       ?? "Host=localhost;Database=hoteldb;Username=admin;Password=admin";

// ================== DB CONTEXTS ==================
builder.Services.AddDbContext<ReservationDbContext>(o => o.UseNpgsql(connectionString));
builder.Services.AddDbContext<FoodDbContext>(o => o.UseNpgsql(connectionString));
builder.Services.AddDbContext<CustomerDbContext>(o => o.UseNpgsql(connectionString));

// ================== OUTBOX ==================
builder.Services.AddScoped<IOutboxDbContext>(sp => sp.GetRequiredService<ReservationDbContext>());
builder.Services.AddHostedService<OutboxPublisherJob>();

// ================== REPOSITORIES ==================
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// ================== MEDIATR ==================
builder.Services.AddMediatR(typeof(CreateReservationHandler), typeof(SellFoodCommandHandler), typeof(RegisterCustomerHandler));

// ================== AUTH ==================
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "http://localhost:8080/realms/hotel-system";
        options.Audience = "backend-api";
        options.RequireHttpsMetadata = false;
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// ================== ENDPOINTS ==================
app.MapReservationEndpoints();

// Food
app.MapPost("/food/sell", async (IMediator mediator, IFoodItemRepository repo, Guid foodId) =>
{
    var cmd = new SellFoodCommand(foodId);
    var id = await mediator.Send(cmd);
    return Results.Ok(new { FoodId = id });
}).RequireAuthorization();

// Customers
app.MapPost("/customers/register", async (IMediator mediator, string name, string email) =>
{
    var cmd = new RegisterCustomerCommand(name, email);
    var id = await mediator.Send(cmd);
    return Results.Ok(new { CustomerId = id });
}).RequireAuthorization();

app.Run();
