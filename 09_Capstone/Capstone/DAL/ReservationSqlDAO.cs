using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationSqlDAO : IReservationDAO
    {
        private const string CONNECTION_STRING = "Server=.\\SQLExpress;Database=npcampground;Trusted_Connection=True;";

        public IList<Reservation> GetAllReservations()
        {
            List<Reservation> reserves = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground", conn);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Reservation reservation = new Reservation
                        {
                            ReservationId = Convert.ToInt32(rdr["reservation_id"]),
                            SiteId = Convert.ToInt32(rdr["site_id"]),
                            Name = Convert.ToString(rdr["name"]),
                            FromDate = Convert.ToDateTime(rdr["from_date"]),
                            ToDate = Convert.ToDateTime(rdr["to_date"]),
                            CreateDate = Convert.ToDateTime(rdr["create_date"])
                        };
                        reserves.Add(reservation);
                    }
                }
                return reserves;

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
