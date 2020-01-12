using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class ChuDeDAO
    {
        ToyContext db = null;
        public ChuDeDAO()
        {
            db = new ToyContext();
        }
        public List<ChuDe> GetListTrongNuoc()
        {
            return db.ChuDes.Where(i => i.flag == true && i.XuatXu=="In").ToList();
        }
        public List<ChuDe> GetListNgoaiNuoc()
        {
            return db.ChuDes.Where(i => i.flag == true && i.XuatXu == "Out").ToList();
        }
        public List<ChuDe> GetList()
        {

            return db.ChuDes.Where(i => i.flag == true).ToList();
        }
      
        public List<ChuDe> Search(string tenCD,string xuatXu)
        {
            if (String.IsNullOrEmpty(tenCD) && xuatXu == "0")
            {
                return db.ChuDes.Where(i => i.flag == true).ToList();
            }
            else
            {
                if(tenCD!=null && xuatXu == "0")
                {
                    return db.ChuDes.Where(i => i.flag == true && i.Ten.Contains(tenCD)).ToList();
                }
                else if(tenCD==null && xuatXu != "0")
                {
                    return db.ChuDes.Where(i => i.flag == true && i.XuatXu==xuatXu).ToList();
                }
                else
                {
                    return db.ChuDes.Where(i => i.flag == true && i.Ten.Contains(tenCD) && i.XuatXu == xuatXu).ToList();
                }
            }
        }

        public ChuDe Find(long idChuDe)
        {
            return db.ChuDes.Where(i => i.IdChuDe == idChuDe).FirstOrDefault();
        }
     
        public void UpdateCD(long idChuDe, ChuDe chuDe)
        {
            var cd = db.ChuDes.Find(idChuDe);
            cd.Ten = chuDe.Ten;
            cd.XuatXu = chuDe.XuatXu;
            db.SaveChanges();
        }
        public bool UpdateCDTestCase(ChuDe chuDe, long idChuDe)
        {
            var cd = db.ChuDes.Find(idChuDe);
            if(cd==null)
            {
                return false;
            }
            else
            {
                cd.Ten = chuDe.Ten;
                cd.XuatXu = chuDe.XuatXu;
                //db.SaveChanges();
                return true;
            }
        }
        public void InsertCD(ChuDe cd)
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
            } while (db.ChuDes.Find(Convert.ToInt64(id)) != null);
            ChuDe newCD = new ChuDe();
            newCD.IdChuDe =Convert.ToInt64(id);
            newCD.Ten = cd.Ten;
            newCD.XuatXu = cd.XuatXu;
            newCD.flag = true;
            db.ChuDes.Add(newCD);
            db.SaveChanges();
        }

        public bool InsertCDTestCase(ChuDe cd, long id)
        {
            try
            {
                if (db.ChuDes.Any(i => i.IdChuDe == id) == true)
                    return false;
                ChuDe newCD = new ChuDe();
                newCD.Ten = cd.Ten;
                newCD.XuatXu = cd.XuatXu;
                newCD.flag = true;
                db.ChuDes.Add(newCD);
                //db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool DeleteCD(long idChuDe)
        {
            if (db.Sachs.Where(i => i.flag == true && i.IdChuDe == idChuDe).Count() >0)
            {
                return false;
            }
            else {
                db.ChuDes.Find(idChuDe).flag = false;
                db.SaveChanges();
                return true;
            }
        }

        public bool DeleteCDTestCase(long idChuDe)
        {
            if (db.Sachs.Where(i => i.flag == true && i.IdChuDe == idChuDe).Count() > 0)
            {
                return false;
            }
            else
            {
                db.ChuDes.Find(idChuDe).flag = false;
                //db.SaveChanges();
                return true;
            }
        }
       
    }
}