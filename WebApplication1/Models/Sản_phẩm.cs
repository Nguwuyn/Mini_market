//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sản_phẩm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sản_phẩm()
        {
            this.Chi_tiết_kiểm_kho = new HashSet<Chi_tiết_kiểm_kho>();
            this.Chi_tiết_đơn_hàng = new HashSet<Chi_tiết_đơn_hàng>();
            this.CTKMs = new HashSet<CTKM>();
        }
    
        public string ID_sản_phẩm { get; set; }
        public decimal Giá_tiền { get; set; }
        public int Số_lượng_tồn { get; set; }
        public double Thuế { get; set; }
        public string Hãng { get; set; }
        public string Tên_sản_phẩm { get; set; }
        public string Danh_mục { get; set; }
        public short Đánh_giá { get; set; }
        public string Hình_ảnh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chi_tiết_kiểm_kho> Chi_tiết_kiểm_kho { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chi_tiết_đơn_hàng> Chi_tiết_đơn_hàng { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTKM> CTKMs { get; set; }
    }
}
