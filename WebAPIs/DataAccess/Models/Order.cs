using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
    public enum OrderStatus
    {
        [Display(Name = "Mới đặt")]
        CurOrder = 1,
        [Display(Name = "Đang giao")]
        Delivering = 2,
        [Display(Name = "Đã giao")]
        Delivered = 3
    }
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int CusId { get; set; }

        [Display(Name = "Ngày đặt")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Bạn cần chọn ngày đặt")]
        public DateTime OrderDate { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Bạn cần nhập giá")]
        [Display(Name = "Tổng tiền")]
        public double Total { get; set; }

        [Display(Name = "Trạng thái")]
        public OrderStatus OrderStatus { get; set; }

        [StringLength(250)]
        [Display(Name = "Ghi chú")]
        [Column(TypeName = "nvarchar(250)")]
        public string Notes { get; set; }

        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
