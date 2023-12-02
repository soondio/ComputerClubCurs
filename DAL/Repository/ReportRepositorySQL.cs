using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.EntitiesCodeFirst;
using System.Data.Entity;
using System.Data.SqlClient;
using DAL.Entities;

namespace DAL.Repository
{
    public class ReportRepositorySQL : IReportsRepository
    {
        private ComputerClubContext db;
        public ReportRepositorySQL(ComputerClubContext dbcontext)
        {
            this.db = dbcontext;
        }

        public Report GetReport(DateTime from, DateTime to)
        {
            Report report = new Report();
            report.PotencionalCount = db.Reservations
                .Where(r => r.StartDateTime > from && r.EndDateTime < to&&(r.ReservationStatus=="забронировано"||r.ReservationStatus=="выполнен"))
                .ToList().Count;
            var jkaldsjakl = db.Reservations
                .Where(r => r.StartDateTime > from && r.EndDateTime < to &&( r.ReservationStatus == "забронировано" || r.ReservationStatus == "выполнен"))
                .ToList();
            report.RealCount = db.Reservations
                .Where(r => r.StartDateTime > from && r.EndDateTime < to && r.ReservationStatus == "выполнен")
                .ToList().Count;
            report.PotencionalMoney = db.Reservations
    .Where(r => r.StartDateTime > from && r.EndDateTime < to && (r.ReservationStatus == "забронировано" || r.ReservationStatus == "выполнен"))
    .Sum(r => (decimal?)r.TotalPrice) ?? 0;

            report.RealMoney = db.Reservations
    .Where(r => r.StartDateTime > from && r.EndDateTime < to && r.ReservationStatus == "выполнен")
    .Sum(r => (decimal?)r.TotalPrice) ?? 0;


            return report;

        }
        public Report GetReportByFood(DateTime from,DateTime to)
        {
            Report report = new Report();
            report.PotencionalCount = db.FoodOrders
                .Where(r => r.OrderDateTime > from && r.OrderDateTime < to && (r.OrderStatus == "забронировано" || r.OrderStatus == "выполнен"))
                .ToList().Count;
            var jkaldsjakl = db.FoodOrders
                .Where(r => r.OrderDateTime > from && r.OrderDateTime < to && (r.OrderStatus == "забронировано" || r.OrderStatus == "выполнен"))
                .ToList();
            report.RealCount = db.FoodOrders
                .Where(r => r.OrderDateTime > from && r.OrderDateTime < to && r.OrderStatus == "выполнен")
                .ToList().Count;
            report.PotencionalMoney = db.FoodOrders
                    .Where(r => r.OrderDateTime > from && r.OrderDateTime < to && (r.OrderStatus == "забронировано" || r.OrderStatus == "выполнен"))
                    .Sum(r => (decimal?)r.TotalPrice) ?? 0;
            report.RealMoney = db.FoodOrders
            .Where(r => r.OrderDateTime > from && r.OrderDateTime < to && r.OrderStatus == "выполнен")
            .Sum(r => (decimal?)r.TotalPrice) ?? 0;
            return report;
        }
    }
}
