using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Controllers
{
    public class LichSuController : Controller
    {
        // GET: LichSu
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../Login/Index");
            }
            else
            {
                var tk = Session["TaiKhoan"] as KhachHang;
                var hd = new HoaDonDAO();
                Session["ListHoaDonHome"] = hd.FindHDHome(tk.IdKhachHang);
                return View("Index");
            }
        }
    }
}