using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class Log
    {
        [Key]
        public long IdLog { get; set; }
        public long IdKhachHang { get; set; }
        public string HanhDong { get; set; }
        public string ChiTiet { get; set; }
        public DateTime Ngay { get; set; }
        [DefaultValue(true)]
        public bool flag { get; set; }

        public virtual KhachHang KhachHang { get; set; }

    }
}
