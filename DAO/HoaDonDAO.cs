using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class HoaDonDAO
    {
        ToyContext db=null;
        public HoaDonDAO()
        {
            db = new ToyContext();
        }
        public List<HoaDon> Search(DateTime? ngay, long? id, string taikhoan)
        {
            List<HoaDon> list = new List<HoaDon>();
            if (ngay != null)
            {
                list = db.HoaDon.Where(i => i.NgayTao == ngay && i.flag == true && i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            }
            if (id != null)
            {
                list = db.HoaDon.Where(i => i.IdHoaDon == id && i.flag == true && i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            }
            if (String.IsNullOrEmpty(taikhoan) ==false)
            {
                list= db.HoaDon.Where(i => i.KhachHang.Username.Contains(taikhoan) && i.flag == true && i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            }
            return list;
        }
        public List<HoaDon> GetListHD()
        {
            return db.HoaDon.Where(i =>i.flag==true && i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
        }
        public void ThanhToan(long idHD,string tenNguoiNhan,string diaChi,int sdt)
        {
            HoaDon hd = db.HoaDon.Find(idHD);
            hd.NgayTao = DateTime.Today;
            hd.TenNguoiNhan = tenNguoiNhan;
            hd.DiaChi = diaChi;
            hd.SDT = sdt.ToString();
            hd.TinhTrangHoaDon = "Đã Thanh Toán";
            double tongTien = 0;
            foreach (var item in db.ChiTietHoaDon.Where(i=>i.IdHoaDon==hd.IdHoaDon).ToList())
            {
                Sach sach = db.Sachs.Find(item.IdSach);
                sach.SoLuong = sach.SoLuong - item.SoLuong;
                db.SaveChanges();
                tongTien = tongTien + item.ThanhTien;
            }
            hd.TinhTrangDonHang = "Đang Giao Hàng";
            hd.TongTien = tongTien;
            db.SaveChanges();
        }
        public HoaDon FindHD(long idKhachHang)
        {
            return db.HoaDon.Where(i => i.IdKhachHang == idKhachHang && i.TinhTrangHoaDon == "Chưa Thanh Toán").FirstOrDefault();

        }
        public List<HoaDon> FindHDHome(long idKhachHang)
        {
            return db.HoaDon.Where(i => i.IdKhachHang == idKhachHang && i.TinhTrangHoaDon == "Đã Thanh Toán").OrderByDescending(i=>i.NgayTao).ToList();
        }
        public HoaDon InsertHD(long idKhachHang,DateTime ngayTao,string TTHD,string TTDH)
        {
            string id;
            do
            {
                id = "";
                for (int i = 0; i < 10; i++)
                {
                    Random rd = new Random();
                    id += rd.Next(0, 10).ToString();
                }
            } while (db.TacGias.Find(Convert.ToInt64(id)) != null);

            HoaDon newHD = new HoaDon();
            newHD.IdHoaDon = Convert.ToInt64(id);
            newHD.IdKhachHang =idKhachHang;
            newHD.NgayTao = DateTime.Today;
            newHD.TinhTrangHoaDon = TTHD;
            newHD.TinhTrangDonHang = TTDH;
            newHD.TongTien = 0.0;
            newHD.TenNguoiNhan = "1";
            newHD.DiaChi = "1";
            newHD.SDT = "1";
            newHD.KhachHang = db.KhachHangs.Where(i=>i.IdKhachHang==idKhachHang).FirstOrDefault();
            newHD.flag = true;
            db.HoaDon.Add(newHD);
            db.SaveChanges();
            return newHD;
        }
    }
}