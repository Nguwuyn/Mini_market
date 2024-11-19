
﻿using WebApplication1.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.CodeDom;
namespace WebApplication1.Controllers

{
    public class ProductsController : Controller
    {
        private readonly Model1 db = new Model1();

        public PartialViewResult PartialProduct(Product product)
        {
            return PartialView(product);
        }

        public PartialViewResult PartialOrdersProduct(OrderDetail product)
        {
            return PartialView(product);
        }

        // GET: Products
        public ActionResult Index(int page = 1)
        {
            if (ControllerContext.IsChildAction)
            {
                return PartialView(db.Products.ToList());
            }
            int maxPage = Math.Max(1, db.Products.Count() / 10);
            if (page > maxPage)
            {
                page = maxPage;
            }
            ViewBag.MaxPage = maxPage;
            ViewBag.CurrentPage = page;
            return View("Index", db.Products.OrderBy(x => x.ProductName).Skip((page - 1) * 15).Take(15).ToList());
        }

        public ActionResult Details(int? id, int? someOtherParameter)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View(new Product());
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
           if (product.ProductImg == null)
            {
                product.ProductImg = "~/Image/Product/CuonTuiRac.jpg";
            }

            if (product.StockQuantity == 0)
            {
                product.StockQuantity = 1000;
            }

            if (product.UploadImage != null)
            {
                product.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Image/Product/"), product.ProductImg.Split('/').Last()));
            }
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
           if (product.UploadImage != null)
            {
                product.UploadImage.SaveAs(Path.Combine(Server.MapPath("~/Image/Product/"), product.ProductImg.Split('/').Last()));
            }

            if (product.OldProductImg != null && product.ProductImg != product.OldProductImg)
            {
                var oldfile = Path.Combine(Server.MapPath("~/Image/Product/"), product.OldProductImg.Split('/').Last());
                var file = Path.Combine(Server.MapPath("~/Image/Product/"), product.ProductImg.Split('/').Last());
                System.IO.File.Move(oldfile, file);
            }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = product.ProductID });
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(db.Products.Where(p => p.ProductID == product.ProductID).FirstOrDefault());
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UserIndex(int page = 1)
        {
            if(db.Products.Count() == 0)
            {
                return RedirectToAction("Empty");
            }    
            if (ControllerContext.IsChildAction)
            {
                return PartialView(db.Products.ToList());
            }
            int maxPage = Math.Max(1, db.Products.Count() / 10);
            if (page > maxPage)
            {
                page = maxPage;
            }
            ViewBag.MaxPage = maxPage;
            ViewBag.CurrentPage = page;
            return View("Index", db.Products.OrderBy(x => x.ProductName).Skip((page - 1) * 15).Take(15).ToList());
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
