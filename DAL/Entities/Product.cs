﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace J6.DAL.Entities
{
    public class Product
    {
      


        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public int? SoldQuantities { get; set; }
        public int? Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public Size size { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public double? Rating { get; set; }
        public double? Discount { get; set; }
        public string Description { get; set; }
        public ShappedType Ship { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string material { set; get; }

        [ForeignKey("Brands")]
        public int? BrandId { get; set; }
        public virtual Brand Brands { get; set; }

        [ForeignKey("Promotion")]
        public int? PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }

        [ForeignKey("Subcategory")]
        public int? SubcategoryId { get; set; }
        public virtual SubCategory Subcategory { get; set; }

        public string Manufacture { get; set; }
        public virtual ShippingDetail ShippingDetail { get; set; }
        public virtual ICollection<ProdCart> ProdCarts { get; set; }
        public virtual ICollection<ProdOrder> ProdOrders { get; set; }

        public  ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public virtual ICollection<MiddleSavedProduct> ProductsBag { get; set; }

    }
}
