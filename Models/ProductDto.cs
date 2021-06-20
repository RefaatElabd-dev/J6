using J6.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace J6.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Price { get; set; }
        public int? SoldQuantities { get; set; }
        public int? Quantity { get; set; }
        public Size Size { get; set; }
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
        public virtual Promotion Promotion { get; set; }
        public string material { set; get; }
        public string Manufacture { get; set; }


        //shaban
        // public string BrandName { get; set; }
        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryDto> Gallery { get; set; }




        public SubCategory Subcategory { get; set; }
        public Brand Brand { get; set; }
        public ICollection<ProdCart> ProdCarts { get; set; }
        public  ICollection<ProdOrder> ProdOrders { get; set; }
    
        public  ICollection<Review> Reviews { get; set; }
     
        public  ICollection<StoreProduct> StoreProducts { get; set; }
       
        public  ICollection<View> Views { get; set; }
        //shaban
        public ICollection<ProductImage> ProductImages { get; set; }

    }
}
