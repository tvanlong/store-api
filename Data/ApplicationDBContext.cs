using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using storeAPI.Models;
using Version = storeAPI.Models.Version;

namespace storeAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Subcategories)
                .WithOne(s => s.Category)
                .HasForeignKey(s => s.CategoryId);

            modelBuilder.Entity<Subcategory>()
                .HasMany(s => s.Products)
                .WithOne(p => p.Subcategory)
                .HasForeignKey(p => p.SubcategoryId);
            
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Versions)
                .WithOne(v => v.Product)
                .HasForeignKey(v => v.ProductId);

            modelBuilder.Entity<Version>()
                .HasMany(v => v.Orders)
                .WithMany(o => o.Versions)
                .UsingEntity<OrderDetail>(
                    j => j.HasOne<Order>(od => od.Order).WithMany(o => o.OrderDetails),
                    j => j.HasOne<Version>(od => od.Version).WithMany(v => v.OrderDetails) 
                );

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Versions)
                .WithMany(v => v.Orders)
                .UsingEntity<OrderDetail>(
                    j => j.HasOne<Version>(od => od.Version).WithMany(v => v.OrderDetails),
                    j => j.HasOne<Order>(od => od.Order).WithMany(o => o.OrderDetails)
                );

            modelBuilder.Entity<Version>()
                .HasOne(v => v.CartItem)
                .WithOne(ci => ci.Version)
                .HasForeignKey<CartItem>(ci => ci.VersionId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Staff>()
                .HasMany(s => s.Orders)
                .WithOne(o => o.Staff)
                .HasForeignKey(o => o.StaffId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Cart)
                .WithOne(c => c.Customer)
                .HasForeignKey<Cart>(c => c.CustomerId);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<Version>()
                .HasOne(v => v.CartItem)
                .WithOne(ci => ci.Version)
                .HasForeignKey<CartItem>(ci => ci.VersionId);

             List<IdentityRole<int>> roles = new List<IdentityRole<int>>
            {
                new IdentityRole<int>
                {
                    Id = 1,
                    Name = "Staff",
                    NormalizedName = "STAFF",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new IdentityRole<int>
                {
                    Id = 2,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
            };
            modelBuilder.Entity<IdentityRole<int>>().HasData(roles);
        }
    }
}