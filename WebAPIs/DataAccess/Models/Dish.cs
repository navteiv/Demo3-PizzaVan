using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
    public enum Category
    {
        [Display(Name = "Món")]
        Food = 1,
        [Display(Name = "Combo")]
        Combo = 2,
        [Display(Name = "Nước")]
        Drink = 3
    }
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Bạn cần nhập tên món ăn")]
        [Display(Name = "Tên món")]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập giá")]
        [Range(0, double.MaxValue, ErrorMessage = "Vui lòng nhập đúng")]
        [Display(Name = "Giá")]
        [Column(TypeName = "money")]
        public double Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Vui lòng chọn phân loại")]
        [Display(Name = "Phân loại")]
        public Category Category { get; set; }

        [StringLength(200)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [NotMapped]
        [Display(Name = "Chọn hình")]
        public IFormFile ImageFile { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }

        [Display(Name = "Đang phục vụ")]
        public bool Status { get; set; }

        public IList<OrderDetail> OrderDetails { get; set; }
    }
}
