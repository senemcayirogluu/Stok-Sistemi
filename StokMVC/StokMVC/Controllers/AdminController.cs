using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class AdminController : Controller
    {
        DBStokMVCEntities db = new DBStokMVCEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TblAdmin a)
        {
            db.TblAdmin.Add(a);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}