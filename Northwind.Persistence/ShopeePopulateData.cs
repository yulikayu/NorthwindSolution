/*using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Northwind.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence
{
    public class ShopeePopulateData
    {
        public static void PopulateData(IApplicationBuilder app)
        {
            ShopeeDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService < NorthwindContext>();

            if(context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                   new Category { CategoryName = "Laptop", Description = "Komputer, Laptop, PC",PhotoImage=String.Empty },
                   new Category { CategoryName = "Handphone", Description = "Hp", PhotoImage = String.Empty },
                   new Category { CategoryName = "T-Shirt", Description = "T-Shirt man", PhotoImage = String.Empty }
                  );
             }
            context.SaveChanges();

          *//*  if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Monitor LG",
                        Description = "LG 16Inch",
                        CategoryId = 1,
                        Price = 500_000

                    },
                    new Product
                    {
                        Name = "Logitect Mouse 330",
                        Description = "Wireless Silent Click",
                        CategoryId = 1,
                        Price = 159_000
                    },
                    new Product
                    {
                        Name = "Keyboard K580",
                        Description = "Slim multidevice for window",
                        CategoryId = 1,
                        Price = 596_000
                    },
                    new Product
                    {
                        Name = "Xiomi Redmi Note 10Pro",
                        Description = "Xiomi Amoled 6.67Inch",
                        CategoryId = 2,
                        Price = 599_000
                    },
                    new Product
                    {
                        Name = "PowerBank Magsafe",
                        Description = "Anker PowerCore 5k",
                        CategoryId = 2,
                        Price = 335_000
                    },
                    new Product
                    {
                        Name = "Chino Pendek",
                        Description = "Chino pendek pria dewasa",
                        CategoryId = 3,
                        Price = 34_000
                    },
                    new Product
                    {
                        Name = "Kemaja Alisan",
                        Description = "Semua ukuran ada",
                        CategoryId = 3,
                        Price = 76_000
                    },
                     new Product
                     {
                         Name = "Kemaja Dony Man",
                         Description = "Kemeja pria lengan pendek",
                         CategoryId = 3,
                         Price = 58_000
                     }
                    );
            }*//*
            context.SaveChanges();



        }
    }
}
*/