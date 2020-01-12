using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class Sach
    {
        [Key]
        public long ID { get; set; }

        //Ten
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        public string Ten { get; set; }

        //Gia
        [Required(ErrorMessage = "Vui lòng nhập giá")]
        public double Gia { get; set; }

        //So Luong
        [Range(0,double.MaxValue,ErrorMessage ="Lỗi giá")]
        [DefaultValue(1)]
        public int SoLuong { get; set; }

        //Hinh Anh
        public string AnhBia { get; set; }
        public string Anh1 { get; set; }
        public string Anh2 { get; set; }

        //Khoa Ngoai
        public long IdChuDe { get; set; }
        public long IdNCC { get; set; }
        public long IdTacGia { get; set; }
        public long IdNXB { get; set; }

        //Nam Xuat Ban
        [Required]
        public DateTime NamXuatBan { get; set; }

        
        public double TrongLuong { get; set; }
        public string KickThuoc { get; set; }
        public int SoTrang { get; set; }
        public string HinhThuc { get; set; }
        public string NgonNgu { get; set; }
        public string MoTa { get; set; }

        [DefaultValue(0)]
        public long LuotXem { get; set; }
        [DefaultValue(0)]
        public long LuotLike { get; set; }
        public long IdKhuyenMai { get; set; }
        [DefaultValue(true)]
        public bool flag { get; set; }

        //Khoa Ngoai
        public virtual ChuDe ChuDe { get; set; }
        public virtual NhaCungCap NhaCungCap { get; set; }
        public virtual NhaXuatBan NhaXuatBan { get; set; }
        public virtual TacGia TacGia { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }

        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    }
}
