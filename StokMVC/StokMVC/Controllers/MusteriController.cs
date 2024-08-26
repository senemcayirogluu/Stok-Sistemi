using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace StokMVC.Controllers
{
    public class MusteriController : Controller
    {
        DBStokMVCEntities db = new DBStokMVCEntities();
        [Authorize]
        public ActionResult Index(int sayfa=1)
        {
            var musteriliste = db.TblMusteri.Where(x => x.durum == true).ToList().ToPagedList(sayfa, 3);
            return View(musteriliste);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(TblMusteri m)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            m.durum = true;
            db.TblMusteri.Add(m);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriSil(TblMusteri p)
        {
            var musteribul = db.TblMusteri.Find(p.id);
            musteribul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mt = db.TblMusteri.Find(id);
            return View("MusteriGetir", mt);
        }
        public ActionResult MusteriGuncelle(TblMusteri k)
        {
            var mst = db.TblMusteri.Find(k.id);
            mst.ad = k.ad;
            mst.soyad = k.soyad;
            mst.sehir = k.sehir;
            mst.bakiye = k.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}