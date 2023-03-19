using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities1 db=new MvcDbStokEntities1();
        public ActionResult Index(string p)
        {
            var degerler = from d in db.Tbl_Müşteriler select d;
            if(!string.IsNullOrEmpty(p))
            {
                degerler=degerler.Where(m=>m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());
            //var degerler = db.Tbl_Müşteriler.ToList();
            //return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(Tbl_Müşteriler _Müşteriler)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Tbl_Müşteriler.Add(_Müşteriler); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var musteri = db.Tbl_Müşteriler.Find(id);
            db.Tbl_Müşteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MüşteriGetir(int id) 
        {
            var mus = db.Tbl_Müşteriler.Find(id);
            return View("MüşteriGetir",mus);
        }
        public ActionResult Güncelle(Tbl_Müşteriler _Müşteriler)
        {
            var musteri = db.Tbl_Müşteriler.Find(_Müşteriler.MUSTERIID);
            musteri.MUSTERIAD = _Müşteriler.MUSTERIAD;
            musteri.MUSTERISOYAD = _Müşteriler.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}