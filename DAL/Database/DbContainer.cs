using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace J6.DAL.Database
{
    public class DbContainer : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>
        , IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbContainer() { }
        public DbContainer(DbContextOptions<DbContainer> opts) : base(opts) {}

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<ProdCart> ProdCarts { get; set; }
        public virtual DbSet<ProdOrder> ProdOrders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ShippingDetail> ShippingDetails { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreProduct> StoreProducts { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<View> Views { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<MiddleSavedProduct> ProductsBag { get; set; }
        public virtual DbSet<SavedBag> SavedBag { get; set; }


        //public virtual DbSet<City> Cities { get; set; }
        //public virtual DbSet<Government> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasMany(ur => ur.userRoles)
                .WithOne(u => u.user)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            });

            modelBuilder.Entity<AppRole>()
                .HasMany(ur => ur.userRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();


            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("cart");

                entity.Property(e => e.Cartid)
                    .ValueGeneratedNever()
                    .HasColumnName("cartid");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("date")
                    .HasColumnName("orderDate");

                entity.Property(e => e.Paymentid)
                    .HasMaxLength(50)
                    .HasColumnName("paymentid");

                entity.Property(e => e.ShippingDate)
                    .HasColumnType("date")
                    .HasColumnName("shippingDate");

                entity.Property(e => e.ShippingDetailsId).HasColumnName("shippingDetailsId");

                entity.HasOne(d => d.ShippingDetails)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ShippingDetailsId)
                    .HasConstraintName("FK_cart_ShippingDetails");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("orderId");

                entity.Property(e => e.Rating).HasColumnName("rating");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payment");

                entity.Property(e => e.PaymentId)
                    .ValueGeneratedNever()
                    .HasColumnName("paymentId");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Paymenttype)
                    .HasMaxLength(50)
                    .HasColumnName("paymenttype")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ProdCart>(entity =>
            {
                entity.HasKey(e => new { e.CartId, e.ProductId });

                entity.ToTable("prod_Cart");

                entity.Property(e => e.CartId).HasColumnName("cartId");

                entity.Property(e => e.ProductId).HasColumnName("productId");

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

            modelBuilder.Entity<ProdOrder>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.ToTable("prod_order");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(10)
                    .HasColumnName("productId")
                    .IsFixedLength(true);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProdOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_prod_order_Orders");

                entity.HasOne(d => d.OrderNavigation)
                    .WithMany(p => p.ProdOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_prod_order_product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("productId");

                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("color");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("date")
                    .HasColumnName("deletedAt");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Discount)
                    .HasMaxLength(50)
                    .HasColumnName("discount");

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("model");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("price");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("productName");

                entity.Property(e => e.PromotionId).HasColumnName("promotionId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Ship)
                    .HasMaxLength(50)
                    .HasColumnName("ship");

                entity.Property(e => e.Size)
                    .HasMaxLength(10)
                    .HasColumnName("size")
                    .IsFixedLength(true);

                entity.Property(e => e.SoldQuantities).HasColumnName("soldQuantities");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategoryId");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PromotionId)
                    .HasConstraintName("FK_product_Promotions");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SubcategoryId)
                    .HasConstraintName("FK_product_subCategory");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ImageId });

                entity.ToTable("ProductImage");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.ImageId).HasColumnName("imageId");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImage_product");
            });
                modelBuilder.Entity<Promotion>(entity =>
                    {
                        entity.Property(e => e.Id)
                            .ValueGeneratedNever()
                            .HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Discount).HasColumnName("discount");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("productId");
                entity.Property(e => e.CustomerId).HasColumnName("customerId");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.Rating)
                    .HasMaxLength(50)
                    .HasColumnName("rating");

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

                entity.HasKey(e => new { e.CustomerId, e.ProductId });
            });

            modelBuilder.Entity<ShippingDetail>(entity =>
            {
                entity.HasKey(e => e.ShippingDetailsId);

                entity.Property(e => e.ShippingDetailsId)
                    .ValueGeneratedNever()
                    .HasColumnName("shippingDetailsId");

                entity.Property(e => e.PaymentId).HasColumnName("paymentId");

                entity.Property(e => e.PurshesCost)
                    .HasMaxLength(50)
                    .HasColumnName("purshesCost");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.ShippingDetails)
                    .HasForeignKey(d => d.PaymentId)
                    .HasConstraintName("FK_ShippingDetails_payment");

                entity.HasOne(d => d.ShippingDetails)
                    .WithOne(p => p.ShippingDetail)
                    .HasForeignKey<ShippingDetail>(d => d.ShippingDetailsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ShippingDetails_product");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("statusId");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(50)
                    .HasColumnName("statusName");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.StoreId)
                    .ValueGeneratedNever()
                    .HasColumnName("storeId");

                entity.Property(e => e.BuildingNumber)
                    .HasMaxLength(50)
                    .HasColumnName("buildingNumber");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .HasColumnName("street");
            });

            modelBuilder.Entity<StoreProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.StoreId });

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.Quantities).HasColumnName("quantities");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StoreProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreProducts_product");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreProducts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreProducts_Stores");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("subCategory");

                entity.Property(e => e.SubcategoryId).HasColumnName("subcategoryId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.SubcategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("subcategoryName");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("date")
                    .HasColumnName("updatedAt");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_subCategory_category");
            });


            modelBuilder.Entity<View>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.IsFar)
                    .HasMaxLength(10)
                    .HasColumnName("isFar");

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

                entity.Property(e=>e.CreationDate)
                 .HasColumnType("datetime2")
                 .HasDefaultValueSql("GETDATE()")
                 .ValueGeneratedOnAdd();


                entity.HasKey(e => new { e.CustomerId, e.ProductId });
            });

            modelBuilder.Entity<SavedBag>(entity =>
            {
                entity.HasMany(B => B.ProductsBag)
                .WithOne(P => P.Bag)
                .HasForeignKey(P => P.SaveBagId)
                .IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasMany(P => P.ProductsBag)
                .WithOne(P => P.Product)
                .HasForeignKey(P => P.ProductId)
                .IsRequired();
            });

            modelBuilder.Entity<MiddleSavedProduct>(entity =>
            {
                entity.HasKey("ProductId", "SaveBagId");
            });
        }

    }
}
