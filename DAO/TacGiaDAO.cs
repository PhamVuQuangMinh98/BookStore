using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class TacGiaDAO
    {
        ToyContext db = null;
        public TacGiaDAO()
        {
            db = new ToyContext();
        }
        public List<TacGia> GetList()
        {

            return db.TacGias.Where(i => i.flag == true).ToList();
        }

        public List<TacGia> Search(string tenTG)
        {
            if (String.IsNullOrEmpty(tenTG))
            {
                return db.TacGias.Where(i => i.flag == true).ToList();
            }
            else
            {
                return db.TacGias.Where(i => i.flag == true && i.Ten.Contains(tenTG)).ToList();
            }
        }

        public TacGia Find(long idTacGia)
        {
            return db.TacGias.Where(i => i.IdTacGia == idTacGia).FirstOrDefault();
        }

        public void UpdateTG(long idTacGia, TacGia tg, string moTa,HttpPostedFileBase hinhAnh)
        {
            var newTG = db.TacGias.Find(idTacGia);
            newTG.Ten = tg.Ten;
            newTG.MoTa = moTa;

            string path = "";
            if (hinhAnh != null)
            {
                if (hinhAnh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(hinhAnh.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    hinhAnh.SaveAs(path);
                    //path= path.Replace("\","/");
                    newTG.HinhAnh = fileName;
                }
            }
            db.SaveChanges();
        }
        public void InsertTG(TacGia tg,string moTa, HttpPostedFileBase hinhAnh)
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
            } while (db.TacGias.Find(Convert.ToInt64(id)) != null);
            TacGia newTG = new TacGia();
            newTG.IdTacGia = Convert.ToInt64(id);
            newTG.Ten = tg.Ten;
            newTG.MoTa = moTa;

            string path = "";
            if (hinhAnh != null)
            {
                if (hinhAnh.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(hinhAnh.FileName);
                    path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    hinhAnh.SaveAs(path);
                    //path= path.Replace("\","/");
                    newTG.HinhAnh = fileName;
                }
                else
                {
                    newTG.HinhAnh = "img";
                }
            }
            else
            {
                newTG.HinhAnh = "img";
            }

            newTG.flag = true;
            db.TacGias.Add(newTG);
            db.SaveChanges();


        }
        public bool DeleteTG(long idTacgia)
        {
            if (db.Sachs.Where(i => i.flag == true && i.IdTacGia == idTacgia).Count() > 0)
            {
                return false;
            }
            else
            {
                db.TacGias.Find(idTacgia).flag = false;
                db.SaveChanges();
                return true;
            }
        }
    }
}