using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication23.Models
{
    public class TichLuy
    {
        [Key]
        public int IdTichLuy { get; set; }
        [DefaultValue(0)]
        public int Diem { get; set; }
        [DefaultValue(0)]
        public double PhamTran { get; set; }

        [DefaultValue(true)]
        public bool flag { get; set; }
    }
}
