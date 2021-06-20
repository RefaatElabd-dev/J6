using J6.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace J6.DAL.ViewModels
{
    public class ProductsViewModel
    {

        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public int? SoldQuantities { get; set; }
        public int? Quantity { get; set; }

        public IFormFile Image { get; set; }
        public string Color { get; set; }
        public Size Size { get; set; }
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

        public int? SellerId { get; set; }
        [ForeignKey("SellerId")]
        public virtual AppUser Seller { get; set; }


        [ForeignKey("Brands")]
        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [ForeignKey("Promotion")]
        public int? PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }

        [ForeignKey("Subcategory")]
        public int? SubcategoryId { get; set; }
        public virtual SubCategory Subcategory { get; set; }

        public string Manufacture { get; set; }
        public virtual ICollection<ProdCart> ProdCarts { get; set; }
        public virtual ICollection<ProdOrder> ProdOrders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<View> Views { get; set; }
        public virtual ICollection<MiddleSavedProduct> ProductsBag { get; set; }

    }
}
