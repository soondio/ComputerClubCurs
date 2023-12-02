using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IReservationService
    {
        bool MakeReservation(ReservationModel reservationDto, int UserId);
        List<ComputerPlaceModel> CheckFreeComputers(DateTime start, DateTime end);
        bool CancelReservation(int id);
        bool ConfirmReservation(int id);

    }
}
