using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Report
    {
        public int PotencionalCount { get; set; }
        public int RealCount { get; set; }
        public decimal? PotencionalMoney { get; set; }
        public decimal? RealMoney { get; set; }

    }
}
