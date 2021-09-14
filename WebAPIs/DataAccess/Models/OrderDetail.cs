using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }

        public int OrderId { get; set; }
        public int DishId { get; set; }

        [Required, Range(0, int.MaxValue, ErrorMessage = "Bạn cần nhập số lượng")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Bạn cần nhập thành tiền")]
        [Display(Name = "Thành tiền")]
        public double TotalPrice { get; set; }

        [StringLength(250)]
        [Column(TypeName = "Nvarchar(250)")]
        [Display(Name = "Ghi chú")]
        public string Notes { get; set; }

        //public Order Order { get; set; }
        public Dish Dish { get; set; }
    }
}
