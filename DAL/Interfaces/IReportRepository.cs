using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IReportsRepository
    {
        Report GetReport(DateTime param1, DateTime param2);
        Report GetReportByFood(DateTime param1, DateTime param2);
        //List<Report> ReportAllOrdersbyClient(int idclient);
    }
}
