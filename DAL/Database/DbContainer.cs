using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace J6.DAL.Database
{
    public class DbContainer : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>
        , IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbContainer() { }
        public DbContainer(DbContextOptions<DbContainer> opts) : base(opts) {}

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<ProdCart> ProdCarts { get; set; }
        public virtual DbSet<ProdOrder> ProdOrders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<MiddleSavedProduct> ProductsBag { get; set; }
        public virtual DbSet<SavedBag> SavedBag { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            base.OnModelCreating(builder);
            builder.Entity<AppUser>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .HasColumnType("bit")
                    .HasColumnName("IsActive")
                    .HasDefaultValue(true)
                    .ValueGeneratedOnAdd()
                    .IsFixedLength(true);

                entity.HasMany(ur => ur.userRoles)
                .WithOne(u => u.user)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            builder.Entity<AppRole>()
                .HasMany(ur => ur.userRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");
                entity.Property(e => e.Cost).HasColumnName("cost");
                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");
            });

            builder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                     .HasColumnType("datetime2")
                     .HasDefaultValueSql("GETDATE()")
                     .ValueGeneratedOnAdd()
                     .HasColumnName("createdAt");


                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");
            });

            builder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Date)
                     .HasColumnType("datetime2")
                     .HasDefaultValueSql("GETDATE()")
                     .ValueGeneratedOnAdd();

                entity.Property(e => e.Paymenttype)
                    .HasMaxLength(50)
                    .HasColumnName("paymenttype")
                    .HasDefaultValue("PayPal")
                    .ValueGeneratedOnAdd()
                    .IsFixedLength(true);
            });

            builder.Entity<ProdCart>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.ProductId });

                entity.ToTable("prod_Cart");

                entity.Property(e => e.CartId).HasColumnName("cartId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.quantity)
                       .HasColumnType("int")
                       .HasDefaultValue(1)
                       .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.ProdCarts)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_prod_Cart_cart");

               entity.HasOne(d => d.CartNavigation)
                    .WithMany(p => p.ProdCarts)
                    .HasForeignKey(d => d.CartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_prod_Cart_product"); 
            });

            builder.Entity<ProdOrder>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProdOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_prod_order_Orders");

                entity.Property(e => e.quantity)
                       .HasColumnType("int")
                       .HasDefaultValue(1)
                       .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProdOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_prod_order_product");
            });

            builder.Entity<Product>(entity =>
            {

                entity.Property(e => e.CreatedAt)
                       .HasColumnType("datetime2")
                       .HasDefaultValueSql("GETDATE()")
                       .ValueGeneratedOnAdd()
                       .HasColumnName("createdAt");

                entity.Property(e => e.Rating)
                       .HasColumnType("float")
                       .HasDefaultValue(3.8)
                       .ValueGeneratedOnAdd();

                entity.Property(e => e.Discount)
                       .HasColumnType("float")
                       .HasDefaultValue(0)
                       .ValueGeneratedOnAdd();

                entity.Property(e => e.Ship)
                    .HasMaxLength(50)
                    .HasColumnName("ship");

                entity.Property(e => e.Size)
                    .HasMaxLength(10)
                    .HasColumnName("size")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_product_Promotions");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SubcategoryId)
                    .HasConstraintName("FK_product_subCategory");

                entity.HasMany(P => P.ProductsBag)
                   .WithOne(P => P.Product)
                   .HasForeignKey(P => P.ProductId)
                   .IsRequired();
            });



            builder.Entity<Review>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.ProductId });

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_product");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reviews_Customer");

                entity.Property(e => e.CreationTime)
                     .HasColumnType("datetime2")
                     .HasDefaultValueSql("GETDATE()")
                     .ValueGeneratedOnAdd()
                     .HasColumnName("CreationTime");
            });
          

            builder.Entity<SubCategory>(entity =>
            {

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETDATE()")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("createdAt");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_subCategory_category");
            });

            builder.Entity<View>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.ProductId });

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Views_product");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Views)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Views_Customer");

                entity.Property(e => e.CreationDate)
                  .HasColumnType("datetime2")
                  .HasDefaultValueSql("GETDATE()")
                  .ValueGeneratedOnAdd();
            });

            builder.Entity<SavedBag>(entity =>
            {

                entity.HasMany(B => B.ProductsBag)
                .WithOne(P => P.Bag)
                .HasForeignKey(P => P.SaveBagId)
                .IsRequired();
            });

            builder.Entity<MiddleSavedProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.SaveBagId });
            });
        }

    }
}