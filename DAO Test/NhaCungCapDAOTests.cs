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
    public class NhaCungCapDAOTests
    {
        NhaCungCapDAO dao = new NhaCungCapDAO();
        [TestMethod()]
        public void GetListTest()//lay ncc khong khoa
        {
            Assert.AreEqual(2, dao.GetList().Count);
        }

        [TestMethod()]
        public void SearchTest1()//tim het
        {
            Assert.AreEqual(2, dao.Search(null).Count);
        }
        [TestMethod()]
        public void SearchTest2()//tim trung ten
        {
            Assert.AreEqual(1, dao.Search("NCCtest1").Count);
        }
        [TestMethod()]
        public void SearchTest3()//tim sai ten
        {
            Assert.AreEqual(0, dao.Search("NCtest1").Count);
        }

        [TestMethod()]
        public void UpdateNCCTest()
        {
            NhaCungCap test = new NhaCungCap();
            test.IdNCC = 2;
            test.Ten = "AAA";
            test.MoTa = "BBB";
            test.HinhAnh = null;
            Assert.AreEqual(true, dao.UpdateNCCtestcase(test.IdNCC, test));
        }

        [TestMethod()]
        public void InsertNCCTest()
        {
            NhaCungCap test = new NhaCungCap();
            test.Ten = "AAA";
            test.MoTa = "BBB";
            test.HinhAnh = null;
            Assert.AreEqual(true, dao.InsertNCCtestcase(test));
        }

        [TestMethod()]
        public void DeleteNCCTest()
        {
            Assert.AreEqual(false,dao.DeleteNCCtestcase(1));
        }
    }
}