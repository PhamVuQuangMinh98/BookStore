using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;

namespace WebApplication23.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult ChuDe(long idCD)
        {
            var cd = new ChuDeDAO();
            Session["Title"] = cd.Find(idCD).Ten;
            var sach = new SachDAO();
            Session["Products"] = sach.GetListCD(idCD);
            return View("../DanhSachSanPham/Index");
        }
    }
}