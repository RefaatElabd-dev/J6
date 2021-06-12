using J6.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Price { get; set; }
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
        public string BrandName { get; set; }
        public string Manufacture { get; set; }

     
        public SubCategory Subcategory { get; set; }
   
        public  ShippingDetail ShippingDetail { get; set; }
 
        public ICollection<ProdCart> ProdCarts { get; set; }
       
        public  ICollection<ProdOrder> ProdOrders { get; set; }


        
        public ICollection<ProductImage> ProductImages { get; set; }
    
        public  ICollection<Review> Reviews { get; set; }
     
        public  ICollection<StoreProduct> StoreProducts { get; set; }
       
        public  ICollection<View> Views { get; set; }
    }
}
