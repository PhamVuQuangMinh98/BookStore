using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class KhuyenMai
    {
        [Key]
        public long IdKhuyenMai { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string TenKhuyenMai { get; set; }

        public string HinhKhuyenMai { get; set; }
        public double PhanTram { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }
        [DefaultValue(true)]
        public bool flag { get; set; }

        public ICollection<Sach> Saches { get; set; }
    }
}
