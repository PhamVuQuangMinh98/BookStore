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
    public class LoginDAOTests
    {
        LoginDAO dao = new LoginDAO();
        [TestMethod()]
        public void GetTKTest1()// lay tk dung
        {
            Assert.AreEqual(true, dao.GetTKtestcase("KHtest1", "123"));
        }
        [TestMethod()]
        public void GetTKTest2()// sai username
        {
            Assert.AreEqual(false, dao.GetTKtestcase("KHtes1", "123"));
        }
        [TestMethod()]
        public void GetTKTest3()// sai password
        {
            Assert.AreEqual(false, dao.GetTKtestcase("KHtest1", "124"));
        }

        [TestMethod()]
        public void UpdateTKTest1()//update duoc
        {
            KhachHang test = new KhachHang();
            test.IdKhachHang = 4;
            test.Ho = "WWW";
            test.Ten = "QQQ";
            test.GioiTinh = true;
            test.NgaySinh = new DateTime(2007, 7, 7);
            string password = "12345";
            string repassword = "12345";
            Assert.AreEqual(true, dao.UpdateKHtestcase(test.IdKhachHang, test, password, repassword));
        }

        [TestMethod()]
        public void UpdateTKTest2()//mat khau moi khong trung voi xac nhan mat khau
        {
            KhachHang test = new KhachHang();
            test.IdKhachHang = 4;
            test.Ho = "WWW";
            test.Ten = "QQQ";
            test.GioiTinh = true;
            test.NgaySinh = new DateTime(2007, 7, 7);
            string password = "12345";
            string repassword = "12346";
            Assert.AreEqual(false, dao.UpdateKHtestcase(test.IdKhachHang, test, password, repassword));
        }


        [TestMethod()]
        public void LoginTest1()//login duoc
        {
            string testname = "KHtest4";
            string testpass = "123";
            Assert.AreEqual(3, dao.Login(testname, testpass));
        }

        [TestMethod()]
        public void LoginTest2()//sai username
        {
            string testname = "KHtet4";
            string testpass = "123";
            Assert.AreEqual(1, dao.Login(testname, testpass));
        }

        [TestMethod()]
        public void LoginTest3()//sai password
        {
            string testname = "KHtest4";
            string testpass = "124";
            Assert.AreEqual(1, dao.Login(testname, testpass));
        }
        [TestMethod()]
        public void LoginTest4()//login vao tk khoa
        {
            string testname = "KHtest3";
            string testpass = "123";
            Assert.AreEqual(2, dao.Login(testname, testpass));
        }
    }
}