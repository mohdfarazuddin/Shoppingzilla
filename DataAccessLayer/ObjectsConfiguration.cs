using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    class ObjectsConfiguration
    {

        public class BrandsConfiguration : IEntityTypeConfiguration<Brands>
        {
            public void Configure(EntityTypeBuilder<Brands> modelBuilder)
            {

                modelBuilder.ToTable("tbl_Brands");
                modelBuilder.HasKey(b => b.Id);
                modelBuilder.Property(b => b.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(b => b.Name)
                            .HasColumnType("nvarchar(30)")
                            .IsRequired();
                modelBuilder.Property(b => b.SubCategoryID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();

                modelBuilder.HasOne(b => b.SubCategory)
                            .WithMany(sc => sc.Brands)
                            .HasForeignKey(sc => sc.SubCategoryID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
            }
        }
        
        public class CartItemsConfiguration : IEntityTypeConfiguration<CartItems>
        {
            public void Configure(EntityTypeBuilder<CartItems> modelBuilder)
            {

                modelBuilder.ToTable("tbl_CartItems");
                modelBuilder.HasKey(ci => new { ci.UserID, ci.ModelID });
                modelBuilder.Property(ci => ci.UserID)
                            .ValueGeneratedNever()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(ci => ci.ModelID)
                            .ValueGeneratedNever()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(ci => ci.Quantity)
                            .HasColumnType("int")
                            .IsRequired();
                modelBuilder.Property(ci => ci.Price)
                            .HasColumnType("float")
                            .IsRequired();

                modelBuilder.HasOne(ci => ci.User)
                            .WithMany(u => u.CartItems)
                            .HasForeignKey(ci => ci.UserID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.HasOne(ci => ci.Model)
                            .WithMany(m => m.Carts)
                            .HasForeignKey(ci => ci.ModelID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
        {
            public void Configure(EntityTypeBuilder<Categories> modelBuilder)
            {

                modelBuilder.ToTable("tbl_Categories");
                modelBuilder.HasKey(c => c.Id);
                modelBuilder.Property(c => c.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(c => c.Name)
                            .HasColumnType("nvarchar(30)")
                            .IsRequired();
            }
        }

        public class DeliveryAddressConfiguration : IEntityTypeConfiguration<DeliveryAddress>
        {
            public void Configure(EntityTypeBuilder<DeliveryAddress> modelBuilder)
            {

                modelBuilder.ToTable("tbl_DeliveryAddresses");
                modelBuilder.HasKey(da => da.Id);
                modelBuilder.Property(da => da.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(da => da.UserID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(da => da.AddressLine)
                            .HasColumnType("nvarchar(50)")
                            .IsRequired();
                modelBuilder.Property(da => da.City)
                            .HasColumnType("nvarchar(25)")
                            .IsRequired();
                modelBuilder.Property(da => da.State)
                            .HasColumnType("nvarchar(30)")
                            .IsRequired();
                modelBuilder.Property(da => da.PinCode)
                            .HasColumnType("nvarchar(10)")
                            .IsRequired();

                modelBuilder.HasOne(da => da.User)
                            .WithMany(u => u.DeliveryAddresses)
                            .HasForeignKey(da => da.UserID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class LoginCredentialsConfiguration : IEntityTypeConfiguration<LoginCredentials>
        {
            public void Configure(EntityTypeBuilder<LoginCredentials> modelBuilder)
            {

                modelBuilder.ToTable("tbl_LoginCredentials");
                modelBuilder.HasKey(lc => lc.Id);
                modelBuilder.HasIndex(lc => lc.EmailID).IsUnique();
                modelBuilder.Property(lc => lc.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(lc => lc.EmailID)
                            .HasColumnType("nvarchar(30)")
                            .IsRequired();
                modelBuilder.Property(lc => lc.PasswordHash)
                            .HasColumnType("nvarchar(100)")
                            .IsRequired();
            }
        }

        public class ModelsConfiguration : IEntityTypeConfiguration<Models>
        {
            public void Configure(EntityTypeBuilder<Models> modelBuilder)
            {

                modelBuilder.ToTable("tbl_Models");
                modelBuilder.HasKey(m => m.Id);
                modelBuilder.Property(m => m.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(m => m.ProductID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(m => m.Colour)
                            .HasColumnType("nvarchar(15)");
                modelBuilder.Property(m => m.Size)
                            .HasColumnType("nvarchar(20)");
                modelBuilder.Property(m => m.Price)
                            .HasColumnType("float")
                            .IsRequired();
                modelBuilder.Property(m => m.Stock)
                            .HasColumnType("int")
                            .IsRequired();
                modelBuilder.Property(m => m.imgurl)
                            .HasColumnType("nvarchar(100)")
                            .IsRequired();

                modelBuilder.HasOne(m => m.Product)
                            .WithMany(p => p.Models)
                            .HasForeignKey(m => m.ProductID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
            }
        }

        public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
        {
            public void Configure(EntityTypeBuilder<OrderDetails> modelBuilder)
            {

                modelBuilder.ToTable("tbl_OrderDetails");
                modelBuilder.HasKey(o => o.Id);
                modelBuilder.Property(o => o.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(o => o.UserID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(o => o.DeliveryAddID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(o => o.PaymentModeID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(o => o.OrderStatusID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(o => o.TotalPrice)
                            .HasColumnType("float")
                            .IsRequired();
                modelBuilder.Property(o => o.Date)
                            .HasColumnType("smalldatetime")
                            .IsRequired();
                modelBuilder.Property(o => o.TransactionID)
                            .HasColumnType("nvarchar(50)")
                            .IsRequired();

                modelBuilder.HasOne(o => o.User)
                            .WithMany(u => u.Orders)
                            .HasForeignKey(o => o.UserID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.HasOne(o => o.DeliveryAddress)
                            .WithMany(da => da.Orders)
                            .HasForeignKey(o => o.DeliveryAddID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
                modelBuilder.HasOne(o => o.PaymentType)
                            .WithMany(p => p.Orders)
                            .HasForeignKey(o => o.PaymentModeID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
                modelBuilder.HasOne(o => o.OrderStatus)
                            .WithMany(os => os.Orders)
                            .HasForeignKey(o => o.OrderStatusID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
            }
        }

        public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItems>
        {
            public void Configure(EntityTypeBuilder<OrderItems> modelBuilder)
            {

                modelBuilder.ToTable("tbl_OrderItems");
                modelBuilder.HasKey(oi => new { oi.OrderID, oi.ModelID });
                modelBuilder.Property(oi => oi.OrderID)
                            .ValueGeneratedNever()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(ci => ci.ModelID)
                            .ValueGeneratedNever()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(ci => ci.Quantity)
                            .HasColumnType("int")
                            .IsRequired();
                modelBuilder.Property(ci => ci.Price)
                            .HasColumnType("float")
                            .IsRequired();

                modelBuilder.HasOne(oi => oi.Order)
                            .WithMany(o => o.OrderItems)
                            .HasForeignKey(oi => oi.OrderID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.HasOne(oi => oi.Model)
                            .WithMany(m => m.Orders)
                            .HasForeignKey(oi => oi.ModelID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
        {
            public void Configure(EntityTypeBuilder<OrderStatus> modelBuilder)
            {

                modelBuilder.ToTable("tbl_OrderStatus");
                modelBuilder.HasKey(os => os.Id);
                modelBuilder.Property(os => os.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(os => os.Status)
                            .HasColumnType("nvarchar(30)")
                            .IsRequired();
            }
        }

        public class PaymentModesConfiguration : IEntityTypeConfiguration<PaymentModes>
        {
            public void Configure(EntityTypeBuilder<PaymentModes> modelBuilder)
            {

                modelBuilder.ToTable("tbl_PaymentModes");
                modelBuilder.HasKey(pm => pm.Id);
                modelBuilder.Property(pm => pm.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(pm => pm.PaymentType)
                            .HasColumnType("nvarchar(50)")
                            .IsRequired();
            }
        }

        public class ProductsConfiguration : IEntityTypeConfiguration<Products>
        {
            public void Configure(EntityTypeBuilder<Products> modelBuilder)
            {

                modelBuilder.ToTable("tbl_Products");
                modelBuilder.HasKey(p => p.Id);
                modelBuilder.Property(p => p.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(p => p.BrandID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(p => p.Name)
                            .HasColumnType("nvarchar(50)")
                            .IsRequired();
                modelBuilder.Property(p => p.Description)
                            .HasColumnType("nvarchar(300)")
                            .IsRequired();

                modelBuilder.HasOne(p => p.Brand)
                            .WithMany(b => b.Products)
                            .HasForeignKey(p => p.BrandID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
            }
        }

        public class RefreshTokensConfiguration : IEntityTypeConfiguration<RefreshTokens>
        {
            public void Configure(EntityTypeBuilder<RefreshTokens> modelBuilder)
            {

                modelBuilder.ToTable("tbl_RefreshTokens");
                modelBuilder.HasKey(r => r.UserID);
                modelBuilder.Property(r => r.UserID)
                            .ValueGeneratedNever()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(r => r.RefreshToken)
                            .HasColumnType("nvarchar(50)")
                            .IsRequired();

                modelBuilder.HasOne(r => r.user)
                            .WithOne(u => u.RefreshToken)
                            .HasForeignKey<RefreshTokens>(r => r.UserID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
            }
        }

        public class RolesConfiguration : IEntityTypeConfiguration<Roles>
        {
            public void Configure(EntityTypeBuilder<Roles> modelBuilder)
            {

                modelBuilder.ToTable("tbl_Roles");
                modelBuilder.HasKey(r => r.Id);
                modelBuilder.Property(r => r.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("int")
                            .IsRequired();
                modelBuilder.Property(r => r.RoleType)
                            .HasColumnType("nvarchar(30)")
                            .IsRequired();
            }
        }

        public class SubCategoriesConfiguration : IEntityTypeConfiguration<SubCategories>
        {
            public void Configure(EntityTypeBuilder<SubCategories> modelBuilder)
            {

                modelBuilder.ToTable("tbl_SubCategories");
                modelBuilder.HasKey(sc => sc.Id);
                modelBuilder.Property(sc => sc.Id)
                            .ValueGeneratedOnAdd()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(sc => sc.CategoryID)
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(sc => sc.Name)
                            .HasColumnType("nvarchar(50)")
                            .IsRequired();

                modelBuilder.HasOne(sc => sc.Category)
                            .WithMany(c => c.SubCategories)
                            .HasForeignKey(sc => sc.CategoryID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
            }
        }

        public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
        {
            public void Configure(EntityTypeBuilder<UserDetails> modelBuilder)
            {

                modelBuilder.ToTable("tbl_UserDetails");
                modelBuilder.HasKey(u => u.UserID);
                modelBuilder.Property(u => u.UserID)
                            .ValueGeneratedNever()
                            .HasColumnType("uniqueidentifier")
                            .IsRequired();
                modelBuilder.Property(u => u.RoleID)
                            .HasColumnType("int")
                            .IsRequired();
                modelBuilder.Property(u => u.FirstName)
                            .HasColumnType("nvarchar(25)")
                            .IsRequired();
                modelBuilder.Property(u => u.LastName)
                            .HasColumnType("nvarchar(25)")
                            .IsRequired();
                modelBuilder.Property(u => u.MobileNo)
                            .HasColumnType("nvarchar(15)")
                            .IsRequired();

                modelBuilder.HasOne(u => u.Logincredential)
                            .WithOne(lc => lc.User)
                            .HasForeignKey<UserDetails>(u => u.UserID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Cascade);
                modelBuilder.HasOne(u => u.Role)
                            .WithMany(r => r.Users)
                            .HasForeignKey(u => u.RoleID)
                            .IsRequired()
                            .OnDelete(DeleteBehavior.Restrict);
            }
        }

    }
}