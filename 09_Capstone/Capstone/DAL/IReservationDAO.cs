using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.DAL
{
    public interface IReservationDAO
    {
        IList<Reservation> GetAllReservations();

        IList<Reservation> SaveReservation(int siteId, string name, DateTime fromDate, DateTime toDate);

        //IList<Reservation> GetReservationDates(string fromDate, string toDate);
    }
}
