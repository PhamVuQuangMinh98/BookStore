using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class NhaCungCapController : Controller
    {
        // GET: Admin/NhaCungCap
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
        public ActionResult Delete(long idNCC)
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
                    var db = new NhaCungCapDAO();
                    bool rs = db.DeleteNCC(idNCC);
                    if (rs == false)
                    {
                        ViewBag.Err = "Vẫn còn sách thuộc nhà cung cấp này";
                        return View("Index");
                    }
                    else
                    {
                        Session["UpdateNCC"] = null;
                        return View("Index");
                    }
                }
            }
        }
        public ActionResult Update(long idNCC)
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
                    var db = new NhaCungCapDAO();
                    Session["UpdateNCC"] = db.Find(idNCC);
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
                    Session["ListNCC"] = null;
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Search(string tenNCC)
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
                    var db = new NhaCungCapDAO();
                    Session["ListNCC"] = db.Search(tenNCC);
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(NhaCungCap ncc, string moTa, HttpPostedFileBase file)
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
                    if (Session["UpdateNCC"] == null)
                    {
                        var db = new NhaCungCapDAO();
                        db.InsertNCC(ncc, moTa, file);
                        Session["ListNCC"] = null;
                        return RedirectToAction("Reset", "NhaCungCap", new { area = "Admin" });
                    }
                    else
                    {
                        var db = new NhaCungCapDAO();
                        var ncc1 = Session["UpdateNCC"] as NhaCungCap;
                        db.UpdateNCC(ncc1.IdNCC, ncc, moTa, file);
                        Session["UpdateNCC"] = null;
                        Session["ListNCC"] = null;
                        return RedirectToAction("Reset", "NhaCungCap", new { area = "Admin" });
                    }
                }

            }
        }
    }
}