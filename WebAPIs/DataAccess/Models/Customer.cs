using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Bạn cần nhập họ tên")]
        [Display(Name = "Họ & tên")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? DoB { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Bạn cần nhập Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại")]
        [Display(Name = "Số điện thoại")]
        [Column(TypeName = "varchar(15)"), MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        [StringLength(200)]
        [Column(TypeName = "nvarchar(200)"), MaxLength(200)]
        public string Address { get; set; }

        [Display(Name = "Mật khẩu")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập lại mật khẩu")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        public ICollection<Order> Orders { get; set; }

    }
}
