using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WebApplication23.Models;

namespace WebApplication23.DAO
{
    public class SachDAO
    {
        ToyContext db = null;
        public SachDAO()
        {
            db = new ToyContext();
        }
        public List<Sach> GetListKM(long idKM)
        {
            return db.Sachs.Where(i => i.flag == true && i.IdKhuyenMai == idKM).ToList();
        }
        public List<Sach> GetListCD(long idCD)
        {
            return db.Sachs.Where(i => i.flag == true && i.IdChuDe == idCD).ToList();
        }
        public List<Sach> GetList()
        {

            return db.Sachs.Where(i => i.flag == true).ToList();
        }

        public List<Sach> Search(string name, long chude, long ncc, long nxb, long tg, long km)
        {
            List<Sach> list = db.Sachs.Where(i => i.flag == true).ToList();
            list = list.Where(i => i.Ten.Contains(name)).ToList();
            if (chude != 0)
            {
                list = list.Where(i => i.IdChuDe == chude).ToList();
            }
            if (ncc != 0)
            {
                list = list.Where(i => i.IdNCC == ncc).ToList();
            }
            if (nxb != 0)
            {
                list = list.Where(i => i.IdNXB == nxb).ToList();
            }
            if (tg != 0)
            {
                list = list.Where(i => i.IdTacGia == tg).ToList();
            }
            if (km != 0)
            {
                list = list.Where(i => i.IdKhuyenMai == km).ToList();
            }
            return list;
        }

        public Sach Find(long idSach)
        {
            return db.Sachs.Where(i => i.ID == idSach).FirstOrDefault();
        }

        public void UpdateSach(long idSach, Sach sach, HttpPostedFileBase anhBia, string chude, string ncc, string tg, string nxb, string km, HttpPostedFileBase anh1, HttpPostedFileBase anh2, double? chieuDai, double? chieuRong, double? chieuSau,string mota)
        {
            var newSach = db.Sachs.Find(idSach);
            newSach.Ten = sach.Ten;
            newSach.Gia = sach.Gia;
            newSach.SoLuong = sach.SoLuong;
            newSach.NamXuatBan = sach.NamXuatBan;
            newSach.IdChuDe = Convert.ToInt64(chude);
            newSach.ChuDe = db.ChuDes.Find(Convert.ToInt64(chude));
            newSach.IdNCC = Convert.ToInt64(ncc);
            newSach.NhaCungCap = db.NhaCungCaps.Find(Convert.ToInt64(ncc));
            newSach.IdNXB = Convert.ToInt64(nxb);
            newSach.NhaXuatBan = db.NhaXuatBans.Find(Convert.ToInt64(nxb));
            newSach.IdTacGia = Convert.ToInt64(tg);
            newSach.TacGia = db.TacGias.Find(Convert.ToInt64(tg));
            newSach.HinhThuc = sach.HinhThuc;
            newSach.MoTa = mota;
            newSach.NgonNgu = sach.NgonNgu;
            newSach.TrongLuong = sach.TrongLuong;
            newSach.SoTrang = sach.SoTrang;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            if (km != "0")
            {
                newSach.IdKhuyenMai = Convert.ToInt64(km);
                newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(km));
            }
            else
            {
                newSach.IdKhuyenMai = 1;
                newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(1));
            }
            GanAnhUpdate(newSach, anhBia, anh1, anh2);
            db.SaveChanges();
        }

        public bool UpdateSachtestcase(long idSach, Sach sach, string chude, string ncc, string tg, string nxb, string km)
        {
            try
            {
                var newSach = db.Sachs.Find(idSach);
                newSach.Ten = sach.Ten;
                newSach.Gia = sach.Gia;
                newSach.SoLuong = sach.SoLuong;
                newSach.NamXuatBan = sach.NamXuatBan;
                newSach.IdChuDe = Convert.ToInt64(chude);
                newSach.ChuDe = db.ChuDes.Find(Convert.ToInt64(chude));
                newSach.IdNCC = Convert.ToInt64(ncc);
                newSach.NhaCungCap = db.NhaCungCaps.Find(Convert.ToInt64(ncc));
                newSach.IdNXB = Convert.ToInt64(nxb);
                newSach.NhaXuatBan = db.NhaXuatBans.Find(Convert.ToInt64(nxb));
                newSach.IdTacGia = Convert.ToInt64(tg);
                newSach.TacGia = db.TacGias.Find(Convert.ToInt64(tg));
                newSach.HinhThuc = sach.HinhThuc;
                newSach.MoTa = sach.MoTa;
                newSach.NgonNgu = sach.NgonNgu;
                newSach.TrongLuong = sach.TrongLuong;
                newSach.SoTrang = sach.SoTrang;
                newSach.Anh1 = null;
                newSach.Anh2 = null;
                newSach.AnhBia = null;
                newSach.KickThuoc = sach.KickThuoc;
                if (km != "0")
                {
                    newSach.IdKhuyenMai = Convert.ToInt64(km);
                    newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(km));
                }
                else
                {
                    newSach.IdKhuyenMai = 1;
                    newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(1));
                }
                //db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
        }
        public void GanAnhUpdate(Sach sach, HttpPostedFileBase anhBia, HttpPostedFileBase anh1, HttpPostedFileBase anh2)
        {
            string pathAnhBia = "";
            if (anhBia != null)
            {
                if (anhBia.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anhBia.FileName);
                    pathAnhBia = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anhBia.SaveAs(pathAnhBia);
                    //path= path.Replace("\","/");
                    sach.AnhBia = fileName;
                }
            }
            string pathAnh1 = "";
            if (anh1 != null)
            {
                if (anh1.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh1.FileName);
                    pathAnh1 = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anh1.SaveAs(pathAnh1);
                    //path= path.Replace("\","/");
                    sach.Anh1 = fileName;
                }
            }


            string pathAnh2 = "";
            if (anh2 != null)
            {
                if (anh2.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh2.FileName);
                    pathAnh2 = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anh2.SaveAs(pathAnh2);
                    //path= path.Replace("\","/");
                    sach.Anh2 = fileName;
                }
            }
        }
        public void GanAnh(Sach sach, HttpPostedFileBase anhBia, HttpPostedFileBase anh1, HttpPostedFileBase anh2)
        {
            string pathAnhBia = "";
            if (anhBia != null)
            {
                if (anhBia.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anhBia.FileName);
                    pathAnhBia = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anhBia.SaveAs(pathAnhBia);
                    //path= path.Replace("\","/");
                    sach.AnhBia = fileName;
                }
                else
                {
                    sach.AnhBia = "img";
                }
            }
            else
            {
                sach.AnhBia = "img";
            }

            string pathAnh1 = "";
            if (anh1 != null)
            {
                if (anh1.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh1.FileName);
                    pathAnh1 = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anh1.SaveAs(pathAnh1);
                    //path= path.Replace("\","/");
                    sach.Anh1 = fileName;
                }
                else
                {
                    sach.Anh1 = "img";
                }
            }
            else
            {
                sach.Anh1 = "img";
            }


            string pathAnh2 = "";
            if (anh2 != null)
            {
                if (anh2.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(anh2.FileName);
                    pathAnh2 = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fileName);
                    anh2.SaveAs(pathAnh2);
                    //path= path.Replace("\","/");
                    sach.Anh2 = fileName;
                }
                else
                {
                    sach.Anh2 = "img";
                }
            }
            else
            {
                sach.Anh2 = "img";
            }
        }
        public void InsertSach(Sach sach, HttpPostedFileBase anhBia, string chude, string ncc, string tg, string nxb, string km, HttpPostedFileBase anh1, HttpPostedFileBase anh2, double? chieuDai, double? chieuRong, double? chieuSau)
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
            Sach newSach = new Sach();
            newSach.ID = Convert.ToInt64(id);
            newSach.Ten = sach.Ten;
            newSach.Gia = sach.Gia;
            newSach.LuotLike = 0;
            newSach.LuotXem = 0;
            newSach.SoLuong = sach.SoLuong;
            newSach.NamXuatBan = sach.NamXuatBan;
            newSach.IdChuDe = Convert.ToInt64(chude);
            newSach.ChuDe = db.ChuDes.Find(Convert.ToInt64(chude));
            newSach.IdNCC = Convert.ToInt64(ncc);
            newSach.NhaCungCap = db.NhaCungCaps.Find(Convert.ToInt64(ncc));
            newSach.IdNXB = Convert.ToInt64(nxb);
            newSach.NhaXuatBan = db.NhaXuatBans.Find(Convert.ToInt64(nxb));
            newSach.IdTacGia = Convert.ToInt64(tg);
            newSach.TacGia = db.TacGias.Find(Convert.ToInt64(tg));
            newSach.HinhThuc = sach.HinhThuc;
            newSach.MoTa = sach.MoTa;
            newSach.NgonNgu = sach.NgonNgu;
            newSach.TrongLuong = sach.TrongLuong;
            newSach.SoTrang = sach.SoTrang;
            newSach.KickThuoc = chieuDai.ToString() + "x" + chieuRong.ToString() + "x" + chieuSau.ToString();
            if (km != "0")
            {
                newSach.IdKhuyenMai = Convert.ToInt64(km);
                newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(km));
            }
            else
            {
                newSach.IdKhuyenMai = 1;
                newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(1));
            }
            GanAnh(newSach, anhBia, anh1, anh2);
            newSach.flag = true;
            db.Sachs.Add(newSach);
            db.SaveChanges();
        }

        public bool InsertSachtestcase(Sach sach, string chude, string ncc, string tg, string nxb, string km)
        {
            try
            {
                var newSach = new Sach();
                newSach.Ten = sach.Ten;
                newSach.Gia = sach.Gia;
                newSach.SoLuong = sach.SoLuong;
                newSach.NamXuatBan = sach.NamXuatBan;
                newSach.IdChuDe = Convert.ToInt64(chude);
                newSach.ChuDe = db.ChuDes.Find(Convert.ToInt64(chude));
                newSach.IdNCC = Convert.ToInt64(ncc);
                newSach.NhaCungCap = db.NhaCungCaps.Find(Convert.ToInt64(ncc));
                newSach.IdNXB = Convert.ToInt64(nxb);
                newSach.NhaXuatBan = db.NhaXuatBans.Find(Convert.ToInt64(nxb));
                newSach.IdTacGia = Convert.ToInt64(tg);
                newSach.TacGia = db.TacGias.Find(Convert.ToInt64(tg));
                newSach.HinhThuc = sach.HinhThuc;
                newSach.MoTa = sach.MoTa;
                newSach.NgonNgu = sach.NgonNgu;
                newSach.TrongLuong = sach.TrongLuong;
                newSach.SoTrang = sach.SoTrang;
                newSach.Anh1 = null;
                newSach.Anh2 = null;
                newSach.AnhBia = null;
                newSach.KickThuoc = sach.KickThuoc;
                if (km != "0")
                {
                    newSach.IdKhuyenMai = Convert.ToInt64(km);
                    newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(km));
                }
                else
                {
                    newSach.IdKhuyenMai = 1;
                    newSach.KhuyenMai = db.KhuyenMais.Find(Convert.ToInt64(1));
                }
                newSach.flag = true;
                db.Sachs.Add(newSach);
                //db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void DeleteSach(long idSach)
        {
            db.Sachs.Find(idSach).flag = false;
            db.SaveChanges();
        }

        public bool DeleteSachtestcase(long idSach)
        {
            var sach = db.Sachs.Find(idSach);
            if(sach!=null)
            {
                db.Sachs.Find(idSach).flag = false;
                return true;
            }
            return false;
        }
    }
}