using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class NhaCungCapDAO
    {

        ToyContext db = null;
        public NhaCungCapDAO()
        {
            db = new ToyContext();
        }
        public List<NhaCungCap> GetList()
        {
            return db.NhaCungCaps.Where(i => i.flag == true).ToList();
        }

        public List<NhaCungCap> Search(string tenNCC)
        {
            if (String.IsNullOrEmpty(tenNCC))
            {
                return db.NhaCungCaps.Where(i => i.flag == true).ToList();
            }
            else
            {
                return db.NhaCungCaps.Where(i => i.flag == true && i.Ten.Contains(tenNCC)).ToList();
            }
        }

        public NhaCungCap Find(long idNCC)
        {
            return db.NhaCungCaps.Where(i => i.IdNCC == idNCC).FirstOrDefault();
        }

        public void UpdateNCC(long idNCC, NhaCungCap ncc, string moTa, HttpPostedFileBase hinhAnh)
        {
            var newNCC = db.NhaCungCaps.Find(idNCC);
            newNCC.Ten = ncc.Ten;
            newNCC.MoTa = moTa;

            string path = "";
            if (hinhAnh != null)
            {
                if (hinhAnh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(hinhAnh.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    hinhAnh.SaveAs(path);
                    //path= path.Replace("\","/");
                    newNCC.HinhAnh = fileName;
                }
            }
            db.SaveChanges();
        }
        public bool UpdateNCCtestcase(long idNCC, NhaCungCap ncc)
        {
            try
            {
                var newNCC = db.NhaCungCaps.Find(idNCC);
                newNCC.Ten = ncc.Ten;
                newNCC.MoTa = ncc.MoTa;
                newNCC.HinhAnh = null;
                db.NhaCungCaps.Add(newNCC);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void InsertNCC(NhaCungCap ncc, string moTa, HttpPostedFileBase hinhAnh)
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
            } while (db.NhaCungCaps.Find(Convert.ToInt64(id)) != null);
            NhaCungCap newNCC = new NhaCungCap();
            newNCC.IdNCC = Convert.ToInt64(id);
            newNCC.Ten = ncc.Ten;
            newNCC.MoTa = moTa;

            string path = "";
            if (hinhAnh != null)
            {
                if (hinhAnh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(hinhAnh.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    hinhAnh.SaveAs(path);
                    //path= path.Replace("\","/");
                    newNCC.HinhAnh = fileName;
                }
                else
                {
                    newNCC.HinhAnh = "img";
                }
            }
            else
            {
                newNCC.HinhAnh = "img";
            }

            newNCC.flag = true;
            db.NhaCungCaps.Add(newNCC);
            db.SaveChanges();
        }
        public bool InsertNCCtestcase(NhaCungCap ncc)
        {
            try
            {
                var newNCC = new NhaCungCap();
                newNCC.Ten = ncc.Ten;
                newNCC.MoTa = ncc.MoTa;
                newNCC.HinhAnh = null;
                db.NhaCungCaps.Add(newNCC);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteNCC(long idNCC)
        {
            if (db.Sachs.Where(i => i.flag == true && i.IdNCC == idNCC).Count() > 0)
            {
                return false;
            }
            else
            {
                db.NhaCungCaps.Find(idNCC).flag = false;
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteNCCtestcase(long idNCC)
        {
            if (db.Sachs.Where(i => i.flag == true && i.IdNCC == idNCC).Count() > 0)
            {
                return false;
            }
            else
            {
                db.NhaCungCaps.Find(idNCC).flag = false;
                return true;
            }
        }

    }
}