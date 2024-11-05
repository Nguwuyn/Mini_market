
﻿using WebApplication1.Models;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly DAPMEntities db = new DAPMEntities();

        // GET: LoginUser
        public ActionResult Index()
        {
            var Id = Session["UserId"];
            if (Id != null)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Customer _user)
        {
            var check = db.Customers.Where(s => s.Username == _user.Username && s.CusPassword == _user.CusPassword).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "Thông tin đăng nhập bị sai. Vui lòng kiểm tra";
                return View("Index");
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["UserId"] = check.CustomerID;
                Session["FullName"] = check.FullName;
                Session["IsAdmin"] = check.Username == "Admin";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult RegisterUser()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult RegisterUser(Customer _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Customers.Where(s => s.CustomerID == _user.CustomerID || s.Username == _user.Username).FirstOrDefault();
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Customers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "Người dùng này đã tồn tại trước đó.";
                    return View();
                }
            }
            return View();
        }

        public ActionResult LogoutUser()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}