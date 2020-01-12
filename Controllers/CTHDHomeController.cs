using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Controllers
{
    public class CTHDHomeController : Controller
    {
        // GET: CTHDHome
        public ActionResult Index(long? idHD)
        {
            if (Session["TaiKhoan"] == null)
            {
                return View("../Login/Index");
            }
            else
            {
                if (idHD == null)
                {
                    return View("../Home/Index");
                }
                var cthd = new CTHDDAO();
                var db = new ToyContext();
                Session["HDHome"] = db.HoaDon.Find(idHD);
                long id = Convert.ToInt64(idHD);
                Session["ListCTHDHome"] = cthd.GetList(id);
                return View("Index");
            }
        }
    }
}