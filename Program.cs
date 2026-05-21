using Microsoft.EntityFrameworkCore;
using MInimalApi.Core.Entities;
using MInimalApi.Infrastructure.Data;

namespace MinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Add Swagger for testing
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            // ✅ Minimal API endpoints (no controllers needed)
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 1200 },
                new Product { Id = 2, Name = "Phone", Price = 800 }
            };

            app.MapGet("/", () => "Welcome to Minimal API!");

            app.MapGet("/products", () => products);

            app.MapGet("/products/{id}", (int id) =>
                products.FirstOrDefault(p => p.Id == id) is Product p ? Results.Ok(p) : Results.NotFound());

            app.MapPost("/products", (Product product) =>
            {
                product.Id = products.Max(p => p.Id) + 1;
                products.Add(product);
                return Results.Created($"/products/{product.Id}", product);
            });

            app.MapPut("/products/{id}", (int id, Product updated) =>
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product is null) return Results.NotFound();
                product.Name = updated.Name;
                product.Price = updated.Price;
                return Results.NoContent();
            });

            app.MapDelete("/products/{id}", (int id) =>
            {
                var product = products.FirstOrDefault(p => p.Id == id);
                if (product is null) return Results.NotFound();
                products.Remove(product);
                return Results.NoContent();
            });

            app.Run();
        }
    }

  
}
