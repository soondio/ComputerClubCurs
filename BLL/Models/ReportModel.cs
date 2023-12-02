using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{

    public class ReportData
    {
        public int PotencionalCount { get; set; }
        public int RealCount { get; set; }
        public decimal? PotencionalMoney { get; set; }
        public decimal? RealMoney { get; set; }
    }
}
