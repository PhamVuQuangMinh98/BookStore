using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication23.Areas.Admin.Models;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class HomeDAO
    {
        ToyContext db = null;
        public HomeDAO()
        {
            db = new ToyContext();
        }
        public List<Sach> GetList()
        {
            return db.Sachs.Where(i => i.flag == true).ToList();
        }
        public List<Sach> GetListSachMoi()
        {
            return db.Sachs.Where(i => i.flag == true).OrderByDescending(i => i.NamXuatBan).Take(10).ToList();
        }
        public List<Sach> GetListSachMoiAll()
        {
            return db.Sachs.Where(i => i.flag == true).OrderByDescending(i => i.NamXuatBan).ToList();
        }
        public List<Sach> GetListNoiBatAll()
        {
            var nb = new BieuDoDAO();
            List<Sach> listSach = new List<Sach>();
            List<SachVM> listSachVM = nb.GetListSachNoiBatAll();
            foreach (var item in listSachVM)
            {
                listSach.Add(db.Sachs.Find(item.IdSach));
            }
            return listSach;
        }
        public List<Sach> GetListTop4NoiBat()
        {
            var nb = new BieuDoDAO();
            List<Sach> listSach = new List<Sach>();
            List<SachVM> listSachVM = nb.GetListSachNoiBat();
            foreach (var item in listSachVM)
            {
                listSach.Add(db.Sachs.Find(item.IdSach));
            }
            listSach = listSach.Take(4).ToList();
            return listSach;
        }
        public List<Sach> GetListNoiBat()
        {
            var nb = new BieuDoDAO();
            List<Sach> listSach = new List<Sach>();
            List<SachVM> listSachVM = nb.GetListSachNoiBat();
            foreach (var item in listSachVM)
            {
                listSach.Add(db.Sachs.Find(item.IdSach));
            }
            return listSach;
        }public List<Sach> GetListTop4TacGia(long idTacGia)
        {
            return db.Sachs.Where(i => i.flag == true && i.IdTacGia == idTacGia).Take(4).ToList();
        }
        public List<Sach> GetListXemNhieu()
        {
            return db.Sachs.Where(i => i.flag == true).OrderByDescending(i=>i.LuotXem).Take(10).ToList();
        }
        public List<Sach> GetListXemNhieuAll()
        {
            return db.Sachs.Where(i => i.flag == true).OrderByDescending(i => i.LuotXem).ToList();
        }
    }
}