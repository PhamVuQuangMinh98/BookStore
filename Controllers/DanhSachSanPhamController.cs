﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication23.Controllers
{
    public class DanhSachSanPhamController : Controller
    {
        // GET: DanhSachSanPham
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}