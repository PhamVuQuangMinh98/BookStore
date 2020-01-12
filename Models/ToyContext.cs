using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class ToyContext : DbContext
    {
        public ToyContext() :base(){
            string dbName = "ToyStore";
            this.Database.Connection.ConnectionString = "Data Source=DESKTOP-P18F2FS;Initial Catalog=" + dbName + ";Trusted_Connection=Yes";
        }
        public DbSet<ChuDe> ChuDes { get; set; }
        public DbSet<TacGia> TacGias { get; set; }
        public DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public DbSet<NhaXuatBan> NhaXuatBans { get; set; }
        public DbSet<KhuyenMai> KhuyenMais { get; set; }
        public DbSet<Sach> Sachs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<HoaDon> HoaDon { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDon { get; set; }
        public DbSet<TichLuy> TichLuys { get; set; }


    }
}
