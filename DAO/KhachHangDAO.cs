using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class KhachHangDAO
    {
        ToyContext db = null;
        public KhachHangDAO()
        {
            db = new ToyContext();
        }
        public List<KhachHang> GetList()
        {
            return db.KhachHangs.Where(i => i.flag == true && i.LoaiTK=="KH").ToList();
        }
        public List<KhachHang> GetListBlock()
        {
            return db.KhachHangs.Where(i => i.flag == false && i.LoaiTK == "KH").ToList();
        }
        public List<KhachHang> SearchBlock(string email, string gioiTinh,DateTime? ngaySinh)
        {
            if(String.IsNullOrEmpty(email)&&ngaySinh==null&& gioiTinh=="0")
            {
                return db.KhachHangs.Where(i =>i.flag == false && i.LoaiTK == "KH").ToList();
            }
            else
            {
                bool? sex=null;
                if (gioiTinh == "nam") {
                    sex = true;
                }
                else if (gioiTinh == "nu")
                {
                    sex = false;
                }

                if(email!=null &&ngaySinh==null && sex==null )
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.flag == false && i.LoaiTK=="KH").ToList();
                }
                else if(email==null && ngaySinh !=null && sex==null )
                {
                    return db.KhachHangs.Where(i => i.NgaySinh == ngaySinh && i.flag == false && i.LoaiTK == "KH").ToList();
                }
                else if(email == null && ngaySinh == null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == false && i.LoaiTK == "KH").ToList();
                }
                else if(email!=null && ngaySinh !=null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.flag == false && i.NgaySinh==ngaySinh && i.LoaiTK == "KH").ToList();
                }
                else if (email != null && ngaySinh == null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == false && i.Username.Contains(email) && i.LoaiTK == "KH").ToList();
                }
                else if (email == null && ngaySinh != null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == false && i.NgaySinh==ngaySinh && i.LoaiTK == "KH").ToList();
                }
                else
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.GioiTinh == sex && i.flag == false && i.NgaySinh == ngaySinh && i.LoaiTK == "KH").ToList();
                }


            }        
        }
        public List<KhachHang> Search(string email, string gioiTinh, DateTime? ngaySinh)
        {
            if (String.IsNullOrEmpty(email) && ngaySinh == null && gioiTinh == "0")
            {
                return db.KhachHangs.Where(i => i.flag == true && i.LoaiTK == "KH").ToList();
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
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.flag == true && i.LoaiTK == "KH").ToList();
                }
                else if (email == null && ngaySinh != null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.NgaySinh == ngaySinh && i.flag == true && i.LoaiTK == "KH").ToList();
                }
                else if (email == null && ngaySinh == null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == true && i.LoaiTK == "KH").ToList();
                }
                else if (email != null && ngaySinh != null && sex == null)
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.flag == true && i.NgaySinh == ngaySinh && i.LoaiTK == "KH").ToList();
                }
                else if (email != null && ngaySinh == null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == true && i.Username.Contains(email) && i.LoaiTK == "KH").ToList();
                }
                else if (email == null && ngaySinh != null && sex != null)
                {
                    return db.KhachHangs.Where(i => i.GioiTinh == sex && i.flag == true && i.NgaySinh == ngaySinh && i.LoaiTK == "KH").ToList();
                }
                else
                {
                    return db.KhachHangs.Where(i => i.Username.Contains(email) && i.GioiTinh == sex && i.flag == true && i.NgaySinh == ngaySinh && i.LoaiTK == "KH").ToList();
                }
            }
        }
        public KhachHang FindTK(string username)
        {
            return db.KhachHangs.Where(i => i.Username == username).FirstOrDefault();
        }
        public KhachHang Find(long idKhachHang)
        {
            return db.KhachHangs.Where(i=>i.IdKhachHang==idKhachHang).FirstOrDefault();
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

        public bool UpdateKHtestcase(long idKhachHang, KhachHang khachhang)
        {
            var kh = db.KhachHangs.Find(idKhachHang);
            if (kh == null)
            {
                return false;
            }
            else
            {
                kh.Ho = khachhang.Ho;
                kh.Ten = khachhang.Ten;
                kh.NgaySinh = khachhang.NgaySinh;
                kh.GioiTinh = khachhang.GioiTinh;
                return true;
            }
        }
            public void InsertKHHome(string email, string ho, string ten, DateTime ngaysinh, int gender,string password)
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
            kh.LoaiKH = "BT";
            kh.Password = password;
            if (gender == 0)
            {
                kh.GioiTinh = true;
            }
            else
            {
                kh.GioiTinh = false;
            }
            kh.LoaiTK = "KH";

            db.KhachHangs.Add(kh);
            db.SaveChanges();
        }
        public bool InsertKHhomeTestcase(KhachHang kh)
        {
            try
            {
                KhachHang newKH = new KhachHang();
                newKH.Username = kh.Username;
                newKH.Password = kh.Password;
                newKH.Ho = kh.Ho;
                newKH.Ten = kh.Ten;
                newKH.GioiTinh = kh.GioiTinh;
                newKH.NgaySinh = kh.NgaySinh;
                db.KhachHangs.Add(newKH);
                return true;
            }
            catch
            {
                return false;
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
            kh.LoaiKH = "BT";
            kh.Password = "1";
            kh.GioiTinh = gender;
            kh.LoaiTK = "KH";


            db.KhachHangs.Add(kh);
            db.SaveChanges();
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
    }
}