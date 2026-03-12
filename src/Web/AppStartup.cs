using Application.Offers;
using Application.Offers.Validation;
using Application.Repositories;
using Application.Suppliers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Web.Middlewares;

namespace Web;

public static class AppStartup
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        builder.Services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateOfferRequestValidator>();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");
        }

        builder.Services.AddDbContext<GazpromTestTaskDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddScoped<IOfferRepository, OfferRepository>();
        builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
        builder.Services.AddScoped<IOfferService, OfferService>();
        builder.Services.AddScoped<ISupplierService, SupplierService>();
    }

    public static void ConfigurePipeline(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<GazpromTestTaskDbContext>();
            db.Database.Migrate();
        }

        app.UseExceptionHandling();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapControllers();
    }
}
