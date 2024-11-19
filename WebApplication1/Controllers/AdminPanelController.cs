using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminPanelController : Controller
    {
        private Model1 database = new Model1();
        // GET: AdminPanel
        public ActionResult Index(int? month)
        {
            // Get the total number of orders
            int totalOrders = database.Orders.Count();
            // Count orders by status
            int waitingForConfirmationOrders = database.Orders.Count(o => o.OrderStatus == "Waiting for confirmation");
            int onDeliveryOrders = database.Orders.Count(o => o.OrderStatus == "On delivery");
            int completedOrders = database.Orders.Count(o => o.OrderStatus == "Completed");
            decimal totalSales = database.Orders.Where(o => o.OrderStatus == "Completed").Select(o => (decimal?)o.TotalMoney).DefaultIfEmpty(0M).Sum().Value;
            ViewBag.TotalSales = totalSales;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.WaitingForConfirmationOrders = waitingForConfirmationOrders;
            ViewBag.OnDeliveryOrders = onDeliveryOrders;
            ViewBag.CompletedOrders = completedOrders;

            return View();
        }
        public ActionResult ProductsManagement()
        {
            return View();
        }
        public ActionResult CategoryManagement()
        {
            return View();

        }
        public ActionResult CustomersManagement()
        {
            return View();
        }
        public ActionResult OrdersManagement()
        {
            return View();

        }
    }
}