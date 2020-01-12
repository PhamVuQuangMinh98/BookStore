using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Controllers
{
    public class ChiTietSachController : Controller
    {
        public static List<Sach> listSach;
        // GET: ChiTietSach
        public ActionResult Index(long id)
        {
            ToyContext db = new ToyContext();
            Sach sach = db.Sachs.Find(id);
            sach.LuotXem++;
            db.SaveChanges();
            Session["CTSach"]= sach;
            return View("Index");
        }
        public ActionResult GoToLogin()
        {
            return RedirectToAction("Index", "Login");
        }
        public ActionResult ThemGioHang(long idSach)
        {
            var db = new HoaDonDAO();
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                KhachHang kh = Session["TaiKhoan"] as KhachHang;
                HoaDon hd = db.FindHD(kh.IdKhachHang);
                var CTHDDAO = new CTHDDAO();
                if (hd == null)
                {
                    HoaDon newHD = db.InsertHD(kh.IdKhachHang, DateTime.Today, "Chưa Thanh Toán", "Chưa Giao Hàng");
                    CTHDDAO.InsertCTHD(newHD.IdHoaDon, idSach);
                    Session["ListCTHD"] = CTHDDAO.GetList(newHD.IdHoaDon);
                    ViewBag.Them = "Thêm vào giỏ hàng thành công";
                    return View("Index");                    
                }
                else
                {
                    int pb = 0;
                    int rs;
                    foreach (var item in CTHDDAO.GetList(hd.IdHoaDon))
                    {
                        if (item.IdSach == idSach)
                        {
                            rs= CTHDDAO.UpdateCTHD(hd.IdHoaDon, idSach);
                            if (rs == 1)
                            {
                                ViewBag.SL = "Sách bạn muốn thêm giỏ hàng đã đạt giới hạn";
                                Session["ListCTHD"] = CTHDDAO.GetList(hd.IdHoaDon);
                                return View("Index");
                            }
                            else if (rs == 2)
                            {
                                ViewBag.SL = "Sách tồn kho không đáp ứng được nhu cầu của bạn";
                                Session["ListCTHD"] = CTHDDAO.GetList(hd.IdHoaDon);
                                return View("Index");
                            }
                            else
                            {
                                pb = 1;
                                break;
                            }
                        }
                    }
                    if (pb == 0)
                    {
                        CTHDDAO.InsertCTHD(hd.IdHoaDon, idSach);
                    }
                    Session["ListCTHD"] = CTHDDAO.GetList(hd.IdHoaDon);
                    ViewBag.Them = "Thêm vào giỏ hàng thành công";
                    return View("Index");
                }
            }
        }
    }
}