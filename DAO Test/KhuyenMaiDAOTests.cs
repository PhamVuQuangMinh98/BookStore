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
    public class KhuyenMaiDAOTests
    {
        ToyContext db = null;
        KhuyenMaiDAO dao = new KhuyenMaiDAO();
        DateTime DTtestBD = new DateTime(2002, 2, 2);
        DateTime DTtestKT = new DateTime(2013, 1, 1);
        [TestMethod()]
        public void GetListTest()// Lay het
        {
            Assert.AreEqual(3, dao.GetList().Count);
        }

        [TestMethod()]
        public void GetListKMTest1()// Con thoi han
        {
            Assert.AreEqual(1, dao.GetListKM().Count);
        }
        [TestMethod()]
        public void GetListKMTest2()// Het thoi han
        {
            Assert.AreEqual(2, dao.GetList().Count-dao.GetListKM().Count);
        }
        [TestMethod()]
        public void SearchTest1()// lay het
        {
            Assert.AreEqual(3, dao.Search(null, null, null).Count);
        }
        [TestMethod()]
        public void SearchTest2()//lay ten giong
        {
            Assert.AreEqual(2, dao.Search("KMtest2", null, null).Count);
        }
        [TestMethod()]
        public void SearchTest3()//lay ten giong
        {
            Assert.AreEqual(2, dao.Search("KMtest2", null, null).Count);
        }
        [TestMethod()]
        public void SearchTest4()//lay ngayBD giong
        {
            Assert.AreEqual(1, dao.Search(null, DTtestBD, null).Count);
        }
        [TestMethod()]
        public void SearchTest5()//lay ngayKT giong
        {
            Assert.AreEqual(1, dao.Search(null, null, DTtestKT).Count);
        }
        [TestMethod()]
        public void SearchTest6()//lay ca 3 thuoc tinh
        {
            Assert.AreEqual(0, dao.Search("KMtest2", DTtestBD, DTtestKT).Count);
        }

        [TestMethod()]
        public void FindTest()// so sanh ten
        {
            KhuyenMai test = new KhuyenMai();
            test.TenKhuyenMai = "KMtest25";
            Assert.AreEqual(test.TenKhuyenMai, dao.Find(5).TenKhuyenMai);
        }

        [TestMethod()]
        public void UpdateKMTest1()//update dung
        {
            KhuyenMai test = new KhuyenMai();
            test.IdKhuyenMai = 4;
            test.TenKhuyenMai = "DDD";
            test.HinhKhuyenMai = null;
            test.PhanTram = 1.7;
            test.NgayBD = new DateTime(2020, 1, 1);
            test.NgayKT = new DateTime(2020, 2, 2);
            Assert.AreEqual(1, dao.UpdateKMtestcase(test.IdKhuyenMai,test,null,test.PhanTram));
        }
        [TestMethod()]
        public void UpdateKMTest2()//update sai
        {
            KhuyenMai test = new KhuyenMai();
            test.IdKhuyenMai = 4;
            test.TenKhuyenMai = "DDD";
            test.HinhKhuyenMai = null;
            test.PhanTram = 1.7;
            test.NgayBD = new DateTime(2020, 2, 2);
            test.NgayKT = new DateTime(2020, 1, 1);
            Assert.AreEqual(0, dao.UpdateKMtestcase(test.IdKhuyenMai, test, null, test.PhanTram));
        }

        [TestMethod()]
        public void InsertKMTest()
        {
            KhuyenMai test = new KhuyenMai();
            test.TenKhuyenMai = "AAA";
            test.HinhKhuyenMai = null;
            test.PhanTram = 1.7;
            test.NgayBD = new DateTime(2020, 1, 1);
            test.NgayKT = new DateTime(2020, 3, 3);
            Assert.AreEqual(true, dao.InsertKMtestcase(test, test.IdKhuyenMai));
        }

        [TestMethod()]
        public void DeleteKMTest()
        {
            KhuyenMai test = new KhuyenMai();
            test.IdKhuyenMai = 5;
            Assert.AreEqual(false, dao.DeleteKMTestCase(test.IdKhuyenMai));
        }
    }
  
}