using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OrderesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Orderes
        public ActionResult Index()
        {
            var Orderes = db.Orders.Include(o => o.Customer);
            return View(Orderes.ToList());
        }

        // GET: Orderes/Details/5
        public ActionResult Details(int? id)
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

        // GET: Orderes/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName");
            return View();
        }

        // POST: Orderes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrderDate,CustomerID,ReceiverPhoneNum,ReceiverAddress,TotalMoney,TotalMoney,TotalTax,TotalDiscount,State")] Order Order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(Order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName", Order.CustomerID);
            return View(Order);
        }

        // GET: Orderes/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName", Order.CustomerID);
            return View(Order);
        }

        // POST: Orderes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrderDate,CustomerID,ReceiverPhoneNum,ReceiverAddress,TotalMoney,TotalMoney,TotalTax,TotalDiscount,State")] Order Order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FullName", Order.CustomerID);
            return View(Order);
        }

        // GET: Orderes/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Orderes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order Order = db.Orders.Find(id);
            db.Orders.Remove(Order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
