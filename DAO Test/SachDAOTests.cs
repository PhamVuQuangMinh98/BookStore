using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication23.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication23.Models;

namespace WebApplication23.DAO.Tests
{
    [TestClass()]
    public class SachDAOTests
    {
        ToyContext db = null;
        SachDAO dao = new SachDAO();
        [TestMethod()]
        public void GetListKMTest()
        {
            Assert.AreEqual(2, dao.GetListKM(1).Count);
        }

        [TestMethod()]
        public void GetListCDTest1()//lay chu de ma sach co
        {
            Assert.AreEqual(1, dao.GetListCD(6).Count);
        }
        [TestMethod()]
        public void GetListCDTest2()//lay chu de ma sach khong co
        {
            Assert.AreEqual(0, dao.GetListCD(4).Count);
        }

        [TestMethod()]
        public void GetListTest()// lay het sach
        {
            Assert.AreEqual(4, dao.GetList().Count);
        }

        [TestMethod()]
        public void SearchTest1()//tim theo ten
        {
            Assert.AreEqual(1, dao.Search("Test3", 0, 0, 0, 0, 0).Count);
        }
        [TestMethod()]
        public void SearchTest2()//tim theo chu de
        {
            Assert.AreEqual(2, dao.Search("", 2, 0, 0, 0, 0).Count);
        }
        [TestMethod()]
        public void SearchTest3()// tim theo ncc
        {
            Assert.AreEqual(2, dao.Search("", 0, 1, 0, 0, 0).Count);
        }
        [TestMethod()]
        public void SearchTest4()// tim theo nxb
        {
            Assert.AreEqual(0, dao.Search("", 0, 0, 3, 0, 0).Count);
        }

        [TestMethod()]
        public void SearchTest5()// tim theo tac gia
        {
            Assert.AreEqual(1, dao.Search("", 0, 0, 0, 2, 0).Count);
        }

        [TestMethod()]
        public void SearchTest6()// tim theo khuyen mai
        {
            Assert.AreEqual(2, dao.Search("", 0, 0, 0, 0, 1).Count);
        }

        public Sach testobj(double? chieuDai, double? chieuRong, double? chieuSau)
        {
            Sach newSach = new Sach();
            newSach.Ten = "KHtest10";
            newSach.Gia = 40000;
            newSach.SoLuong = 200;
            newSach.NamXuatBan = new DateTime(2009,12,8);
            newSach.IdChuDe = 1;
            newSach.ChuDe = db.ChuDes.Find(Convert.ToInt64(1));
            newSach.IdNCC = 1;
            newSach.NhaCungCap = db.NhaCungCaps.Find(Convert.ToInt64(1));
            newSach.IdNXB = 1;
            newSach.NhaXuatBan = db.NhaXuatBans.Find(Convert.ToInt64(1));
            newSach.IdTacGia = 1;
            newSach.TacGia = db.TacGias.Find(Convert.ToInt64(1));
            newSach.IdKhuyenMai = 2;
            newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(2));
            newSach.HinhThuc = "Bia cung";
            newSach.MoTa = "ALALA";
            newSach.NgonNgu = "Tiếng Anh";
            newSach.TrongLuong = 13;
            newSach.SoTrang = 70;
            newSach.Anh1 = null;
            newSach.Anh2 = null;
            newSach.AnhBia = null;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            return newSach;
        }
        
        
        [TestMethod()]
        public void UpdateSachTest()
        {
            //Sach test = new Sach();
            //test = testobj(12, 12, 12);
            Sach newSach = new Sach();
            newSach.Ten = "KHtest10";
            newSach.Gia = 40000;
            newSach.SoLuong = 200;
            newSach.NamXuatBan = new DateTime(2009, 12, 8);
            newSach.IdChuDe = 1;
            newSach.IdNCC = 1;
            newSach.IdNXB = 1;
            newSach.IdTacGia = 1;
            newSach.IdKhuyenMai = 2;
            newSach.HinhThuc = "Bia cung";
            newSach.MoTa = "ALALA";
            newSach.NgonNgu = "Tiếng Anh";
            newSach.TrongLuong = 13;
            newSach.SoTrang = 70;
            newSach.Anh1 = null;
            newSach.Anh2 = null;
            newSach.AnhBia = null;
            double chieuDai = 12;
            double chieuRong = 12;
            double chieuSau = 12;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            //Assert.AreEqual(true, dao.UpdateSachtestcase(2, test, test.IdChuDe.ToString(), test.IdNCC.ToString(), test.IdTacGia.ToString(), test.IdNXB.ToString(), test.IdKhuyenMai.ToString()));
            Assert.AreEqual(true, dao.UpdateSachtestcase(4, newSach, newSach.IdChuDe.ToString(), newSach.IdNCC.ToString(), newSach.IdTacGia.ToString(), newSach.IdNXB.ToString(), newSach.IdKhuyenMai.ToString()));
        }

        [TestMethod()]
        public void InsertSachTest1()// Them sach dung
        {
            Sach newSach = new Sach();
            newSach.Ten = "KHtest10";
            newSach.Gia = 40000;
            newSach.SoLuong = 200;
            newSach.NamXuatBan = new DateTime(2009, 12, 8);
            newSach.IdChuDe = 1;
            newSach.IdNCC = 1;
            newSach.IdNXB = 1;
            newSach.IdTacGia = 1;
            newSach.IdKhuyenMai = 2;
            newSach.HinhThuc = "Bia cung";
            newSach.MoTa = "ALALA";
            newSach.NgonNgu = "Tiếng Anh";
            newSach.TrongLuong = 13;
            newSach.SoTrang = 70;
            newSach.Anh1 = null;
            newSach.Anh2 = null;
            newSach.AnhBia = null;
            double chieuDai = 12;
            double chieuRong = 12;
            double chieuSau = 12;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            //Assert.AreEqual(true, dao.UpdateSachtestcase(2, test, test.IdChuDe.ToString(), test.IdNCC.ToString(), test.IdTacGia.ToString(), test.IdNXB.ToString(), test.IdKhuyenMai.ToString()));
            Assert.AreEqual(true, dao.InsertSachtestcase(newSach, newSach.IdChuDe.ToString(), newSach.IdNCC.ToString(), newSach.IdTacGia.ToString(), newSach.IdNXB.ToString(), newSach.IdKhuyenMai.ToString()));
        }
        [TestMethod()]
        public void DeleteSachTest()
        {
            Assert.AreEqual(true, dao.DeleteSachtestcase(3));
        }

        [TestMethod()]
        public void InsertSachTest2()// Them sach sai gia
        {
            Sach newSach = new Sach();
            newSach.Ten = "KHtest10";
            newSach.Gia = -40000;
            newSach.SoLuong = 200;
            newSach.NamXuatBan = new DateTime(2009, 12, 8);
            newSach.IdChuDe = 1;
            newSach.IdNCC = 1;
            newSach.IdNXB = 1;
            newSach.IdTacGia = 1;
            newSach.IdKhuyenMai = 2;
            newSach.HinhThuc = "Bia cung";
            newSach.MoTa = "ALALA";
            newSach.NgonNgu = "Tiếng Anh";
            newSach.TrongLuong = 13;
            newSach.SoTrang = 70;
            newSach.Anh1 = null;
            newSach.Anh2 = null;
            newSach.AnhBia = null;
            double chieuDai = 12;
            double chieuRong = 12;
            double chieuSau = 12;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            //Assert.AreEqual(true, dao.UpdateSachtestcase(2, test, test.IdChuDe.ToString(), test.IdNCC.ToString(), test.IdTacGia.ToString(), test.IdNXB.ToString(), test.IdKhuyenMai.ToString()));
            Assert.AreEqual(false, dao.InsertSachtestcase(newSach, newSach.IdChuDe.ToString(), newSach.IdNCC.ToString(), newSach.IdTacGia.ToString(), newSach.IdNXB.ToString(), newSach.IdKhuyenMai.ToString()));
        }
        [TestMethod()]
        public void InsertSachTest3()// Them sach sai kich thuoc
        {
            Sach newSach = new Sach();
            newSach.Ten = "KHtest10";
            newSach.Gia = 40000;
            newSach.SoLuong = 200;
            newSach.NamXuatBan = new DateTime(2009, 12, 8);
            newSach.IdChuDe = 1;
            newSach.IdNCC = 1;
            newSach.IdNXB = 1;
            newSach.IdTacGia = 1;
            newSach.IdKhuyenMai = 2;
            newSach.HinhThuc = "Bia cung";
            newSach.MoTa = "ALALA";
            newSach.NgonNgu = "Tiếng Anh";
            newSach.TrongLuong = 13;
            newSach.SoTrang = 70;
            newSach.Anh1 = null;
            newSach.Anh2 = null;
            newSach.AnhBia = null;
            double chieuDai = 12;
            double chieuRong = -12;
            double chieuSau = 12;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            //Assert.AreEqual(true, dao.UpdateSachtestcase(2, test, test.IdChuDe.ToString(), test.IdNCC.ToString(), test.IdTacGia.ToString(), test.IdNXB.ToString(), test.IdKhuyenMai.ToString()));
            Assert.AreEqual(false, dao.InsertSachtestcase(newSach, newSach.IdChuDe.ToString(), newSach.IdNCC.ToString(), newSach.IdTacGia.ToString(), newSach.IdNXB.ToString(), newSach.IdKhuyenMai.ToString()));
        }

        [TestMethod()]
        public void InsertSachTest4()// Them sach sai trong luong
        {
            Sach newSach = new Sach();
            newSach.Ten = "KHtest10";
            newSach.Gia = 40000;
            newSach.SoLuong = 200;
            newSach.NamXuatBan = new DateTime(2009, 12, 8);
            newSach.IdChuDe = 1;
            newSach.IdNCC = 1;
            newSach.IdNXB = 1;
            newSach.IdTacGia = 1;
            newSach.IdKhuyenMai = 2;
            newSach.HinhThuc = "Bia cung";
            newSach.MoTa = "ALALA";
            newSach.NgonNgu = "Tiếng Anh";
            newSach.TrongLuong = -13;
            newSach.SoTrang = 70;
            newSach.Anh1 = null;
            newSach.Anh2 = null;
            newSach.AnhBia = null;
            double chieuDai = 12;
            double chieuRong = 12;
            double chieuSau = 12;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            //Assert.AreEqual(true, dao.UpdateSachtestcase(2, test, test.IdChuDe.ToString(), test.IdNCC.ToString(), test.IdTacGia.ToString(), test.IdNXB.ToString(), test.IdKhuyenMai.ToString()));
            Assert.AreEqual(false, dao.InsertSachtestcase(newSach, newSach.IdChuDe.ToString(), newSach.IdNCC.ToString(), newSach.IdTacGia.ToString(), newSach.IdNXB.ToString(), newSach.IdKhuyenMai.ToString()));
        }
        [TestMethod()]
        public void InsertSachTest5()// Them sach sai so trang
        {
            Sach newSach = new Sach();
            newSach.Ten = "KHtest10";
            newSach.Gia = 40000;
            newSach.SoLuong = 200;
            newSach.NamXuatBan = new DateTime(2009, 12, 8);
            newSach.IdChuDe = 1;
            newSach.IdNCC = 1;
            newSach.IdNXB = 1;
            newSach.IdTacGia = 1;
            newSach.IdKhuyenMai = 2;
            newSach.HinhThuc = "Bia cung";
            newSach.MoTa = "ALALA";
            newSach.NgonNgu = "Tiếng Anh";
            newSach.TrongLuong = -13;
            newSach.SoTrang = -70;
            newSach.Anh1 = null;
            newSach.Anh2 = null;
            newSach.AnhBia = null;
            double chieuDai = 12;
            double chieuRong = 12;
            double chieuSau = 12;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            //Assert.AreEqual(true, dao.UpdateSachtestcase(2, test, test.IdChuDe.ToString(), test.IdNCC.ToString(), test.IdTacGia.ToString(), test.IdNXB.ToString(), test.IdKhuyenMai.ToString()));
            Assert.AreEqual(false, dao.InsertSachtestcase(newSach, newSach.IdChuDe.ToString(), newSach.IdNCC.ToString(), newSach.IdTacGia.ToString(), newSach.IdNXB.ToString(), newSach.IdKhuyenMai.ToString()));
        }


    }
}