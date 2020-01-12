
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        public long IdCTHD { get; set; }
        public long IdHoaDon { get; set; }
        public long IdSach { get; set; }

        public double DonGia { get; set; }
        public int SoLuong { get; set; }
        public double ThanhTien { get; set; }

        [DefaultValue(true)]
        public bool flag { get; set; }

        public virtual HoaDon HoaDon { get; set; }
        public virtual Sach Sach { get; set; }

    }
}

