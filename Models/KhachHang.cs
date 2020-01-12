using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class KhachHang
    {
        [Key]
        public long IdKhachHang { get; set; }

        [Required(ErrorMessage ="Không được để trống")]
        [MaxLength(50)]
        public string Ho { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        [MaxLength(50)]
        public string Ten { get; set; }
        
        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; }

        [MaxLength(32)]
        public string Username { get; set; }
        
        [MaxLength(32)]
        public string Password { get; set; }

        [DefaultValue(0)]
        public int DiemTichLuy { get; set; }

        [DefaultValue("BT")]
        public string LoaiKH { get; set; }

        [DefaultValue("KH")]
        public string LoaiTK { get; set; }

        [DefaultValue(true)]
        public bool flag { get; set; }

        public ICollection<Log> Logs { get; set; }
        public ICollection<HoaDon> HoaDons { get; set; }
    }
}
