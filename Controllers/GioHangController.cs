using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Controllers
{
    public class GioHangController : Controller
    {
        public static long idHD;
        // GET: GioHang
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var db =new HoaDonDAO();
                KhachHang kh = Session["TaiKhoan"] as KhachHang;
                HoaDon hd = db.FindHD(kh.IdKhachHang);
                var CTHDDAO = new CTHDDAO();
                if (hd != null)
                {
                    idHD = hd.IdHoaDon;
                    Session["ListCTHD"] = CTHDDAO.GetList(hd.IdHoaDon);
                }
                return View("Index");
            }
        }
        public ActionResult DeleteCTHD(long idCTHD)
        {
            CTHDDAO db = new CTHDDAO();
            db.DeleteCTHD(idCTHD);
            ViewBag.GioHang = "Xóa thành công";
            Session["ListCTHD"] = db.GetList(idHD);
            return View("Index");
        }
        [HttpPost]
        public ActionResult UpdateSL(long idCTHD,int sl)
        {
            CTHDDAO db = new CTHDDAO();
            int rs = db.UpdateCTHD_GioHang(idCTHD, sl);
            if (rs == 1)
            {
                ViewBag.GioHang = "Số lượng chỉ <= 3";
            }
            else if (rs == 2)
            {
                ViewBag.GioHang = "Số lượng tồn kho không đủ cho nhu cầu của bạn";
            }
            else
            {
                ViewBag.GioHang = "Cập nhật thành công";
            }
            Session["ListCTHD"] = db.GetList(idHD);
            return View("Index");
        }
    }
}