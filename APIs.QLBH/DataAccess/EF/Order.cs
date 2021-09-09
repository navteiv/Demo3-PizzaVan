namespace DataAccess.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }

        public int CusId { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày đặt")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Tổng tiền")]
        public double Total { get; set; }

        [Display(Name = "Trạng thái đơn hàng")]
        public int OrderStatus { get; set; }

        [StringLength(250)]
        [Display(Name = "Ghi chú")]
        public string Notes { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
