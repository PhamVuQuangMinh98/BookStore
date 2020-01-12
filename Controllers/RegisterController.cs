using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;

namespace WebApplication23.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View("Index");
        }
        [HttpPost]
        public ActionResult Index(string email,string password,string repassword, string ho,string ten,DateTime ngaysinh,int gioitinh)
        {
            Session["Err"] = null;
            var db = new KhachHangDAO();
            var kh = db.FindTK(email);
            if (kh == null)
            {
                if (password != repassword)
                {
                    Session["Err"] = "Mật khẩu phải giống với mật khẩu nhập lại";
                    return View("Index");
                }
                else
                {
                    ViewBag.DK = "Đăng ký thành công";
                    db.InsertKHHome(email, ho, ten, ngaysinh, gioitinh, password);
                    return View("../Login/Index");
                }
            }
            else
            {
                Session["Err"] = "Email đã được sử dụng";
                return View("Index");
            }
        }
    }
}