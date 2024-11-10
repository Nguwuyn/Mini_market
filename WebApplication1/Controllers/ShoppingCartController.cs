
﻿using WebApplication1.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace WebApplication1.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        private readonly Model1 db = new Model1();
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
            {
                return View();
            }
            Cart cart = Session["Cart"] as Cart;

            int _id = (int?)Session["UserId"] ?? -1;
            if (_id > -1)
            {
                Customer _cus = db.Customers.FirstOrDefault(x => x.CustomerID == _id);
                TempData["Address"] = _cus.CusAddress;
                TempData["ReceiverPhoneNum"] = _cus.CusPhone;
            }
            return View(cart);
        }

        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        [HttpPost]
        public ActionResult AddToCart()
        {
            int id;
            using (StreamReader reader = new StreamReader(HttpContext.Request.InputStream))
            {
                if (!int.TryParse(reader.ReadLine(), out id))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            }

            var _pro = db.Products.SingleOrDefault(s => s.ProductID == id);
            if (_pro == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            if (GetCart().AddProductCart(_pro))
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.RemoveCartItem(id);
            return RedirectToAction("Index", "ShoppingCart");
        }

        public PartialViewResult BagCart()
        {
            int total_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)
                total_quantity_item = cart.TotalQuantity();

            ViewBag.QuantityCart = total_quantity_item;
            return PartialView("BagCart");
        }

        public ActionResult UpdateCartQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);
            int _quantity = int.Parse(form["cartQuantity"]);
            cart.UpdateQuantity(id_pro, _quantity);
            return RedirectToAction("Index", "ShoppingCart");
        }

        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                int _userId = (int)Session["UserId"];
                var _user = db.Customers.FirstOrDefault(x => x.CustomerID == _userId);

                if (cart.Items.Count() == 0)
                {
                    return RedirectToAction("Index");
                }

                if (form["ReceiverAddress"] == "")
                {
                    if (_user.CusAddress == null || _user.CusAddress == "")
                    {
                        TempData["Error"] = "Bạn cần phải nhập địa chỉ giao hàng";
                        return RedirectToAction("Index");
                    }
                    form["ReceiverAddress"] = _user.CusAddress;
                }

                if (form["ReceiverPhoneNum"] == "")
                {
                    if (_user.CusPhone == null || _user.CusPhone == "")
                    {
                        TempData["Error"] = "Bạn cần phải nhập số điện thoại để liên hệ khi giao hàng";
                        return RedirectToAction("Index");
                    }
                    form["ReceiverPhoneNum"] = _user.CusPhone;
                }

                if (form["CodeCustomer"] == null)
                {
                    form["CodeCustomer"] = _user.CustomerID.ToString();
                }

                if (_user.CusAddress == null)
                {
                    _user.CusAddress = form["ReceiverAddress"];
                    db.Entry<Customer>(_user).State = EntityState.Modified;
                }

                Order _order = new Order(); //Bang Hoa Don San pham

                _order.OrderDate = DateTime.Now;
                _order.ReceiverAddress = form["ReceiverAddress"];
                _order.ReceiverPhoneNum = form["ReceiverPhoneNum"];
                _order.CustomerID = int.Parse(form["CodeCustomer"]);
                _order.OrderStatus = "Đang xử lý";

                int totalPrice = 0;
                int totalQuantity = 0;

                foreach (var item in cart.Items)
                {
                    var prodTotal = (item._quantity * item._product.ProductPrice);
                    var tax = (int)(prodTotal * item._product.Tax);
                    OrderDetail _order_detail = new OrderDetail
                    {
                        ProductID = item._product.ProductID,
                        OrderID = _order.OrderID,
                        ProductQuantity = item._quantity,
                        ItemPrice = (int)item._product.ProductPrice,
                        Total = (int)(prodTotal + tax),
                    };

                    totalQuantity += item._quantity;
                    totalPrice += (int)_order_detail.Total;
                    db.OrderDetails.Add(_order_detail);

                    var _prod = db.Products.Find(item._product.ProductID);
                    _prod.StockQuantity = Math.Max((int)(_prod.StockQuantity - item._quantity), 0);
                    db.Entry(_prod).State = EntityState.Modified;
                }

                _order.TotalMoney = totalPrice;
                _order.OrderQuantity = totalQuantity;

                db.Orders.Add(_order);
                db.SaveChanges();
                cart.ClearCart();
                return View("CheckOutSuccess", _order);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult BuyNow(int id)
        {
            Cart cart = GetCart();
            var _pro = db.Products.SingleOrDefault(s => s.ProductID == id);
            if (_pro != null)
            {
                cart.AddProductCart(_pro);
                if (Session["UserId"] == null)
                {
                    TempData["Error"] = "Bạn cần phải đăng nhập trước khi thanh toán";
                    return RedirectToAction("Index");
                }
                int _userId = (int)Session["UserId"];
                var _user = db.Customers.FirstOrDefault(x => x.CustomerID == _userId);
                FormCollection form = new FormCollection();
                form["ReceiverAddress"] = _user.CusAddress;
                form["CodeCustomer"] = _user.CustomerID.ToString();
                return RedirectToAction("CheckOut", "ShoppingCart", form);
            }
            TempData["Error"] = "Sản phẩm không tồn tại hoặc đã bị xóa";
            return RedirectToAction("Index", "Products");
        }

        public ActionResult CheckOutSuccess(Order _order)
        {
            return View(_order);
        }

        public ActionResult CheckOrder(int? id)
        {
            if (id == null)
            {
                return View();
            }

            Order _order = db.Orders.Where(x => x.OrderID == id).FirstOrDefault();
            if (_order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(_order);
        }

        public ActionResult EditOrder(int? id)
        {
            if (id == null)
            {
                return View();
            }

            Order _order = db.Orders.Where(x => x.OrderID == id).FirstOrDefault();
            if (_order == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var l = new List<String>()
            {
                "Đang xử lý",
                "Đã xử lý",
                "Đang giao hàng",
                "Đã giao hàng",
                "Đang hủy",
                "Đã hủy",
            };

            ViewBag.State = l.Select(x => new SelectListItem { Text = x, Value = x, Selected = (x == _order.OrderStatus) }).ToList();
            return View(_order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder([Bind(Include = "OrderID,StockQuantity,TotalMoney,OrderDate,OrderStatus,ReceiverFullName,ReceiverPhoneNum,ReceiverAddress,CustomerID,EmployeeID,CouponID,FinalTotal")] Order Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CheckOrder", new { id = Order.OrderID });
            }

            var l = new List<String>()
            {
                "Đang xử lý",
                "Đã xử lý",
                "Đang giao hàng",
                "Đã giao hàng",
                "Đang hủy",
                "Đã hủy",
            };

            ViewBag.State = l.Select(x => new SelectListItem { Text = x, Value = x, Selected = (x == Order.OrderStatus) }).ToList();

            return View(Order);
        }

        public ActionResult DeleteOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        [HttpPost, ActionName("DeleteOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOrderConfirmed(int id)
        {
            Order Order = db.Orders.Find(id);
            db.OrderDetails.Where(x => x.OrderID == Order.OrderID).ForEach(x => db.OrderDetails.Remove(x));
            db.Orders.Remove(Order);
            db.SaveChanges();
            return RedirectToAction("Details", "Customers", new { id = Order.CustomerID });
        }

        public ActionResult CancelOrder(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        [HttpPost, ActionName("CancelOrder")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelOrderConfirmed(int id)
        {
            Order Order = db.Orders.Find(id);
            Order.OrderStatus = "Đang hủy";
            db.Entry(Order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CheckOrder", new { id = Order.OrderID });
        }
    }
}