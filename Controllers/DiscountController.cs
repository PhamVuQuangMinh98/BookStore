using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;

namespace WebApplication23.Controllers
{
    public class DiscountController : Controller
    {
        // GET: Discount
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult ChiTietKhuyenMai(long id)
        {
            var km = new KhuyenMaiDAO();
            Session["KM"] = km.Find(id);
            var sach = new SachDAO();
            Session["Title"] = km.Find(id).TenKhuyenMai;
            Session["Products"] = sach.GetListKM(id);
            return View("../DanhSachSanPham/Index");
        }
    }
}