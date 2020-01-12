using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class HoaDon
    {
        [Key]
        public long IdHoaDon { get; set; }
        public long IdKhachHang { get; set; }
        public DateTime NgayTao { get; set; }
        public double TongTien { get; set; }
        
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        public string TenNguoiNhan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập sdt")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ")]
        public string DiaChi { get; set; }
       
        public string TinhTrangDonHang { get; set; }
        public string TinhTrangHoaDon { get; set; }

        [DefaultValue(true)]
        public bool flag { get; set; }

        public virtual KhachHang KhachHang { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
