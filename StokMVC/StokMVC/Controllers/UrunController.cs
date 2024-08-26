using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class UrunController : Controller
    {
        DBStokMVCEntities db = new DBStokMVCEntities();
        public ActionResult Index(string p)
        {
            var urunler = db.TblUrunler.Where(x => x.durum == true);
            if(!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum == true);
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktgr = (from x in db.TblKategori.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.drop = ktgr;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(TblUrunler u)
        {
            var ktg = db.TblKategori.Where(x => x.id == u.TblKategori.id).FirstOrDefault();
            u.TblKategori = ktg;
            db.TblUrunler.Add(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kat = (from x in db.TblKategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.urunkategori = kat;
            var urun = db.TblUrunler.Find(id);
            return View("UrunGetir", urun);
        }
        public ActionResult UrunGuncelle(TblUrunler p)
        {
            var urun = db.TblUrunler.Find(p.id);
            urun.ad = p.ad;
            urun.marka = p.marka;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.satisfiyat = p.satisfiyat;
            var ktr = db.TblKategori.Where(x => x.id == p.TblKategori.id).FirstOrDefault();
            urun.kategori = ktr.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(TblUrunler p1)
        {
            var urunbul = db.TblUrunler.Find(p1.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}