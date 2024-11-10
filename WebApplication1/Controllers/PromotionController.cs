using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class PromotionController : Controller
    {
        // GET: Promotion
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail() { return View(); }
        public ActionResult Create() { return View(); }
        public ActionResult Edit() { return View(); }
        public ActionResult Delete() { return View(); }
    }
}