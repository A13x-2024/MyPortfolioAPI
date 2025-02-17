
using Labb3Fullstack.Data;
using Labb3Fullstack.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb3Fullstack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<PortfolioDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<PortfolioService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapPost("/portfolio", async (Portfolio portfolio, PortfolioService service) =>
            {
                await service.AddToPortfolio(portfolio);
                return Results.Ok();
            });

            app.MapGet("/portfolios", async (PortfolioService service) =>
            {
                var getAll = await service.GetPortfolios();
                return Results.Ok(getAll);
            });

            app.MapPut("/portfolio/{id}", async (int id, Portfolio portfolio, PortfolioService service) =>
            {
                var updatedPortfolio = await service.UpdatePortfolio(id, portfolio);
                if (updatedPortfolio == null)
                    return Results.NotFound("Portfolio not found");

                return Results.Ok(updatedPortfolio);
            });

            app.MapDelete("/portfolio/{id}", async (int id, PortfolioService service) =>
            {
                var isDeleted = await service.DeleteFromPortfolio(id);
                if (isDeleted == null)
                {
                    return Results.NotFound("Portfolio not found");
                }
                return Results.Ok(isDeleted);
            });


            app.Run();
        }
    }
}
