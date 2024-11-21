namespace WebApplication1.Models
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

        public int OrderID { get; set; }

        public int OrderQuantity { get; set; }

        public decimal TotalMoney { get; set; }

        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderStatus { get; set; }

        [Required]
        [StringLength(100)]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(15)]
        public string ReceiverPhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string ReceiverAddress { get; set; }

        public int CustomerID { get; set; }

        public int? EmployeeID { get; set; }

        public int? CouponID { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
