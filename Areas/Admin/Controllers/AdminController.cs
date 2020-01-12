using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            var tk = Session["TaiKhoan"] as KhachHang;
            if (tk == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (tk.LoaiTK == "Admin")
                {
                    return View("Index");
                }
                else
                {
                    return View("../Home/Index");
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

                    Session["ListAd"] = null;
                    Session["ListBlockAd"] = null;
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
                    var dao = new AdminDAO();
                    dao.BlockKH(idKhachHang);
                    Session["UpdateAd"] = null;
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
                    var dao = new AdminDAO();
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
                    var dao = new AdminDAO();
                    Session["UpdateAd"] = dao.Find(idKhachHang);
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
                    var dao = new AdminDAO();
                    if (stt == "block")
                    {
                        Session["ListBlockAd"] = dao.SearchBlock(email, gioiTinh, ngaySinh);
                    }
                    else
                    {
                        Session["ListAd"] = dao.Search(email, gioiTinh, ngaySinh,tk.IdKhachHang);
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
                    if (Session["UpdateAd"] == null)
                    {
                        var dao = new AdminDAO();
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

                        var dao = new AdminDAO();
                        var kh1 = Session["UpdateAd"] as KhachHang;
                        dao.UpdateKH(kh1.IdKhachHang, kh);
                        Session.Remove("UpdateAd");
                        Session["ListBlockAd"] = null;
                        Session["ListAd"] = null;
                        //View("Index");
                        return RedirectToAction("Reset", "Admin", new { area = "Admin" });
                    }


                }
            }
        }
    }
}