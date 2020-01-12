using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class NhaXuatBanDAO
    {
        ToyContext db = null;
        public NhaXuatBanDAO()
        {
            db = new ToyContext();
        }
        public List<NhaXuatBan> GetList()
        {

            return db.NhaXuatBans.Where(i => i.flag == true).ToList();
        }

        public List<NhaXuatBan> Search(string tenNXB)
        {
            if (String.IsNullOrEmpty(tenNXB))
            {
                return db.NhaXuatBans.Where(i => i.flag == true).ToList();
            }
            else
            {
                return db.NhaXuatBans.Where(i => i.flag == true && i.Ten.Contains(tenNXB)).ToList();
            }
        }

        public NhaXuatBan Find(long idNXB)
        {
            return db.NhaXuatBans.Where(i => i.IdNXB == idNXB).FirstOrDefault();
        }

        public void UpdateNXB(long idNXB, NhaXuatBan nxb, string moTa, HttpPostedFileBase hinhAnh)
        {
            var newNXB = db.NhaXuatBans.Find(idNXB);
            newNXB.Ten = nxb.Ten;
            newNXB.MoTa = moTa;

            string path = "";
            if (hinhAnh != null)
            {
                if (hinhAnh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(hinhAnh.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    hinhAnh.SaveAs(path);
                    //path= path.Replace("\","/");
                    newNXB.HinhAnh = fileName;
                }
            }
            db.SaveChanges();
        }
        public void InsertNXB(NhaXuatBan nxb, string moTa, HttpPostedFileBase hinhAnh)
        {
            string id;

            do
            {
                id = "";
                for (int i = 0; i < 10; i++)
                {
                    Random rd = new Random();
                    id += rd.Next(0, 10).ToString();
                }
            } while (db.NhaXuatBans.Find(Convert.ToInt64(id)) != null);
            NhaXuatBan newNXB = new NhaXuatBan();
            newNXB.IdNXB = Convert.ToInt64(id);
            newNXB.Ten = nxb.Ten;
            newNXB.MoTa = moTa;

            string path = "";
            if (hinhAnh != null)
            {
                if (hinhAnh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(hinhAnh.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    hinhAnh.SaveAs(path);
                    //path= path.Replace("\","/");
                    newNXB.HinhAnh = fileName;
                }
                else
                {
                    newNXB.HinhAnh = "img";
                }
            }
            else
            {
                newNXB.HinhAnh = "img";
            }

            newNXB.flag = true;
            db.NhaXuatBans.Add(newNXB);
            db.SaveChanges();


        }
        public bool DeleteNXB(long idNXB)
        {
            if (db.Sachs.Where(i => i.flag == true && i.IdNXB == idNXB).Count() > 0)
            {
                return false;
            }
            else
            {
                db.NhaXuatBans.Find(idNXB).flag = false;
                db.SaveChanges();
                return true;
            }
        }
    }
}