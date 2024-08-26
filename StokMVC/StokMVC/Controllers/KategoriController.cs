using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class KategoriController : Controller
    {
        DBStokMVCEntities db = new DBStokMVCEntities();
        public ActionResult Index()
        {
            var kategoriler = db.TblKategori.ToList();
            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(TblKategori p)
        {
            db.TblKategori.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var kg = db.TblKategori.Find(id);
            db.TblKategori.Remove(kg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var kt = db.TblKategori.Find(id);
            return View("KategoriGetir", kt);
        }
        public ActionResult KategoriGuncelle(TblKategori k)
        {
            var kr = db.TblKategori.Find(k.id);
            kr.ad = k.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}