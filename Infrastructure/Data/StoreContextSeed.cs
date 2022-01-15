using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(
            StoreContext context,
            ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    string brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    List<ProductBrand> brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (ProductBrand item in brands)
                    {
                        _ = context.ProductBrands.Add(item);
                    }

                    _ = await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    string typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    List<ProductType> types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (ProductType item in types)
                    {
                        _ = context.ProductTypes.Add(item);
                    }

                    _ = await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    string productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    List<Product> products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (Product item in products)
                    {
                        _ = context.Products.Add(item);
                    }

                    _ = await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ILogger<StoreContextSeed> logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
