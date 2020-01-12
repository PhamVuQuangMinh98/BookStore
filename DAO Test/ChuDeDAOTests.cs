using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication23.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO.Tests
{
    [TestClass()]
    public class ChuDeDAOTests
    {
        ToyContext db = null;
        ChuDeDAO dao = new ChuDeDAO();

        [TestMethod()]
        public void GetListTrongNuocTest()//So luong In
        {
            Assert.AreEqual(3, dao.GetListTrongNuoc().Count);
        }

        [TestMethod()]
        public void GetListNgoaiNuocTest()//So luong Out
        {
            Assert.AreEqual(2, dao.GetListNgoaiNuoc().Count);
        }

        [TestMethod()]
        public void GetListTest()//So luong tat ca
        {
            Assert.AreEqual(5, dao.GetList().Count);
        }

        [TestMethod()]
        public void SearchTest1()//2 gia tri null
        {
            Assert.AreEqual(5, dao.Search(null, "0").Count());
        }
        [TestMethod()]
        public void SearchTest2()//xuat xu null
        {
            Assert.AreEqual(2, dao.Search("CDtest1", "0").Count());
        }
        [TestMethod()]
        public void SearchTest3()// ten null
        {
            Assert.AreEqual(3, dao.Search(null, "In").Count());
        }
        [TestMethod()]
        public void SearchTest4()//ca 2 thuoc tinh dung
        {
            Assert.AreEqual(1, dao.Search("CDtest2", "In").Count());
        }
        [TestMethod()]
        public void SearchTest5()//doi tuong false
        {
            Assert.AreEqual(0, dao.Search("CDtest5", "Out").Count());
        }
        [TestMethod()]
        public void SearchTest6()//1 trong 2 thuoc tinh dung
        {
            Assert.AreEqual(0, dao.Search("CDtest3", "Out").Count());
        }

        [TestMethod()]
        public void FindTest()// lay ra 1 doi tuong va so sanh ten
        {
            ChuDe test = new ChuDe();
            test.IdChuDe = 3;
            test.Ten = "CDtest3";
            test.XuatXu = "In";
            test.flag = true;
            Assert.AreEqual(test.Ten, dao.Find(3).Ten);
        }

        [TestMethod()]
        public void UpdateCDTest()
        {
            ChuDe test = new ChuDe();
            test.IdChuDe = 3;
            test.Ten = "BBBBBBB";
            test.XuatXu = "Out";
            test.flag = true;
            Assert.AreEqual(true, dao.UpdateCDTestCase(test, test.IdChuDe));
        }

        [TestMethod()]
        public void InsertCDTest()
        {
            ChuDe test = new ChuDe();
            test.Ten = "AAAAAAA";
            test.XuatXu = "In";
            test.flag = true;
            Assert.AreEqual(true, dao.InsertCDTestCase(test,test.IdChuDe));
        }

        [TestMethod()]
        public void DeleteCDTest1()// Xoa doi tuong ton tai
        {
            ChuDe test = new ChuDe();
            test.IdChuDe = 4;
            Assert.AreEqual(true, dao.DeleteCDTestCase(test.IdChuDe));
        }

        [TestMethod()]
        public void DeleteCDTest2()// Xoa chu de ma sach con
        {
            long idChuDe = 1;
            Assert.AreEqual(false, dao.DeleteCDTestCase(idChuDe));
        }
    }
}