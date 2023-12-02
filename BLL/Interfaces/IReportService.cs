using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IReportService
    {
        ReportData GetReport(DateTime param1, DateTime param2);
        ReportData GetReportByFood(DateTime param1, DateTime param2);
    }
}
