using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class KhuyenMaiDAO
    {
        ToyContext db = null;
        public KhuyenMaiDAO()
        {
            db = new ToyContext();
        }
        public List<KhuyenMai> GetList()
        {
            return db.KhuyenMais.Where(i => i.flag == true && i.IdKhuyenMai != 1).ToList();
        }
        public List<KhuyenMai> GetListKM()
        {
            DateTime today = DateTime.Today;
            return db.KhuyenMais.Where(i => i.flag == true && i.IdKhuyenMai != 1 && i.NgayKT >= today).ToList();
        }

        public List<KhuyenMai> Search(string tenKM, DateTime? ngayBD, DateTime? ngayKT)
        {
            if (String.IsNullOrEmpty(tenKM) && ngayBD == null && ngayKT == null)
            {
                return db.KhuyenMais.Where(i => i.flag == true && i.IdKhuyenMai != 1).ToList();
            }
            else
            {
                if (tenKM != null && ngayBD == null && ngayKT == null)
                {
                    return db.KhuyenMais.Where(i => i.flag == true && i.TenKhuyenMai.Contains(tenKM) && i.IdKhuyenMai != 1).ToList();
                }
                else if (tenKM == null && ngayBD != null && ngayKT == null)
                {
                    return db.KhuyenMais.Where(i => i.flag == true && i.NgayBD == ngayBD && i.IdKhuyenMai != 1).ToList();
                }
                else if (tenKM == null && ngayBD == null && ngayKT != null)
                {
                    return db.KhuyenMais.Where(i => i.flag == true && i.NgayKT == ngayKT && i.IdKhuyenMai != 1).ToList();
                }
                else if (tenKM != null && ngayBD != null && ngayKT == null)
                {
                    return db.KhuyenMais.Where(i => i.flag == true && i.NgayBD == ngayBD && i.TenKhuyenMai.Contains(tenKM) && i.IdKhuyenMai != 1).ToList();
                }
                else if (tenKM != null && ngayBD == null && ngayKT != null)
                {
                    return db.KhuyenMais.Where(i => i.flag == true && i.NgayKT == ngayKT && i.TenKhuyenMai.Contains(tenKM) && i.IdKhuyenMai != 1).ToList();
                }
                else if (tenKM == null && ngayBD != null && ngayKT != null)
                {
                    return db.KhuyenMais.Where(i => i.flag == true && i.NgayKT == ngayKT && i.NgayBD == ngayBD && i.IdKhuyenMai != 1).ToList();
                }
                else
                {
                    return db.KhuyenMais.Where(i => i.flag == true && i.NgayKT == ngayKT && i.NgayBD == ngayBD && i.TenKhuyenMai.Contains(tenKM) && i.IdKhuyenMai != 1).ToList();
                }
            }
        }

        public KhuyenMai Find(long idKM)
        {
            return db.KhuyenMais.Where(i => i.IdKhuyenMai == idKM).FirstOrDefault();
        }

        public int UpdateKM(long idKM, KhuyenMai km, HttpPostedFileBase hinhAnh, double phanTram)
        {
            if (km.NgayKT <= km.NgayBD)
            {
                return 0;
            }
            else
            {

                KhuyenMai newKM = db.KhuyenMais.Find(idKM);
                newKM.TenKhuyenMai = km.TenKhuyenMai;
                newKM.PhanTram = phanTram;
                newKM.NgayKT = km.NgayKT;
                newKM.flag = true;
                string path = "";
                if (hinhAnh != null)
                {
                    if (hinhAnh.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(hinhAnh.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                        hinhAnh.SaveAs(path);
                        //path= path.Replace("\","/");
                        newKM.HinhKhuyenMai = fileName;
                    }
                }
                db.SaveChanges();
                return 2;
            }
        }
        public int UpdateKMtestcase(long idKM, KhuyenMai km, HttpPostedFileBase hinhAnh, double phanTram)
        {
            if (km.NgayKT <= km.NgayBD)
            {
                return 0;
            }
            else
            {

                KhuyenMai newKM = db.KhuyenMais.Find(idKM);
                newKM.TenKhuyenMai = km.TenKhuyenMai;
                newKM.PhanTram = phanTram;
                newKM.NgayKT = km.NgayKT;
                newKM.flag = true;
                string path = "";
                if (hinhAnh != null)
                {
                    if (hinhAnh.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(hinhAnh.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                        hinhAnh.SaveAs(path);
                        //path= path.Replace("\","/");
                        newKM.HinhKhuyenMai = fileName;
                    }
                }
                //db.SaveChanges();
                return 1;
            }
        }
        public int InsertKM(KhuyenMai km, HttpPostedFileBase hinhAnh, double phanTram)
        {
            if (km.NgayKT <= km.NgayBD)
            {
                return 0;
            }
            else if (km.NgayBD < DateTime.Today)
            {
                return 1;
            }
            else
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
                } while (db.KhuyenMais.Find(Convert.ToInt64(id)) != null);
                KhuyenMai newKM = new KhuyenMai();
                newKM.IdKhuyenMai = Convert.ToInt64(id);
                newKM.TenKhuyenMai = km.TenKhuyenMai;
                newKM.PhanTram = phanTram;
                newKM.NgayBD = km.NgayBD;
                newKM.NgayKT = km.NgayKT;
                newKM.flag = true;
                string path = "";
                if (hinhAnh != null)
                {
                    if (hinhAnh.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(hinhAnh.FileName);
                        path = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                        hinhAnh.SaveAs(path);
                        //path= path.Replace("\","/");
                        newKM.HinhKhuyenMai = fileName;
                    }
                    else
                    {
                        newKM.HinhKhuyenMai = "img";
                    }
                }
                else
                {
                    newKM.HinhKhuyenMai = "img";
                }
                db.KhuyenMais.Add(newKM);
                db.SaveChanges();
                return 2;
            }

        }
        public bool InsertKMtestcase(KhuyenMai km, long id)
            {
            try
            {
                if (db.KhuyenMais.Any(i => i.IdKhuyenMai == id) == true)
                    return false;
                KhuyenMai newKM = new KhuyenMai();
                newKM.TenKhuyenMai = km.TenKhuyenMai;
                newKM.PhanTram = km.PhanTram;
                newKM.NgayBD = km.NgayBD;
                newKM.NgayKT = km.NgayKT;
                newKM.flag = true;
                newKM.HinhKhuyenMai = null;
                if (newKM.NgayBD >= newKM.NgayKT)
                    return false;
                else
                {
                    db.KhuyenMais.Add(newKM);
                    //db.SaveChanges();
                    return true;
                }                                        
            }
            catch
            { return false; }
            }
        public void DeleteKM(long idKM)
        {
            foreach (var item in db.Sachs.Where(i=>i.flag==true && i.IdKhuyenMai==idKM).ToList())
            {
                
                var sach = db.Sachs.Find(item.ID);
                sach.IdKhuyenMai =Convert.ToInt64(8);
                db.SaveChanges();
            }
            db.KhuyenMais.Remove(db.KhuyenMais.Find(idKM));
            db.SaveChanges();
        }

        public bool DeleteKMTestCase(long idKhuyenMai)
        {
            if (db.Sachs.Where(i => i.flag == true && i.IdKhuyenMai == idKhuyenMai).Count() > 0)
            {
                return false;
            }
            else
            {
                db.KhuyenMais.Find(idKhuyenMai).flag = false;
                //db.SaveChanges();
                return true;
            }
        }
    }
}