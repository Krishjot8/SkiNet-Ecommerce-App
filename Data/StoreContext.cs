﻿using ECommerce_App.Models;
using ECommerce_App.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerce_App.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Order> Orders { get; set; }


        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        
        { 
        
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        }

    }
}
