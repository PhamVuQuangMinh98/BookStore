using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication23.Areas.Admin.Models;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class BieuDoDAO
    {
        ToyContext db = null;
        public BieuDoDAO()
        {
            db = new ToyContext();
        }
        public List<ChiTietHoaDon> GetListCTHD()
        {
            List<HoaDon> listHD = db.HoaDon.Where(i => i.flag == true && i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            List<ChiTietHoaDon> listCTHD = new List<ChiTietHoaDon>();
            foreach (var item in listHD)
            {
                foreach (var item1 in db.ChiTietHoaDon.Where(i=>i.IdHoaDon==item.IdHoaDon).ToList())
                {
                    listCTHD.Add(item1);
                }
            }
            return listCTHD;
        }
        public List<SachVM> GetListSachNoiBatAll()
        {
            List<Sach> listSach = GetListSach();
            List<SachVM> listSachNB = new List<SachVM>();
            List<ChiTietHoaDon> listCTHD = GetListCTHD();
            foreach (var item in listSach)
            {
                SachVM svm = new SachVM();
                svm.IdSach = item.ID;
                foreach (var item1 in listCTHD.Where(i => i.IdSach == item.ID).ToList())
                {
                    svm.SoLuong = svm.SoLuong + item1.SoLuong;
                }
                listSachNB.Add(svm);
            }
            listSachNB = listSachNB.OrderByDescending(i => i.SoLuong).ToList();
            return listSachNB;
        }
        public List<SachVM> GetListSachNoiBat()
        {
            List<Sach> listSach = GetListSach();
            List<SachVM> listSachNB = new List<SachVM>();
            List<ChiTietHoaDon> listCTHD = GetListCTHD();
            foreach (var item in listSach)
            {
                SachVM svm = new SachVM();
                svm.IdSach = item.ID;
                foreach (var item1 in listCTHD.Where(i => i.IdSach == item.ID).ToList())
                {
                    svm.SoLuong = svm.SoLuong + item1.SoLuong;
                }
                listSachNB.Add(svm);
            }
            listSachNB = listSachNB.OrderByDescending(i => i.SoLuong).Take(10).ToList();
            return listSachNB;
        }
        public List<ChuDeVM> GetListChuDeNoiBat()
        {
            List<ChuDe> listCD = GetListChuDe();
            List<ChuDeVM> listChuDeNB = new List<ChuDeVM>();
            List<ChiTietHoaDon> listCTHD = GetListCTHD();
            foreach (var item in listCD)
            {
                ChuDeVM cdvm = new ChuDeVM();
                cdvm.IdChuDe = item.IdChuDe;
                foreach (var item1 in listCTHD.Where(i => i.Sach.IdChuDe == item.IdChuDe).ToList())
                {
                    cdvm.SoLuong = cdvm.SoLuong + item1.SoLuong;
                }
                listChuDeNB.Add(cdvm);
            }
            listChuDeNB = listChuDeNB.OrderByDescending(i => i.SoLuong).Take(10).ToList();
            return listChuDeNB;
        }
        public List<string> GetTenCDNB()
        {
            List<string> listCD = new List<string>();
            foreach (var item in GetListChuDeNoiBat())
            {
                listCD.Add(db.ChuDes.Find(item.IdChuDe).Ten);
            }
            return listCD;
        }
        public List<string> GetTenSachNB()
        {
            List<string> listSach = new List<string>();
            foreach (var item in GetListSachNoiBat())
            {
                listSach.Add(db.Sachs.Find(item.IdSach).Ten);
            }
            return listSach;
        }
        public List<int> GetSLCDNB()
        {
            List<int> listSL = new List<int>();
            foreach (var item in GetListChuDeNoiBat())
            {
                listSL.Add(item.SoLuong);
            }
            return listSL;
        }
        public List<int> GetSLSachNB()
        {
            List<int> listSL = new List<int>();
            foreach (var item in GetListSachNoiBat())
            {
                listSL.Add(item.SoLuong);
            }
            return listSL;
        }
        public List<Sach> GetListSach()
        {
            return db.Sachs.Where(i => i.flag == true).ToList();
        }
        public List<ChuDe> GetListChuDe()
        {
            return db.ChuDes.Where(i => i.flag == true).ToList();
        }
        public List<int> GetListMonth()
        {
            List<int> listMonth = new List<int>();
            for (int i = 1; i <=12 ; i++)
            {
                listMonth.Add(i);
            }
            return listMonth;
        }
        public int DemSoLuongCTHD(long idHD)
        {
            int sl = 0;
            foreach (var item in db.ChiTietHoaDon.Where(i=>i.IdHoaDon==idHD).ToList())
            {
                sl = sl + item.SoLuong;
            }
            return sl;
        }
        public List<double> GetDoanhThuTheoThang()
        {
            List<int> listMonth = GetListMonth();
            List<double> listDT = new List<double>();
            List<HoaDon> listHD = db.HoaDon.Where(i => i.flag == true && i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            foreach (var item in listMonth)
            {
                double dt = 0;
                foreach (var item1 in listHD)
                {
                    if (item == item1.NgayTao.Month && item1.NgayTao.Year == DateTime.Today.Year)
                    {
                        dt = dt + item1.TongTien;
                    }
                }
                listDT.Add(dt);
            }
            return listDT;
        }
        public List<int> GetListSoLuongTheoThang()
        {
            List<int> listMonth = GetListMonth();
            List<int> listSL = new List<int>();
            List<HoaDon> listHD = db.HoaDon.Where(i => i.flag == true && i.TinhTrangHoaDon == "Đã Thanh Toán").ToList();
            foreach (var item in listMonth)
            {
                int sl = 0;
                foreach (var item1 in listHD)
                {                   
                    if (item == item1.NgayTao.Month && item1.NgayTao.Year==DateTime.Today.Year)
                    {
                        sl = sl + DemSoLuongCTHD(item1.IdHoaDon);
                    }
                }
                listSL.Add(sl);
            }
            return listSL;
                
        }
    }
}