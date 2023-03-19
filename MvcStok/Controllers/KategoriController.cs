using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities1 db= new MvcDbStokEntities1();
        public ActionResult Index(int sayfa=1)
        {
            //var degerler=db.Tbl_Kategoriler.ToList();
            var degerler = db.Tbl_Kategoriler.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniKategori() 
        { 
            return  View();
        }
        [HttpPost]
        public ActionResult YeniKategori(Tbl_Kategoriler _Kategoriler) 
        {
            if(!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.Tbl_Kategoriler.Add(_Kategoriler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var kategori=db.Tbl_Kategoriler.Find(id);
           db.Tbl_Kategoriler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.Tbl_Kategoriler.Find(id);
            return View("KategoriGetir",ktgr);
        }
        public ActionResult Güncelle(Tbl_Kategoriler _Kategoriler)
        {
            var ktg = db.Tbl_Kategoriler.Find(_Kategoriler.KATEGORIID);
            ktg.KATEGORIAD=_Kategoriler.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");   
        }
    }
}