using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class KhuyenMaiController : Controller
    {
        // GET: Admin/KhuyenMai
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
        [HttpPost]
        public ActionResult Search(string tenKM, DateTime? ngayBD, DateTime? ngayKT)
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
                    var db = new KhuyenMaiDAO();
                    Session["ListKM"] = db.Search(tenKM, ngayBD, ngayKT);
                    return View("Index");
                }
            }
        }
        public ActionResult Delete(long idKM)
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
                    var db = new KhuyenMaiDAO();
                    db.DeleteKM(idKM);
                    Session["UpdateKM"] = null;
                    return View("Index");
                }
            }
        }
        public ActionResult Update(long idKM)
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
                    var db = new KhuyenMaiDAO();
                    Session["UpdateKM"] = db.Find(idKM);
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
                    Session["ListKM"] = null;
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(KhuyenMai km, HttpPostedFileBase file, double phanTram)
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
                    if (Session["UpdateKM"] == null)
                    {
                        var db = new KhuyenMaiDAO();
                        int result = db.InsertKM(km, file, phanTram);
                        if (result == 0)
                        {
                            ModelState.AddModelError("Date", "Ngày bắt đầu phải < ngày kết thúc");
                            return View("Index", km);
                        }
                        else if (result == 1)
                        {
                            ModelState.AddModelError("Date1", "Ngày bắt đầu phải > ngày hiện tại");
                            return View("Index", km);
                        }
                        else
                        {
                            Session["ListKM"] = null;
                            View("Index");
                            return Redirect(Request.UrlReferrer.ToString());
                        }

                    }
                    else
                    {
                        var db = new KhuyenMaiDAO();
                        var km1 = Session["UpdateKM"] as KhuyenMai;
                        int result = db.UpdateKM(km1.IdKhuyenMai, km, file, phanTram);
                        if (result == 0)
                        {
                            ModelState.AddModelError("Date", "Ngày bắt đầu phải < ngày kết thúc");
                            return View("Index", km);
                        }
                        else
                        {
                            Session["ListKM"] = null;
                            Session["UpdateKM"] = null;
                            return RedirectToAction("Reset", "KhuyenMai", new { area = "Admin" });
                        }
                    }
                }
            }
        }
    }
}