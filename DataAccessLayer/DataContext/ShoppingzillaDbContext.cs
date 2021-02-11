using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using DataAccessLayer.Entities;

namespace DataAccessLayer.DataContext
{
    public class ShoppingzillaDbContext : DbContext
    {

        public class OptionsBuild
        {
            public OptionsBuild()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<ShoppingzillaDbContext>();
                opsBuilder.UseSqlServer(settings.connectionstring);
                dbOptions = opsBuilder.Options;
            }
            public DbContextOptionsBuilder<ShoppingzillaDbContext> opsBuilder { get; set; }
            public DbContextOptions<ShoppingzillaDbContext> dbOptions { get; set; }
            private AppConfiguration settings { get; set; }
        }

        public static OptionsBuild ops = new OptionsBuild();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public ShoppingzillaDbContext(DbContextOptions<ShoppingzillaDbContext> options) : base(options) { }


        public DbSet<Brands> Brands { get; set; }

        public DbSet<CartItems> CartItems { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<DeliveryAddress> DeliveryAddresses { get; set; }

        public DbSet<LoginCredentials> LoginDetails { get; set; }

        public DbSet<Models> Models { get; set; }

        public DbSet<OrderDetails> Orders { get; set; }

        public DbSet<OrderItems> OrderItems { get; set; }

        public DbSet<OrderStatus> OrderStatus { get; set; }

        public DbSet<PaymentModes> PaymentModes { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<SubCategories> SubCategories { get; set; }

        public DbSet<UserDetails> UserDetails { get; set; }


    }
}