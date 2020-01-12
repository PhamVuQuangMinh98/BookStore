using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;

namespace WebApplication23.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../Login/Index");
            }
            else
            {
                return View("Index");
            }
        }
        [HttpPost]
        public ActionResult Index(long idtaikhoan,string email,string oldpassword,string newpassword,string renewpassword, string ho,string ten,DateTime ngaysinh, int gioitinh)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../Login/Index");
            }
            else
            {
                var login = new LoginDAO();
                if(String.IsNullOrEmpty(newpassword)!=true || String.IsNullOrEmpty(renewpassword) != true)
                {
                    if (newpassword != renewpassword)
                    {
                        ViewBag.Err = "Mật Khẩu Mới Phải Trùng Với Mật Khẩu Nhập Lại";
                        return View("Index");
                    }
                }
                int rs = login.Login(email,oldpassword);
                if (rs == 1)
                {
                    ViewBag.Err = "Sai Mật Khẩu";
                    return View("Index");
                }
                else
                {
                    Session["TaiKhoan"]=login.UpdateTK( idtaikhoan,  email,  newpassword, renewpassword,  ho,  ten,  ngaysinh,gioitinh);
                    ViewBag.Err = "Cập Nhật Thông Tin Thành Công";
                    return View("Index");
                }
            }
        }
    }
}