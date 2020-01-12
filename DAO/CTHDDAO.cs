using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class CTHDDAO
    {
        ToyContext db = null;
        public CTHDDAO()
        {
            db = new ToyContext();
        }
        public List<ChiTietHoaDon> GetList(long idHD)
        {
            return db.ChiTietHoaDon.Where(i => i.IdHoaDon == idHD).ToList();
        }
        public void DeleteCTHD(long idCTHD)
        {
            ChiTietHoaDon CTHD = db.ChiTietHoaDon.Find(idCTHD);
            db.ChiTietHoaDon.Remove(CTHD);
            db.SaveChanges();
            if (db.ChiTietHoaDon.Where(i => i.IdHoaDon == CTHD.IdHoaDon).Count() == 0)
            {
                db.HoaDon.Remove(db.HoaDon.Find(CTHD.IdHoaDon));
                db.SaveChanges();
            }
        }
        public int UpdateCTHD_GioHang(long idCTHD, int sl)
        {
            ChiTietHoaDon CTHD = db.ChiTietHoaDon.Find(idCTHD);
            if (sl > 3)
            {
                return 1;
            }
            else
            {
                if (sl > db.Sachs.Find(db.ChiTietHoaDon.Find(idCTHD).IdSach).SoLuong)
                {
                    return 2;
                }
                else
                {
                    CTHD.SoLuong = sl;
                    CTHD.ThanhTien = CTHD.DonGia * sl;
                    db.SaveChanges();
                    return 3;
                }
            }
        }
        public int UpdateCTHD(long idHD,long idSach)
        {
            var id = db.ChiTietHoaDon.Where(i => i.IdHoaDon == idHD && i.IdSach == idSach).First().IdCTHD;
            ChiTietHoaDon CTHD = db.ChiTietHoaDon.Find(id);
            if (CTHD.SoLuong >= 3)
            {
                return 1;
            }
            else
            {
                CTHD.SoLuong++;
                if(db.Sachs.Where(i=>i.ID==idSach && i.flag == true).FirstOrDefault().SoLuong < CTHD.SoLuong)
                {
                    return 2;
                }
                CTHD.ThanhTien = CTHD.DonGia * CTHD.SoLuong;
                db.SaveChanges();
                return 3;
            }
        }
        public void InsertCTHD(long idHD,long idSach)
        {
            ChiTietHoaDon newCTHD = new ChiTietHoaDon();
            newCTHD.IdHoaDon = idHD;
            newCTHD.IdSach = idSach;
            var sach = db.Sachs.Find(idSach);
            if (sach.IdKhuyenMai != 8 && sach.KhuyenMai.NgayKT >= DateTime.Today && sach.KhuyenMai.NgayBD <= DateTime.Today)
            {
                double phanTram = sach.KhuyenMai.PhanTram / 100;
                double tienKM = sach.Gia - (sach.Gia * phanTram);
                newCTHD.DonGia =Math.Round(tienKM,MidpointRounding.ToEven);
            }
            else
            {
                newCTHD.DonGia = sach.Gia;
            }
            newCTHD.SoLuong = 1;
            newCTHD.ThanhTien = newCTHD.DonGia;
            newCTHD.flag = true;
            newCTHD.Sach = db.Sachs.Find(idSach);
            db.ChiTietHoaDon.Add(newCTHD);
            db.SaveChanges();
        }
    }
}