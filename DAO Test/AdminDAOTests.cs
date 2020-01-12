using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication23.DAO;
using WebApplication23.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.DAO.Tests
{
    [TestClass()]
    public class AdminDAOTests
    {
        AdminDAO dao = new AdminDAO();


        [TestMethod()]
        public void GetListTest()//lay tat ca tk admin tru ban than
        {
            Assert.AreEqual(1, dao.GetList(1).Count());
        }

        [TestMethod()]
        public void GetListBlockTest()// lay tat ca tk admin bi khoa
        {
            Assert.AreEqual(2, dao.GetListBlock().Count());
        }

        [TestMethod()]
        public void SearchBlockTest1()//tim tat ca
        {
            Assert.AreEqual(2, dao.SearchBlock(null,"0",null).Count());
        }
        [TestMethod()]
        public void SearchBlockTest2()//tim theo ten
        {
            Assert.AreEqual(1, dao.SearchBlock("6", "0", null).Count());
        }
        [TestMethod()]
        public void SearchBlockTest3()//tim theo ten
        {
            Assert.AreEqual(2, dao.SearchBlock("Htest", "0", null).Count());
        }
        [TestMethod()]
        public void SearchBlockTest4()//tim theo gioi tinh
        {
            Assert.AreEqual(0, dao.SearchBlock(null, "nu", null).Count());
        }
        [TestMethod()]
        public void SearchBlockTest5()//tim theo nam sinh
        {
            DateTime test = new DateTime(2007, 7, 7);
            Assert.AreEqual(1, dao.SearchBlock(null, "0", test).Count());
        }

        [TestMethod()]
        public void SearchTest1()//tim tat ca
        {
            Assert.AreEqual(1, dao.Search(null, "0", null,5).Count());
        }
        [TestMethod()]
        public void SearchTest2()//tim theo ten
        {
            Assert.AreEqual(0, dao.Search("5", "0", null, 5).Count());
        }
        [TestMethod()]
        public void SearchTest3()//tim theo gioi tinh
        {
            Assert.AreEqual(1, dao.Search(null, "nam", null, 5).Count());
        }
        [TestMethod()]
        public void SearchTest4()//tim theo ngay sinh
        {
            DateTime test = new DateTime(2007, 7, 7);
            Assert.AreEqual(0, dao.Search(null, "0", test, 5).Count());
        }

        [TestMethod()]
        public void FindTKTest1()//tim ten ton tai
        {
            Assert.AreEqual(true, dao.FindTKtestcase("KHtest3"));
        }
        [TestMethod()]
        public void FindTKTest2()//tim ten ko ton tai
        {
            Assert.AreEqual(false, dao.FindTKtestcase("KHtest"));
        }

        [TestMethod()]
        public void FindTest1()// tim id ton tai
        {
            Assert.AreEqual(true, dao.Findtestcase(1));
        }
        [TestMethod()]
        public void FindTest2()// tim id ko ton tai
        {
            Assert.AreEqual(false, dao.Findtestcase(10));
        }

        [TestMethod()]
        public void UpdateKHTest()
        {
            KhachHang test = new KhachHang();
            test.IdKhachHang = 4;
            test.Ho = "KKK";
            test.Ten = "KK";
            test.NgaySinh = new DateTime(2008, 8, 8);
            test.GioiTinh = false;
            Assert.AreEqual(true, dao.UpdateKHtestcase(test, test.IdKhachHang));
        }

        [TestMethod()]
        public void InsertKHTest()
        {
            KhachHang test = new KhachHang();
            test.Username = "KHtest888";
            test.Password = "123";
            test.Ho = "KKK";
            test.Ten = "KK";
            test.NgaySinh = new DateTime(2008, 8, 8);
            test.GioiTinh = false;
            Assert.AreEqual(true, dao.InsertKHtestcase(test, test.IdKhachHang));
        }

        [TestMethod()]
        public void BlockKHTest1()//khoa khach hang dung
        {
            Assert.AreEqual(true, dao.BlockKHtestcase(2));
        }
        [TestMethod()]
        public void BlockKHTest2()//khoa khach hang da khoa
        {
            Assert.AreEqual(false, dao.BlockKHtestcase(3));
        }

        [TestMethod()]
        public void UnblockKHTest1()//mo khoa khach hang dung
        {
            Assert.AreEqual(true, dao.UnblockKHtestcase(6));
        }
        [TestMethod()]
        public void UnblockKHTest2()//mo khoa khach hang chua khoa
        {
            Assert.AreEqual(false, dao.UnblockKHtestcase(5));
        }
    }
}