using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class CampgroundSqlDAO :ICampgroundDAO
    {
        private const string CONNECTION_STRING = "Server=.\\SQLExpress;Database=npcampground;Trusted_Connection=True;";

        public IList<Campground> GetAllCampgrounds(int parkId)
        {
            List<Campground> camps = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM campground WHERE park_id = @parkId", conn);
                    cmd.Parameters.AddWithValue("@parkId", parkId);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Campground camp = new Campground
                        {
                            CampgroundId = Convert.ToInt32(rdr["campground_id"]),
                            ParkId = Convert.ToInt32(rdr["park_id"]),
                            Name = Convert.ToString(rdr["name"]),
                            OpenFrom = Convert.ToInt32(rdr["open_from_mm"]),
                            OpenTo = Convert.ToInt32(rdr["open_to_mm"]),
                            DailyFee = Convert.ToDecimal(rdr["daily_fee"])
                        };
                        camps.Add(camp);
                    }
                }
                return camps;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}
