﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DAPMEntities : DbContext
    {
        public DAPMEntities()
            : base("name=DAPMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CTKM> CTKMs { get; set; }
        public virtual DbSet<Danh_mục> Danh_mục { get; set; }
        public virtual DbSet<Đơn_mua_hàng> Đơn_mua_hàng { get; set; }
        public virtual DbSet<Khách_hàng> Khách_hàng { get; set; }
        public virtual DbSet<Mã_giảm_giá> Mã_giảm_giá { get; set; }
        public virtual DbSet<Nhân_viên> Nhân_viên { get; set; }
        public virtual DbSet<Sản_phẩm> Sản_phẩm { get; set; }
        public virtual DbSet<Chi_tiết_đơn_hàng> Chi_tiết_đơn_hàng { get; set; }
    }
}
