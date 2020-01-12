using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: Admin/TacGia
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
        public ActionResult Delete(long idTacGia)
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
                    var db = new TacGiaDAO();
                    
                    bool rs = db.DeleteTG(idTacGia);
                    if (rs == false)
                    {
                        ViewBag.Err = "Vẫn còn sách thuộc tác giả này";
                        return View("Index");
                    }
                    else
                    {
                        Session["UpdateTG"] = null;
                        return View("Index");
                    }
                }
            }
        }
        public ActionResult Update(long idTacGia)
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
                    var db = new TacGiaDAO();
                    Session["UpdateTG"] = db.Find(idTacGia);
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
                    Session["ListTG"] = null;
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Search(string tenTG)
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
                    var db = new TacGiaDAO();
                    Session["ListTG"] = db.Search(tenTG);
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(TacGia tg, string moTa, HttpPostedFileBase file)
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
                    if (Session["UpdateTG"] == null)
                    {
                        var db = new TacGiaDAO();
                        db.InsertTG(tg, moTa, file);
                        Session["ListTG"] = null;
                        return RedirectToAction("Reset", "TacGia", new { area = "Admin" });
                    }
                    else
                    {
                        var db = new TacGiaDAO();
                        var tg1 = Session["UpdateTG"] as TacGia;
                        db.UpdateTG(tg1.IdTacGia, tg, moTa, file);
                        Session["UpdateTG"] = null;
                        Session["ListTG"] = null;
                        return RedirectToAction("Reset", "TacGia", new { area = "Admin" });
                    }
                }
            }
        }
    }
}