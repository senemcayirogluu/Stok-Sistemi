using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class GirisController : Controller
    {
        DBStokMVCEntities db = new DBStokMVCEntities();
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(TblAdmin t)
        {
            var bilgiler = db.TblAdmin.FirstOrDefault(x => x.kullanici == t.kullanici && x.sifre == t.sifre);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.kullanici, false);
                return RedirectToAction("Index","Musteri");
            }
            else
            {
                return View();
            }
        }
    }
}