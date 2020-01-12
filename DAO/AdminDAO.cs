using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication23.Models;


namespace WebApplication23.DAO
{
    public class AdminDAO
    {
        ToyContext db = null;
        public AdminDAO()
        {
            db = new ToyContext();
        }
        public List<KhachHang> GetList(long idAdmin)
        {
            return db.KhachHangs.Where(i => i.flag == true && i.LoaiTK == "Admin" && i.IdKhachHang!=idAdmin).ToList();
        }
        public List<KhachHang> GetListBlock()
        {
            return db.KhachHangs.Where(i => i.flag == false && i.LoaiTK == "Admin").ToList();
        }
        public List<KhachHang> SearchBlock(string email, string gioiTinh, DateTime? ngaySinh)
        {
            if (String.IsNullOrEmpty(email) && ngaySinh == null && gioiTinh == "0")
            {
                return db.KhachHangs.Where(i => i.flag == false && i.LoaiTK == "Admin").ToList();
            }
            else
            {
                bool? sex = null;
                if (gioiTinh == "nam")
                {
                    sex = true;
                }
                else if (gioiTinh == "nu")
                {
                    sex = false;
                }

                if (email != null && ngaySinh == null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.flag == false && i.LoaiTK == "Admin").ToList();
                }
                else if (email == null && ngaySinh != null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.NgaySinh == ngaySinh && i.flag == false && i.LoaiTK == "Admin").ToList();
                }
                else if (email == null && ngaySinh == null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == false && i.LoaiTK == "Admin").ToList();
                }
                else if (email != null && ngaySinh != null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.flag == false && i.NgaySinh == ngaySinh && i.LoaiTK == "Admin").ToList();
                }
                else if (email != null && ngaySinh == null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == false && i.Username.Contains(email) && i.LoaiTK == "Admin").ToList();
                }
                else if (email == null && ngaySinh != null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == false && i.NgaySinh == ngaySinh && i.LoaiTK == "Admin").ToList();
                }
                else
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.GioiTinh == sex && i.flag == false && i.NgaySinh == ngaySinh && i.LoaiTK == "Admin").ToList();
                }


            }
        }
        public List<KhachHang> Search(string email, string gioiTinh, DateTime? ngaySinh,long idAdmin)
        {
            if (String.IsNullOrEmpty(email) && ngaySinh == null && gioiTinh == "0")
            {
                return db.KhachHangs.Where(i => i.flag == true && i.LoaiTK == "Admin" && i.IdKhachHang!=idAdmin).ToList();
            }
            else
            {
                bool? sex = null;
                if (gioiTinh == "nam")
                {
                    sex = true;
                }
                else if (gioiTinh == "nu")
                {
                    sex = false;
                }

                if (email != null && ngaySinh == null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.IdKhachHang != idAdmin && i.flag == true && i.LoaiTK == "Admin").ToList();
                }
                else if (email == null && ngaySinh != null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.NgaySinh == ngaySinh && i.IdKhachHang != idAdmin && i.flag == true && i.LoaiTK == "Admin").ToList();
                }
                else if (email == null && ngaySinh == null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == true && i.IdKhachHang != idAdmin && i.LoaiTK == "Admin").ToList();
                }
                else if (email != null && ngaySinh != null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.flag == true && i.IdKhachHang != idAdmin && i.NgaySinh == ngaySinh && i.LoaiTK == "Admin").ToList();
                }
                else if (email != null && ngaySinh == null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == true && i.IdKhachHang != idAdmin && i.Username.Contains(email) && i.LoaiTK == "Admin").ToList();
                }
                else if (email == null && ngaySinh != null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == true && i.IdKhachHang != idAdmin && i.NgaySinh == ngaySinh && i.LoaiTK == "Admin").ToList();
                }
                else
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.GioiTinh == sex && i.IdKhachHang != idAdmin && i.flag == true && i.NgaySinh == ngaySinh && i.LoaiTK == "Admin").ToList();
                }
            }
        }
        public KhachHang FindTK(string username)
        {
            return db.KhachHangs.Where(i => i.Username == username).FirstOrDefault();
        }
        public bool FindTKtestcase(string username)
        {
            if (db.KhachHangs.Where(i => i.Username == username).FirstOrDefault() == null)
            {
                return false;
            }
            else return true;
        }
        public KhachHang Find(long idKhachHang)
        {
            return db.KhachHangs.Where(i => i.IdKhachHang == idKhachHang).FirstOrDefault();
        }
        public bool Findtestcase(long idKhachHang)
        {
            if (db.KhachHangs.Where(i => i.IdKhachHang == idKhachHang).FirstOrDefault() == null)
            {
                return false;
            }
            else return true;
        }
        public bool Login(string username, string password)
        {
            int count = db.KhachHangs.Where(i => i.Username == username && i.Password == password).Count();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void UpdateKH(long idKhachHang, KhachHang khachHang)
        {
            var kh = db.KhachHangs.Find(idKhachHang);
            kh.Ho = khachHang.Ho;
            kh.Ten = khachHang.Ten;
            kh.NgaySinh = khachHang.NgaySinh;
            kh.GioiTinh = khachHang.GioiTinh;
            db.SaveChanges();
        }
        public bool UpdateKHtestcase(KhachHang khachHang, long idKhachHang)
        {
            var kh = db.KhachHangs.Find(idKhachHang);
            if (kh == null)
            {
                return false;
            }
            else
            {
                kh.Ho = khachHang.Ho;
                kh.Ten = khachHang.Ten;
                kh.NgaySinh = khachHang.NgaySinh;
                kh.GioiTinh = khachHang.GioiTinh;
                //db.SaveChanges();
                return true;
            }
        }
        public void InsertKH(string email, string ho, string ten, DateTime ngaysinh, bool gender)
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
            } while (db.KhachHangs.Find(Convert.ToInt64(id)) != null);

            KhachHang kh = new KhachHang();
            kh.IdKhachHang = Convert.ToInt64(id);
            kh.DiemTichLuy = 0;
            kh.Username = email;
            kh.flag = true;
            kh.Ho = ho;
            kh.Ten = ten;
            kh.NgaySinh = ngaysinh;
            kh.LoaiKH = "";
            kh.Password = "1";
            kh.GioiTinh = gender;
            kh.LoaiTK = "Admin";

            db.KhachHangs.Add(kh);
            db.SaveChanges();
        }
        public bool InsertKHtestcase(KhachHang kh,long id)
        {
            try
            {
                if (db.KhachHangs.Any(i => i.IdKhachHang == id) == true)
                    return false;
                else
                {
                    KhachHang newKH = new KhachHang();
                    newKH.Username = kh.Username;
                    newKH.Password = kh.Password;
                    newKH.Ho = kh.Ho;
                    newKH.Ten = kh.Ten;
                    newKH.GioiTinh = kh.GioiTinh;
                    newKH.NgaySinh = kh.NgaySinh;
                    db.KhachHangs.Add(newKH);
                    //db.SaveChanges();
                    return true;
                }
            }catch
            {
                return false;
            }
        }
        public void BlockKH(long idKhachHang)
        {
            db.KhachHangs.Find(idKhachHang).flag = false;
            db.SaveChanges();
        }
        public void UnblockKH(long idKhachHang)
        {
            db.KhachHangs.Find(idKhachHang).flag = true;
            db.SaveChanges();
        }

        public bool BlockKHtestcase(long idKhachHang)
        {           
            if(db.KhachHangs.Find(idKhachHang).flag==true)
            {
                db.KhachHangs.Find(idKhachHang).flag = false;
                return true;
            }
            return false;
        }
        public bool UnblockKHtestcase(long idKhachHang)
        {
            if (db.KhachHangs.Find(idKhachHang).flag == false)
            {
                db.KhachHangs.Find(idKhachHang).flag = true;
                return true;
            }
            return false;
        }
    }
}