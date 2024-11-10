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

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        public int OrderQuantity { get; set; }

        public int TotalMoney { get; set; }

        [Column(TypeName = "date")]
        public DateTime OrderDate { get; set; }

        [StringLength(2)]
        public string OrderStatus { get; set; }

        [StringLength(50)]
        public string ReceiverName { get; set; }

        [StringLength(10)]
        public string ReceiverPhoneNum { get; set; }

        [StringLength(200)]
        public string ReceiverAddress { get; set; }

        public int CustomerID { get; set; }

        public int EmployeeID { get; set; }

        public int? CouponID { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
