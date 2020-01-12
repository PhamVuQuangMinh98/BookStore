using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class SachController : Controller
    {
        // GET: Admin/Sach
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
        public ActionResult Search(string name, long chude, long ncc, long nxb, long tg, long km)
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
                    var db = new SachDAO();
                    Session["ListSach"] = db.Search(name, chude, ncc, nxb, tg, km);
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
                    Session["ListSach"] = null;
                    return View("Index");
                }
            }
        }
        public ActionResult Delete(long idSach)
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
                    var db = new SachDAO();
                    db.DeleteSach(idSach);
                    return View("Index");
                }
            }
        }
        public ActionResult Update(long idSach)
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
                    var db = new SachDAO();
                    Session["UpdateSach"] = db.Find(idSach);
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(Sach sach, HttpPostedFileBase anhBia, string chude, string ncc, string tg, string nxb, string km, HttpPostedFileBase anh1, HttpPostedFileBase anh2, double? chieuDai, double? chieuRong, double? chieuSau, string mota)
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
                    if (Session["UpdateSach"] == null)
                    {
                        var db = new SachDAO();
                        db.InsertSach(sach, anhBia, chude, ncc, tg, nxb, km, anh1, anh2, chieuDai, chieuRong, chieuSau);

                        Session["ListSach"] = null;
                        View("Index");
                        return Redirect(Request.UrlReferrer.ToString());


                    }
                    else
                    {
                        var db = new SachDAO();
                        var newSach = Session["UpdateSach"] as Sach;
                        db.UpdateSach(newSach.ID, sach, anhBia, chude, ncc, tg, nxb, km, anh1, anh2, chieuDai, chieuRong, chieuSau, mota);
                        Session["UpdateSach"] = null;
                        Session["ListSach"] = null;
                        return RedirectToAction("Reset", "Sach", new { area = "Admin" });
                    }
                }
            }
        }
    }
}