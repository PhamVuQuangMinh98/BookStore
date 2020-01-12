using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session["KM"] = null;
            return View("Index");
            
        }
        [HttpPost]
        public ActionResult TimKiemSach(string tenSach)
        {
            Session["KM"] = null;
            ToyContext db = new ToyContext();
            Session["Title"] = "Tìm Kiếm: " + tenSach;
            Session["Products"] = db.Sachs.Where(i => i.flag == true && i.Ten.Contains(tenSach)).ToList();
            return View("../DanhSachSanPham/Index");
        }
        public ActionResult TatCa()
        {
            Session["KM"] = null;
            var db = new HomeDAO();
            Session["Title"] = "Tất Cả Sách";
            Session["Products"] = db.GetList();
            return View("../DanhSachSanPham/Index");
        }
        public ActionResult SachMoi()
        {
            Session["KM"] = null;
            var db = new HomeDAO();
            Session["Title"] = "Sách Mới";
            Session["Products"] = db.GetListSachMoiAll();
            return View("../DanhSachSanPham/Index");
        }
        public ActionResult Noibat()
        {
            Session["KM"] = null;
            var db = new HomeDAO();
            Session["Title"] = "Nổi Bật";
            Session["Products"]=db.GetListNoiBatAll();
            return View("../DanhSachSanPham/Index");
        }
        public ActionResult XemNhieu()
        {
            Session["KM"] = null;
            var db = new HomeDAO();
            Session["Title"] = "Xem nhiều";
            Session["Products"] = db.GetListXemNhieuAll();
            return View("../DanhSachSanPham/Index");
        }
    }
}