namespace DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public enum Category
    {
        [Display(Name = "Món ăn")]
        Food = 1,
        [Display(Name = "Combo")]
        Combo = 2,
        [Display(Name = "Nước")]
        Drink = 3
    }
    public partial class Dish
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Dish()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int DishId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên món")]
        [StringLength(250)]
        [Display(Name = "Tên món")]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Display(Name = "Đơn giá")]
        public decimal Price { get; set; }

        [Display(Name = "Phân loại")]
        public Category Category { get; set; }

        [StringLength(200)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [NotMapped]
        [Display(Name = "Chọn hình")]
        public HttpPostedFileBase ImageFile { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
