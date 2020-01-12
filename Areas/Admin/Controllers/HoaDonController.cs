using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Areas.Admin.Controllers
{
    public class HoaDonController : Controller
    {
        // GET: Admin/HoaDon
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
                    var hd = new HoaDonDAO();
                    Session["ListHoaDon"] = hd.GetListHD();
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
                    var hd = new HoaDonDAO();
                    Session["ListHoaDon"] = hd.GetListHD();
                    return View("Index");
                }
            }
        }
        public ActionResult XuatExcel(long idHD)
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
                    var db = new ToyContext();
                    var cthd = new CTHDDAO();
                    var list = cthd.GetList(idHD);
                    try
                    {
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                        Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                        excel.Visible = false;
                        int index = 2;
                        int process = list.Count;

                        ws.Cells[1, 1] = "Mã Sách";
                        ws.Cells[1, 2] = "Tên Sách";
                        ws.Cells[1, 3] = "Đơn Giá";
                        ws.Cells[1, 4] = "Số Lượng";
                        ws.Cells[1, 5] = "Thành Tiền";
                        double TongTien = 0;
                        foreach (var sinhVien in list)
                        {
                            idHD = sinhVien.IdHoaDon;
                            ws.Cells[index, 1] = sinhVien.IdSach;
                            ws.Cells[index, 2] = sinhVien.Sach.Ten;
                            ws.Cells[index, 3] = sinhVien.Sach.Gia;
                            ws.Cells[index, 4] = sinhVien.SoLuong;
                            ws.Cells[index, 5] = sinhVien.ThanhTien;
                            TongTien = TongTien + sinhVien.ThanhTien;
                            index += 1;
                        }
                        ws.Cells[index, 4] = "Tổng Tiền";
                        ws.Cells[index, 5] = TongTien;
                        ws.SaveAs(@path + "/HoaDonAdmin" + idHD + ".xlsx",
                        XlFileFormat.xlWorkbookDefault,
                        Type.Missing,
                        Type.Missing,
                        true, false,
                        XlSaveAsAccessMode.xlNoChange,
                        XlSaveConflictResolution.xlLocalSessionChanges,
                        Type.Missing,
                        Type.Missing);
                        excel.Quit();
                    }
                    catch (Exception ex) { }
                    return RedirectToAction("Reset", "HoaDon", new { area = "Admin" });
                }
            }
        }
        public ActionResult Giao(long idHD)
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
                    var db = new ToyContext();
                    db.HoaDon.Find(idHD).TinhTrangDonHang = "Đã Giao Hàng";
                    db.SaveChanges();
                    return RedirectToAction("Reset", "HoaDon", new { area = "Admin" });
                }
            }
        }
        [HttpPost]
        public ActionResult Search(DateTime? ngay,long? id,string taikhoan)
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
                    var hd = new HoaDonDAO();
                    Session["ListHoaDon"] = hd.Search(ngay,id,taikhoan);
                    return View("Index");
                }
            }
        }
        public ActionResult ChiTietHoaDon(long idHD)
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
                    var cthd = new CTHDDAO();
                    Session["IdHD"] = idHD.ToString();
                    Session["ListCTHoaDon"] = cthd.GetList(idHD);
                    return RedirectToAction("Index", "CTHD", new { area = "Admin" });
                }
            }
        }
    }
}