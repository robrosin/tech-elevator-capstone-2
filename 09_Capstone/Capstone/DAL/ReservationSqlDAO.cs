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

                    SqlCommand cmd = new SqlCommand("SELECT * FROM reservation", conn);

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

        public IList<Reservation> SaveReservation(int siteId, string name, DateTime fromDate, DateTime toDate)
        {
            List<Reservation> reserves = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    conn.Open();

                    string sql = @"
INSERT INTO reservation
    (site_id, name, from_date, to_date)
VALUES
    (@siteId, @name, @fromDate, @toDate)
SELECT @@IDENTITY";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@siteId", siteId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);

                    int newId = Convert.ToInt32(cmd.ExecuteScalar()); // TODO 01: Find way to get newId into CreateReservation method under CampSearchMenu

                    cmd.ExecuteNonQuery();

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
