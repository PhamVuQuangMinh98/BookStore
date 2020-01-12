using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        public ActionResult Index()
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK != "Admin")
                {
                    return View("../Home/Index");
                }
                else
                {
                    return View("Index");
                }
            }
        }
        public ActionResult Reset()
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK != "Admin")
                {
                    return View("../Home/Index");
                }
                else
                {
                    Session["ListKH"] = null;
                    Session["ListBlockKH"] = null;
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult SearchGioiTinh(string gioiTinh)
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK != "Admin")
                {
                    return View("../Home/Index");
                }
                else
                {
                    ToyContext db = new ToyContext();
                    List<KhachHang> list = Session["ListKH"] as List<KhachHang>;
                    if (gioiTinh == "nam")
                    {
                        Session["ListKH"] = list.Where(i => i.GioiTinh == true).ToList();
                    }
                    else if (gioiTinh == "nu")
                    {
                        Session["ListKH"] = list.Where(i => i.GioiTinh == false).ToList();
                    }
                    else
                    {
                        Session["ListKH"] = list;
                    }
                    return View("Index");
                }
            }
        }
        public ActionResult Block(long idKhachHang)
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK != "Admin")
                {
                    return View("../Home/Index");
                }
                else
                {
                    var dao = new KhachHangDAO();
                    Session["Update"] = null;
                    dao.BlockKH(idKhachHang);
                    return View("Index");
                }
            }
        }
        public ActionResult Unblock(long idKhachHang)
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK != "Admin")
                {
                    return View("../Home/Index");
                }
                else
                {
                    var dao = new KhachHangDAO();
                    dao.UnblockKH(idKhachHang);
                    return View("Index");
                }
            }
        }
        public ActionResult Update(long idKhachHang)
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK != "Admin")
                {
                    return View("../Home/Index");
                }
                else
                {
                    var dao = new KhachHangDAO();
                    Session["Update"] = dao.Find(idKhachHang);
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Search(string stt, string email, string gioiTinh, DateTime? ngaySinh)
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK != "Admin")
                {
                    return View("../Home/Index");
                }
                else
                {
                    var dao = new KhachHangDAO();
                    if (stt == "block")
                    {
                        Session["ListBlockKH"] = dao.SearchBlock(email, gioiTinh, ngaySinh);
                    }
                    else
                    {
                        Session["ListKH"] = dao.Search(email, gioiTinh, ngaySinh);
                    }
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(KhachHang kh)
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK != "Admin")
                {
                    return View("../Home/Index");
                }
                else
                {
                    if (Session["Update"] == null)
                    {
                        var dao = new KhachHangDAO();
                        var result = dao.FindTK(kh.Username);
                        if (result != null)
                        {
                            ModelState.AddModelError("TK", "Tài khoản đã tồn tại");
                            return View("Index", kh);
                        }
                        else
                        {
                            dao.InsertKH(kh.Username, kh.Ho, kh.Ten, kh.NgaySinh, kh.GioiTinh);
                            Session["ListBlockAd"] = null;
                            Session["ListAd"] = null;
                            View("Index");
                            return Redirect(Request.UrlReferrer.ToString());
                        }

                    }
                    else
                    {

                        var dao = new KhachHangDAO();
                        var kh1 = Session["Update"] as KhachHang;
                        dao.UpdateKH(kh1.IdKhachHang, kh);
                        Session["Update"] = null;
                        Session["ListBlockAd"] = null;
                        Session["ListAd"] = null;
                        return RedirectToAction("Reset", "KhachHang", new { area = "Admin" });
                    }
                }
            }
            
          
        }
    }
}