using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            return View("../Home/Index");
        }
        [HttpPost]
        public ActionResult Index(string email,string password)
        {
            LoginDAO db = new LoginDAO();
            int rs = db.Login(email, password);
            if (rs == 1)
            {
                ViewBag.Acc = "Sai tài khoản hoặc mật khẩu";
                return View("Index");
            }
            else if (rs == 2)
            {
                ViewBag.Acc = "Tài khoản của bạn đã bị khóa";
                return View("Index");
            }
            else
            {
                Session["TaiKhoan"] = db.GetTK(email, password);
                var tk = Session["TaiKhoan"] as KhachHang;
                if (tk.LoaiTK == "Admin")
                {
                    return RedirectToAction("Reset", "Admin", new { area = "Admin" });
                }
                else
                {
                    return View("../Home/Index");
                }
            }
        }
    }
}