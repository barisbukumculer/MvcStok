using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities1 db=new MvcDbStokEntities1();
        public ActionResult Index()
        {
            var degerler = db.Tbl_Ürünler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value=i.KATEGORIID.ToString(),
                                           }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Tbl_Ürünler _Ürünler)
        {
            var ktg=db.Tbl_Kategoriler.Where(m=>m.KATEGORIID==_Ürünler.Tbl_Kategoriler.KATEGORIID).FirstOrDefault();
            _Ürünler.Tbl_Kategoriler= ktg;
            db.Tbl_Ürünler.Add(_Ürünler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urun = db.Tbl_Ürünler.Find(id);
            db.Tbl_Ürünler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ÜrünGetir(int id) 
        {
            var urun = db.Tbl_Ürünler.Find(id);

            List<SelectListItem> degerler = (from i in db.Tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString(),
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("ÜrünGetir",urun);
        }
        public ActionResult Güncelle(Tbl_Ürünler _Ürünler)
        {
            var ürünler = db.Tbl_Ürünler.Find(_Ürünler.URUNID);
            ürünler.URUNAD = _Ürünler.URUNAD;
            //ürünler.URUNKATEGORI = _Ürünler.URUNKATEGORI;
            ürünler.STOK = _Ürünler.STOK;
            ürünler.MARKA = _Ürünler.MARKA;
            ürünler.FIYAT = _Ürünler.FIYAT;
            var ktg = db.Tbl_Kategoriler.Where(m => m.KATEGORIID == _Ürünler.Tbl_Kategoriler.KATEGORIID).FirstOrDefault();
            ürünler.URUNKATEGORI = ktg.KATEGORIID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}