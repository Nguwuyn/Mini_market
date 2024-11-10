namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PromotionDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PromotionID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateEnd { get; set; }

        public virtual Product Product { get; set; }

        public virtual Promotion Promotion { get; set; }
    }
}
