using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication23.DAO;
using WebApplication23.Models;

namespace WebApplication23.Controllers
{
    public class ThanhToanController : Controller
    {
        // GET: ThanhToan
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                HoaDonDAO db = new HoaDonDAO();
                KhachHang kh = Session["TaiKhoan"] as KhachHang;
                HoaDon hd = db.FindHD(kh.IdKhachHang);
                Session["TTHD"] = hd;
                return View("Index");
            }
        }
        public void XuatExcel(List<ChiTietHoaDon> cthd)
        {
            try
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                excel.Visible = false;
                int index = 2;
                long idHD = 0;
                int process = cthd.Count;

                ws.Cells[1, 1] = "Mã Sách";
                ws.Cells[1, 2] = "Tên Sách";
                ws.Cells[1, 3] = "Đơn Giá";
                ws.Cells[1, 4] = "Số Lượng";
                ws.Cells[1, 5] = "Thành Tiền";
                double TongTien = 0;
                foreach (var sinhVien in cthd)
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
                ws.SaveAs(@path + "/HoaDon"+idHD+".xlsx",
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
        }
        [HttpPost]
        public ActionResult Index(string tenngnhan,string diachi,int sdt)
        {
            HoaDonDAO db = new HoaDonDAO();
            KhachHang kh = Session["TaiKhoan"] as KhachHang;
            HoaDon hd = db.FindHD(kh.IdKhachHang);
            if (hd != null)
            {
                db.ThanhToan(hd.IdHoaDon, tenngnhan, diachi, sdt);
                List<ChiTietHoaDon> list = Session["ListCTHD"] as List<ChiTietHoaDon>;
                XuatExcel(list);
                Session["ListCTHD"] = null;

            }
            return View("../Home/Index");
        }
    }
}