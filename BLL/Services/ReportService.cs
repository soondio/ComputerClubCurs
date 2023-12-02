using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using DAL.Entities;


namespace BLL.Services
{
    public class ReportService : IReportService
    {
        private IDbRepos db;
        public ReportService(IDbRepos repos)
        {
            db = repos;
        }
        public ReportData GetReport(DateTime param1, DateTime param2)
        {
            ReportData reportModel = new ReportData();
            Report report = db.Reports.GetReport(param1, param2);
            reportModel.PotencionalCount = report.PotencionalCount;
            reportModel.PotencionalMoney = report.PotencionalMoney;
            reportModel.RealMoney = report.RealMoney;
            reportModel.RealCount = report.RealCount;
            return reportModel;
        }
        public ReportData GetReportByFood(DateTime param1, DateTime param2)
        {
            ReportData reportModel = new ReportData();
            Report report = db.Reports.GetReportByFood(param1, param2);
            reportModel.PotencionalCount = report.PotencionalCount;
            reportModel.PotencionalMoney = report.PotencionalMoney;
            reportModel.RealMoney = report.RealMoney;
            reportModel.RealCount = report.RealCount;
            return reportModel;
        }

    }
}
