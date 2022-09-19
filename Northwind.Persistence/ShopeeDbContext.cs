using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Northwind.Domain.Entities;
using System;

namespace Northwind.Persistence
{
    //penghubung dengan tabel yang ada didomain
    //meregister ke DBDomain
    public class ShopeeDbContext : DbContext // kalau error klik ctrl + . nnti diarahkab untuk menggunakan   using Microsoft.EntityFrameworkCore;
    {

        private static IConfigurationRoot configuration;

        public ShopeeDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
