using WebApplication1.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.ThongKe
{
    public class ChiTiet
    {
        public ChiTiet(int idsp, int tongSo, decimal tongThu, string tenSP)
        {
            IDSP = idsp;
            TongSo = tongSo;
            TongThu = tongThu;
            TenSP = tenSP;
        }

        public int IDSP { get; set; }
        public string TenSP { get; set; }
        public int TongSo { get; set; }
        public string DonViTinh { get; set; }
        public decimal TongThu { get; set; }
    }

    public class DanhSachChiTiet
    {
        public DanhSachChiTiet() { }
        public DanhSachChiTiet(int tongThu, List<ChiTiet> chiTiet)
        {
            TongThu = tongThu;
            ChiTiet = chiTiet;
        }

        public int TongThu { get; set; }
        public List<ChiTiet> ChiTiet { get; set; } = new List<ChiTiet>();
    }

    public class ThongKeNgay
    {
        public ThongKeNgay() { }
        public ThongKeNgay(double tongDoanhThu, int tongKhacHang, int tongDatHang)
        {
            TongDoanhThu = tongDoanhThu;
            TongKhacHang = tongKhacHang;
            TongDatHang = tongDatHang;

            ChiTietSanPham = new DanhSachChiTiet();
        }

        public double TongDoanhThu { get; set; }
        public int TongKhacHang { get; set; }
        public int TongDatHang { get; set; }

        public DanhSachChiTiet ChiTietSanPham { get; set; } = new DanhSachChiTiet();

        public static ThongKeNgay UseDB(Model1 db)
        {
            var self = new ThongKeNgay();

            db.Orders.Where(x => x.OrderDate.Date == DateTime.Now.Date).ForEach(item =>
            {
                self.TongDoanhThu += item.TotalMoney;
                self.TongKhacHang += 1;
                db.OrderDetails.Where(x => x.OrderID == item.OrderID)
                    .ForEach(x =>
                    {
                        self.TongDatHang += 1;
                        self.ChiTietSanPham.TongThu += (int)x.Total;
                        var isSet = self.ChiTietSanPham.ChiTiet.Where(
                            sp => sp.IDSP == x.ProductID).FirstOrDefault().IfNotNull(sp =>
                            {
                                sp.TongSo += x.ProductQuantity;
                                sp.TongThu += (int)x.Total;
                                return true;
                            }, false);
                        if (!isSet)
                        {
                            db.Products.Where(
                                sp => sp.ProductID == x.ProductID).FirstOrDefault().IfNotNull(sp =>
                                {
                                    self.ChiTietSanPham.ChiTiet.Add(
                                        new ChiTiet((int)x.ProductID, x.ProductQuantity, x.Total, sp.ProductName));
                                });
                        }
                    });
            });
            return self;
        }
    }

    public class ThongKeThang
    {
        public ThongKeThang() { }
        public ThongKeThang(double tongDoanhThu, int tongKhacHang, int tongDatHang)
        {
            TongDoanhThu = tongDoanhThu;
            TongKhacHang = tongKhacHang;
            TongDatHang = tongDatHang;

            ChiTietSanPham = new DanhSachChiTiet();
        }

        public double TongDoanhThu { get; set; }
        public int TongKhacHang { get; set; }
        public int TongDatHang { get; set; }

        public DanhSachChiTiet ChiTietSanPham { get; set; } = new DanhSachChiTiet();

        public static ThongKeThang UseDB(Model1 db, int? monthGet = null, int? yearGet = null)
        {
            var self = new ThongKeThang();

            int month = DateTime.Now.Month;
            if (monthGet.HasValue)
            {
                month = monthGet.Value;
            }

            int year = DateTime.Now.Year;
            if (yearGet.HasValue)
            {
                year = yearGet.Value;
            }

            db.Orders.Where(x => x.OrderDate.Month == month && x.OrderDate.Year == year).ForEach(item =>
            {
                self.TongDoanhThu += item.TotalMoney;
                self.TongKhacHang += 1;
                db.OrderDetails.Where(x => x.OrderID == item.OrderID)
                    .ForEach(x =>
                    {
                        self.TongDatHang += 1;
                        self.ChiTietSanPham.TongThu += (int)x.Total;
                        var isSet = self.ChiTietSanPham.ChiTiet.Where(
                            sp => sp.IDSP == x.ProductID).FirstOrDefault().IfNotNull(sp =>
                            {
                                sp.TongSo += x.ProductQuantity;
                                sp.TongThu += (int)x.Total;
                                return true;
                            }, false);
                        if (!isSet)
                        {
                            db.Products.Where(
                                sp => sp.ProductID == x.ProductID).FirstOrDefault().IfNotNull(sp =>
                                {
                                    self.ChiTietSanPham.ChiTiet.Add(
                                        new ChiTiet((int)x.ProductID, x.ProductQuantity, x.Total, sp.ProductName));
                                });
                        }
                    });
            });
            return self;
        }
    }

    public class ThongKeNam
    {
        public ThongKeNam() { }
        public ThongKeNam(double tongDoanhThu, int tongKhacHang, int tongDatHang)
        {
            TongDoanhThu = tongDoanhThu;
            TongKhacHang = tongKhacHang;
            TongDatHang = tongDatHang;

            ChiTietSanPham = new DanhSachChiTiet();
        }

        public double TongDoanhThu { get; set; }
        public int TongKhacHang { get; set; }
        public int TongDatHang { get; set; }

        public DanhSachChiTiet ChiTietSanPham { get; set; } = new DanhSachChiTiet();

        public static ThongKeNam UseDB(Model1 db, int? yearGet = null)
        {
            var self = new ThongKeNam();

            int year = DateTime.Now.Year;
            if (yearGet.HasValue)
            {
                year = yearGet.Value;
            }

            db.Orders.Where(x => x.OrderDate.Year == year).ForEach(item =>
            {
                self.TongDoanhThu += item.TotalMoney;
                self.TongKhacHang += 1;
                db.OrderDetails.Where(x => x.OrderID == item.OrderID)
                    .ForEach(x =>
                    {
                        self.TongDatHang += 1;
                        self.ChiTietSanPham.TongThu += (int)x.Total;
                        var isSet = self.ChiTietSanPham.ChiTiet.Where(
                            sp => sp.IDSP == x.ProductID).FirstOrDefault().IfNotNull(sp =>
                            {
                                sp.TongSo += x.ProductQuantity;
                                sp.TongThu += x.Total;
                                return true;
                            }, false);
                        if (!isSet)
                        {
                            db.Products.Where(
                                sp => sp.ProductID == x.ProductID).FirstOrDefault().IfNotNull(sp =>
                                {
                                    self.ChiTietSanPham.ChiTiet.Add(
                                        new ChiTiet(x.ProductID, x.ProductQuantity, x.Total, sp.ProductName));
                                });
                        }
                    });
            });
            return self;
        }
    }
}