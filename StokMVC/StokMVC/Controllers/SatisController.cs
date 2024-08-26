using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokMVC.Models.Entity;

namespace StokMVC.Controllers
{
    public class SatisController : Controller
    {
        DBStokMVCEntities db = new DBStokMVCEntities();
        public ActionResult Index()
        {
            var satislar = db.TblSatislar.ToList(); 
            return View(satislar);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urun = (from x in db.TblUrunler.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad,
                                             Value = x.id.ToString()
                                         }).ToList();
            ViewBag.drop1 = urun;

            List<SelectListItem> personel = (from x in db.TblPersonel.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.ad +" "+ x.soyad,
                                                Value = x.id.ToString()
                                            }).ToList();

            ViewBag.drop2 = personel;

            List<SelectListItem> musteri = (from x in db.TblMusteri.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.ad +" "+ x.soyad,
                                             Value = x.id.ToString()
                                         }).ToList();

            ViewBag.drop3 = musteri;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(TblSatislar s)
        {
            var urun = db.TblUrunler.Where(x => x.id == s.TblUrunler.id).FirstOrDefault();
            var personel = db.TblPersonel.Where(x => x.id == s.TblPersonel.id).FirstOrDefault();
            var musteri = db.TblMusteri.Where(x => x.id == s.TblMusteri.id).FirstOrDefault();
            s.TblUrunler = urun;
            s.TblPersonel = personel;
            s.TblMusteri = musteri;
            s.tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TblSatislar.Add(s);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}