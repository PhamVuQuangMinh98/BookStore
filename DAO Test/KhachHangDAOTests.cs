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
    public class KhachHangDAOTests
    {
        KhachHangDAO dao = new KhachHangDAO();
        [TestMethod()]
        public void GetListTest()//Lay kh chua khoa
        {
            Assert.AreEqual(3, dao.GetList().Count);
        }

        [TestMethod()]
        public void GetListBlockTest()//Lay kh da khoa
        {
            Assert.AreEqual(2, dao.GetListBlock().Count);
        }

        string testname = "KHtest8";
        string testgioitinh = "nam";
        DateTime testngaysinh = new DateTime(2008, 8, 8);
        [TestMethod()]
        public void SearchBlockTest1()//lay tat ca kh khoa
        {
            Assert.AreEqual(2, dao.SearchBlock(null, "0", null).Count);
        }
        [TestMethod()]
        public void SearchBlockTest2()//lay ten trung
        {
            Assert.AreEqual(1, dao.SearchBlock(testname, "0", null).Count);
        }
        [TestMethod()]
        public void SearchBlockTest3()//lay ten giong
        {
            Assert.AreEqual(2, dao.SearchBlock("KHtest", "0", null).Count);
        }
        [TestMethod()]
        public void SearchBlockTest4()//lay gioi tinh
        {
            Assert.AreEqual(testname, dao.SearchBlock(null, testgioitinh, null).FirstOrDefault().Username);
        }
        [TestMethod()]
        public void SearchBlockTest5()//lay ngay sinh trung
        {
            Assert.AreEqual(testname, dao.SearchBlock(null, "0", testngaysinh).FirstOrDefault().Username);
        }
        [TestMethod()]
        public void SearchBlockTest6()//lay ngay sinh sai
        {
            Assert.AreEqual(0, dao.SearchBlock(null, "0", new DateTime(2009,08,08)).Count);
        }
        [TestMethod()]
        public void SearchBlockTest7()//lay ten sai
        {
            Assert.AreEqual(0, dao.SearchBlock("KHtet8", "0", null).Count);
        }

        string testname1 = "KHtest2";
        string testgioitinh1 = "nu";
        DateTime testngaysinh1 = new DateTime(2002, 2, 2);
        [TestMethod()]
        public void SearchTest1()//lay tat ca kh khoa
        {
            Assert.AreEqual(3, dao.Search(null, "0", null).Count);
        }
        [TestMethod()]
        public void SearchTest2()//lay ten trung
        {
            Assert.AreEqual(1, dao.Search(testname1, "0", null).Count);
        }
        [TestMethod()]
        public void SearchTest3()//lay ten giong
        {
            Assert.AreEqual(3, dao.Search("KHtest", "0", null).Count);
        }
        [TestMethod()]
        public void SearchTest4()//lay gioi tinh
        {
            Assert.AreEqual(2, dao.Search(null, testgioitinh1, null).Count);
        }
        [TestMethod()]
        public void SearchTest5()//lay ngay sinh trung
        {
            Assert.AreEqual(testname1, dao.Search(null, "0", testngaysinh1).FirstOrDefault().Username);
        }
        [TestMethod()]
        public void SearchTest6()//lay ngay sinh sai
        {
            Assert.AreEqual(0, dao.Search(null, "0", new DateTime(2009, 08, 08)).Count);
        }
        [TestMethod()]
        public void SearchTest7()//lay ten sai
        {
            Assert.AreEqual(0, dao.Search("KHtet", "0", null).Count);
        }

        [TestMethod()]
        public void FindTKTest1()//tim khach hang ton tai
        {
            var test = dao.FindTK(testname);
            bool kq=false;
            if(test!=null)
            {
                kq = true;
            }
            Assert.AreEqual(true, kq);
        }
        [TestMethod()]
        public void FindTKTest2()//tim khach hang khong ton tai
        {
            var test = dao.FindTK("KHtetet");
            bool kq = false;
            if (test != null)
            {
                kq = true;
            }
            Assert.AreEqual(false, kq);
        }

        [TestMethod()]
        public void UpdateKHTest()
        {
            KhachHang test = new KhachHang();
            test.IdKhachHang = 9;
            test.Ho = "LA";
            test.Ten = "LA";
            test.GioiTinh = false;
            test.NgaySinh = new DateTime(2009, 10, 09);
            Assert.AreEqual(true, dao.UpdateKHtestcase(test.IdKhachHang, test));
        }

        [TestMethod()]
        public void InsertKHHomeTest()
        {
            KhachHang test = new KhachHang();
            test.Username = "KHtest";
            test.Password = "123";
            test.Ho = "LA";
            test.Ten = "LA";
            test.GioiTinh = false;
            test.NgaySinh = new DateTime(2009, 10, 09);
            Assert.AreEqual(true, dao.InsertKHhomeTestcase(test));
        }

    }
}