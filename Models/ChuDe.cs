
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class ChuDe
    {
        [Key]
        public long IdChuDe { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên")]
        [MaxLength(100)]
        public string Ten { get; set; }
        
        public string XuatXu { get; set; }

        [DefaultValue(true)]
        public bool flag { get; set; }
        public ICollection<Sach> Saches { get; set; }
    }
}
