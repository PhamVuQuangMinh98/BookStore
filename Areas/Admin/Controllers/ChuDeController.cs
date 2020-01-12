using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: Admin/ChuDe
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
        public ActionResult Update(long idChuDe)
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
                    var db = new ChuDeDAO();
                    Session["UpdateCD"] = db.Find(idChuDe);
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Search(string tenCD, string xuatXu)
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
                    var db = new ChuDeDAO();
                    Session["ListCD"] = db.Search(tenCD, xuatXu);
                    return View("Index");
                }
            }
        }
        public ActionResult Delete(long idChuDe)
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
                    var db = new ChuDeDAO();
                    
                    bool rs= db.DeleteCD(idChuDe);
                    if (rs == false)
                    {
                        ViewBag.Err = "Vẫn còn sách thuộc chủ đề này";
                        return View("Index");
                    }
                    else
                    {
                        Session["UpdateCD"] = null;
                        return View("Index");
                    }
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
                    Session["ListCD"] = null;
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(ChuDe cd)
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
                    if (Session["UpdateCD"] == null)
                    {
                        var db = new ChuDeDAO();
                        db.InsertCD(cd);
                        Session["ListCD"] = null;
                        return RedirectToAction("Reset", "ChuDe", new { area = "Admin" });
                    }
                    else
                    {
                        var dao = new ChuDeDAO();
                        var cd1 = Session["UpdateCD"] as ChuDe;
                        dao.UpdateCD(cd1.IdChuDe, cd);
                        Session["UpdateCD"] = null;
                        Session["ListCD"] = null;
                        return RedirectToAction("Reset", "ChuDe", new { area = "Admin" });
                    }
                }
            }
        }
    }
}