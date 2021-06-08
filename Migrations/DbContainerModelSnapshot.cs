﻿// <auto-generated />
using System;
using J6.DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace J6.Migrations
{
    [DbContext(typeof(DbContainer))]
    partial class DbContainerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("J6.DAL.Entities.Address", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("J6.DAL.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("J6.DAL.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("AddressID")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AddressID");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("J6.DAL.Entities.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("J6.DAL.Entities.Cart", b =>
                {
                    b.Property<int>("Cartid")
                        .HasColumnType("int")
                        .HasColumnName("cartid");

                    b.Property<int?>("Cost")
                        .HasColumnType("int")
                        .HasColumnName("cost");

                    b.Property<int>("CustimerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("date")
                        .HasColumnName("orderDate");

                    b.Property<string>("Paymentid")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("paymentid");

                    b.Property<DateTime?>("ShippingDate")
                        .HasColumnType("date")
                        .HasColumnName("shippingDate");

                    b.Property<int?>("ShippingDetailsId")
                        .HasColumnType("int")
                        .HasColumnName("shippingDetailsId");

                    b.HasKey("Cartid");

                    b.HasIndex("CustimerId")
                        .IsUnique();

                    b.HasIndex("ShippingDetailsId");

                    b.ToTable("cart");
                });

            modelBuilder.Entity("J6.DAL.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("categoryId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("categoryName");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("createdAt");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updatedAt");

                    b.HasKey("CategoryId");

                    b.ToTable("category");
                });

            modelBuilder.Entity("J6.DAL.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderId");

                    b.Property<int>("CustimerId")
                        .HasColumnType("int");

                    b.Property<int?>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustimerId")
                        .IsUnique();

                    b.ToTable("orders");
                });

            modelBuilder.Entity("J6.DAL.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .HasColumnType("int")
                        .HasColumnName("paymentId");

                    b.Property<int?>("Amount")
                        .HasColumnType("int")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<string>("Paymenttype")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasColumnName("paymenttype")
                        .IsFixedLength(true);

                    b.HasKey("PaymentId");

                    b.ToTable("payment");
                });

            modelBuilder.Entity("J6.DAL.Entities.ProdCart", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int")
                        .HasColumnName("cartId");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productId");

                    b.HasKey("CartId", "ProductId");

                    b.ToTable("prod_Cart");
                });

            modelBuilder.Entity("J6.DAL.Entities.ProdOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderId");

                    b.Property<int>("ProductId")
                        .HasMaxLength(10)
                        .HasColumnType("int")
                        .HasColumnName("productId")
                        .IsFixedLength(true);

                    b.HasKey("OrderId", "ProductId");

                    b.ToTable("prod_order");
                });

            modelBuilder.Entity("J6.DAL.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productId");

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Color")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("color");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("createdAt");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("date")
                        .HasColumnName("deletedAt");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<double?>("Discount")
                        .HasMaxLength(50)
                        .HasColumnType("float")
                        .HasColumnName("discount");

                    b.Property<string>("Manufacture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("model");

                    b.Property<double>("Price")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<string>("ProductName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("productName");

                    b.Property<int?>("PromotionId")
                        .HasColumnType("int")
                        .HasColumnName("promotionId");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<double?>("Rating")
                        .HasColumnType("float")
                        .HasColumnName("rating");

                    b.Property<int>("Ship")
                        .HasMaxLength(50)
                        .HasColumnType("int")
                        .HasColumnName("ship");

                    b.Property<string>("Size")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("size")
                        .IsFixedLength(true);

                    b.Property<int?>("SoldQuantities")
                        .HasColumnType("int")
                        .HasColumnName("soldQuantities");

                    b.Property<int?>("SubcategoryId")
                        .HasColumnType("int")
                        .HasColumnName("subcategoryId");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updatedAt");

                    b.Property<string>("material")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("PromotionId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("product");
                });

            modelBuilder.Entity("J6.DAL.Entities.ProductImage", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productId");

                    b.Property<int>("ImageId")
                        .HasColumnType("int")
                        .HasColumnName("imageId");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.HasKey("ProductId", "ImageId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("J6.DAL.Entities.Promotion", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int?>("Discount")
                        .HasColumnType("int")
                        .HasColumnName("discount");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("J6.DAL.Entities.Review", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customerId");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productId");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("comment");

                    b.Property<string>("Rating")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("rating");

                    b.HasKey("CustomerId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("J6.DAL.Entities.ShippingDetail", b =>
                {
                    b.Property<int>("ShippingDetailsId")
                        .HasColumnType("int")
                        .HasColumnName("shippingDetailsId");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("int")
                        .HasColumnName("paymentId");

                    b.Property<string>("PurshesCost")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("purshesCost");

                    b.HasKey("ShippingDetailsId");

                    b.HasIndex("PaymentId");

                    b.ToTable("ShippingDetails");
                });

            modelBuilder.Entity("J6.DAL.Entities.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("statusId");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("orderId");

                    b.Property<string>("StatusName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("statusName");

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("J6.DAL.Entities.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnType("int")
                        .HasColumnName("storeId");

                    b.Property<string>("BuildingNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("buildingNumber");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("city");

                    b.Property<int>("SellerId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("street");

                    b.HasKey("StoreId");

                    b.HasIndex("SellerId")
                        .IsUnique();

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("J6.DAL.Entities.StoreProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productId");

                    b.Property<int>("StoreId")
                        .HasColumnType("int")
                        .HasColumnName("storeId");

                    b.Property<int?>("Quantities")
                        .HasColumnType("int")
                        .HasColumnName("quantities");

                    b.HasKey("ProductId", "StoreId");

                    b.HasIndex("StoreId");

                    b.ToTable("StoreProducts");
                });

            modelBuilder.Entity("J6.DAL.Entities.SubCategory", b =>
                {
                    b.Property<int>("SubcategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("subcategoryId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("categoryId");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("date")
                        .HasColumnName("createdAt");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("image");

                    b.Property<string>("SubcategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("subcategoryName");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("date")
                        .HasColumnName("updatedAt");

                    b.HasKey("SubcategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("subCategory");
                });

            modelBuilder.Entity("J6.DAL.Entities.View", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("productId");

                    b.Property<string>("IsFar")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("isFar");

                    b.HasKey("CustomerId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Views");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("J6.DAL.Entities.AppUser", b =>
                {
                    b.HasOne("J6.DAL.Entities.Address", "Address")
                        .WithMany("AppUsers")
                        .HasForeignKey("AddressID");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("J6.DAL.Entities.AppUserRole", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppRole", "Role")
                        .WithMany("userRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("J6.DAL.Entities.AppUser", "user")
                        .WithMany("userRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("user");
                });

            modelBuilder.Entity("J6.DAL.Entities.Cart", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", "Customer")
                        .WithOne("Cart")
                        .HasForeignKey("J6.DAL.Entities.Cart", "CustimerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("J6.DAL.Entities.ShippingDetail", "ShippingDetails")
                        .WithMany("Carts")
                        .HasForeignKey("ShippingDetailsId")
                        .HasConstraintName("FK_cart_ShippingDetails");

                    b.Navigation("Customer");

                    b.Navigation("ShippingDetails");
                });

            modelBuilder.Entity("J6.DAL.Entities.Order", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", "Customer")
                        .WithOne("Order")
                        .HasForeignKey("J6.DAL.Entities.Order", "CustimerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("J6.DAL.Entities.ProdCart", b =>
                {
                    b.HasOne("J6.DAL.Entities.Cart", "Cart")
                        .WithMany("ProdCarts")
                        .HasForeignKey("CartId")
                        .HasConstraintName("FK_prod_Cart_cart")
                        .IsRequired();

                    b.HasOne("J6.DAL.Entities.Product", "CartNavigation")
                        .WithMany("ProdCarts")
                        .HasForeignKey("CartId")
                        .HasConstraintName("FK_prod_Cart_product")
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("CartNavigation");
                });

            modelBuilder.Entity("J6.DAL.Entities.ProdOrder", b =>
                {
                    b.HasOne("J6.DAL.Entities.Order", "Order")
                        .WithMany("ProdOrders")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_prod_order_Orders")
                        .IsRequired();

                    b.HasOne("J6.DAL.Entities.Product", "OrderNavigation")
                        .WithMany("ProdOrders")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_prod_order_product")
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("OrderNavigation");
                });

            modelBuilder.Entity("J6.DAL.Entities.Product", b =>
                {
                    b.HasOne("J6.DAL.Entities.Promotion", "Promotion")
                        .WithMany("Products")
                        .HasForeignKey("PromotionId")
                        .HasConstraintName("FK_product_Promotions");

                    b.HasOne("J6.DAL.Entities.SubCategory", "Subcategory")
                        .WithMany("Products")
                        .HasForeignKey("SubcategoryId")
                        .HasConstraintName("FK_product_subCategory");

                    b.Navigation("Promotion");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("J6.DAL.Entities.ProductImage", b =>
                {
                    b.HasOne("J6.DAL.Entities.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_ProductImage_product")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("J6.DAL.Entities.Promotion", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", "Seller")
                        .WithMany("Promotions")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("J6.DAL.Entities.Review", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Reviews_Customer")
                        .IsRequired();

                    b.HasOne("J6.DAL.Entities.Product", "Product")
                        .WithMany("Reviews")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Reviews_product")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("J6.DAL.Entities.ShippingDetail", b =>
                {
                    b.HasOne("J6.DAL.Entities.Payment", "Payment")
                        .WithMany("ShippingDetails")
                        .HasForeignKey("PaymentId")
                        .HasConstraintName("FK_ShippingDetails_payment");

                    b.HasOne("J6.DAL.Entities.Product", "ShippingDetails")
                        .WithOne("ShippingDetail")
                        .HasForeignKey("J6.DAL.Entities.ShippingDetail", "ShippingDetailsId")
                        .HasConstraintName("FK_ShippingDetails_product")
                        .IsRequired();

                    b.Navigation("Payment");

                    b.Navigation("ShippingDetails");
                });

            modelBuilder.Entity("J6.DAL.Entities.Store", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", "Seller")
                        .WithOne("Store")
                        .HasForeignKey("J6.DAL.Entities.Store", "SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("J6.DAL.Entities.StoreProduct", b =>
                {
                    b.HasOne("J6.DAL.Entities.Product", "Product")
                        .WithMany("StoreProducts")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_StoreProducts_product")
                        .IsRequired();

                    b.HasOne("J6.DAL.Entities.Store", "Store")
                        .WithMany("StoreProducts")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK_StoreProducts_Stores")
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("J6.DAL.Entities.SubCategory", b =>
                {
                    b.HasOne("J6.DAL.Entities.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_subCategory_category");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("J6.DAL.Entities.View", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", "Customer")
                        .WithMany("Views")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Views_Customer")
                        .IsRequired();

                    b.HasOne("J6.DAL.Entities.Product", "Product")
                        .WithMany("Views")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Views_product")
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("J6.DAL.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("J6.DAL.Entities.Address", b =>
                {
                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("J6.DAL.Entities.AppRole", b =>
                {
                    b.Navigation("userRoles");
                });

            modelBuilder.Entity("J6.DAL.Entities.AppUser", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Order");

                    b.Navigation("Promotions");

                    b.Navigation("Reviews");

                    b.Navigation("Store");

                    b.Navigation("userRoles");

                    b.Navigation("Views");
                });

            modelBuilder.Entity("J6.DAL.Entities.Cart", b =>
                {
                    b.Navigation("ProdCarts");
                });

            modelBuilder.Entity("J6.DAL.Entities.Category", b =>
                {
                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("J6.DAL.Entities.Order", b =>
                {
                    b.Navigation("ProdOrders");
                });

            modelBuilder.Entity("J6.DAL.Entities.Payment", b =>
                {
                    b.Navigation("ShippingDetails");
                });

            modelBuilder.Entity("J6.DAL.Entities.Product", b =>
                {
                    b.Navigation("ProdCarts");

                    b.Navigation("ProdOrders");

                    b.Navigation("ProductImages");

                    b.Navigation("Reviews");

                    b.Navigation("ShippingDetail");

                    b.Navigation("StoreProducts");

                    b.Navigation("Views");
                });

            modelBuilder.Entity("J6.DAL.Entities.Promotion", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("J6.DAL.Entities.ShippingDetail", b =>
                {
                    b.Navigation("Carts");
                });

            modelBuilder.Entity("J6.DAL.Entities.Store", b =>
                {
                    b.Navigation("StoreProducts");
                });

            modelBuilder.Entity("J6.DAL.Entities.SubCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
