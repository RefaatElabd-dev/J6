using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace J6.DAL.Entities
{
    public class Product
    {
        public Product()
        {
            ProdCarts = new HashSet<ProdCart>();
            ProdOrders = new HashSet<ProdOrder>();
            //ProductBrands = new HashSet<ProductBrand>();
            ProductImages = new HashSet<ProductImage>();
            Reviews = new HashSet<Review>();
            StoreProducts = new HashSet<StoreProduct>();
            Views = new HashSet<View>();
        }

        public int ProductId { get; set; }
        public double Price { get; set; }
        public int? SoldQuantities { get; set; }
        public int? Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public string ProductName { get; set; }
        public string Model { get; set; }
        public int? SubcategoryId { get; set; }
        public double? Rating { get; set; }
        public double? Discount { get; set; }
        public string Description { get; set; }
        public ShappedType Ship { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? PromotionId { get; set; }
     //   [JsonIgnore]
        public virtual Promotion Promotion { get; set; }
        public string material { set; get; }

        //shababn
        public string BrandName { get; set; }
        //public virtual ICollection<Brand> Brands { get; set; }




        public string Manufacture { get; set; }

        [JsonIgnore]
        public virtual SubCategory Subcategory { get; set; }
       // [JsonIgnore]
        public virtual ShippingDetail ShippingDetail { get; set; }
      //  [JsonIgnore]
        public virtual ICollection<ProdCart> ProdCarts { get; set; }
      //  [JsonIgnore]
        public virtual ICollection<ProdOrder> ProdOrders { get; set; }
        


        // [JsonIgnore]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
      //  [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
       // [JsonIgnore]
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
       // [JsonIgnore]
        public virtual ICollection<View> Views { get; set; }
    }
}
