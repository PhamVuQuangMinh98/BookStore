using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class LoginDAO
    {
        ToyContext db=null;
        public LoginDAO()
        {
            db = new ToyContext();
        }
        public KhachHang GetTK(string email, string password)
        {
            string pass = password;
            KhachHang kh = db.KhachHangs.Where(i => i.Username == email && i.Password == password && i.flag==true).FirstOrDefault();
            return kh;
        }
        public bool GetTKtestcase(string email, string password)
        {
            KhachHang kh = db.KhachHangs.Where(i => i.Username == email && i.Password == password && i.flag == true).FirstOrDefault();
            if(kh!=null)
            {
                return true;
            }
            return false;
        }
        public KhachHang UpdateTK(long idtaikhoan, string email, string newpassword, string newrepassword, string ho, string ten, DateTime ngaysinh, int gioitinh)
        {
            KhachHang kh = db.KhachHangs.Find(idtaikhoan);
            kh.Ho = ho;
            kh.Ten = ten;
            kh.NgaySinh = ngaysinh;
            if (gioitinh == 0)
            {
                kh.GioiTinh = true;
            }
            else
            {
                kh.GioiTinh = false;
            }
            if(String.IsNullOrEmpty(newpassword)==false && String.IsNullOrEmpty(newrepassword)==false && newpassword == newrepassword)
            {
                kh.Password = newpassword;
            }
            db.SaveChanges();
            return kh;
        }
        public bool UpdateKHtestcase(long idTaiKhoan,KhachHang khachhang, string newpassword, string repassword)
        {
            var kh = db.KhachHangs.Find(idTaiKhoan);
            if(kh==null)
            {
                return false;
            }
            else
            {
                kh.Ho = khachhang.Ho;
                kh.Ten = khachhang.Ten;
                kh.NgaySinh = khachhang.NgaySinh;
                kh.GioiTinh = khachhang.GioiTinh;
                if (String.IsNullOrEmpty(newpassword) == false && String.IsNullOrEmpty(repassword) == false && newpassword == repassword)
                {
                    kh.Password = newpassword;
                }
                else return false;
                return true;
            }
        }
        public int Login(string email,string password)
        {
            string pass = password;
            var kh = db.KhachHangs.Where(i => i.Username == email && i.Password == password).FirstOrDefault();
            if (kh == null)
            {
                return 1;
            }
            else
            {
                if (kh.flag == false){
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }
    }
}