using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class NhaXuatBanController : Controller
    {
        // GET: Admin/NhaXuatBan
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
        public ActionResult Delete(long idNXB)
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
                    var db = new NhaXuatBanDAO();
                    
                    bool rs = db.DeleteNXB(idNXB);
                    if (rs == false)
                    {
                        ViewBag.Err = "Vẫn còn sách thuộc nhà xuất bản này";
                        return View("Index");
                    }
                    else
                    {
                        Session["UpdateNXB"] = null;
                        return View("Index");
                    }
                }
            }
        }
        public ActionResult Update(long idNXB)
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
                    var db = new NhaXuatBanDAO();
                    Session["UpdateNXB"] = db.Find(idNXB);
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
                    Session["ListNXB"] = null;
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Search(string tenNXB)
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
                    var db = new NhaXuatBanDAO();
                    Session["ListNXB"] = db.Search(tenNXB);
                    return View("Index");
                }
            }
        }
        [HttpPost]
        public ActionResult Index(NhaXuatBan nxb, string moTa, HttpPostedFileBase file)
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
                    if (Session["UpdateNXB"] == null)
                    {
                        var db = new NhaXuatBanDAO();
                        db.InsertNXB(nxb, moTa, file);
                        Session["ListNXB"] = null;
                        return RedirectToAction("Reset", "NhaXuatBan", new { area = "Admin" });
                    }
                    else
                    {
                        var db = new NhaXuatBanDAO();
                        var nxb1 = Session["UpdateNXB"] as NhaXuatBan;
                        db.UpdateNXB(nxb1.IdNXB, nxb, moTa, file);
                        Session["UpdateNXB"] = null;
                        Session["ListNXB"] = null;
                        return RedirectToAction("Reset", "NhaXuatBan", new { area = "Admin" });
                    }
                }
            }
        }
    }
}