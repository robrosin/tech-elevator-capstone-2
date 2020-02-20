using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ParkSqlDAO : IParkDAO
    {
        private const string CONNECTION_STRING = "Server=.\\SQLExpress;Database=npcampground;Trusted_Connection=True;";

        public IList<Park> GetAllParks()
        {
            List<Park> parks = new List<Park>();
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park", conn);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Park park = new Park()
                        {
                            ParkId = Convert.ToInt32(rdr["park_id"]),
                            Name = Convert.ToString(rdr["name"]),
                            Location = Convert.ToString(rdr["location"]),
                            EstablishDate = Convert.ToDateTime(rdr["establish_date"]),
                            Area = Convert.ToInt32(rdr["area"]),
                            Visitors = Convert.ToInt32(rdr["visitors"]),
                            Description = Convert.ToString(rdr["description"])
                        };
                        parks.Add(park);
                    }
                }

                return parks;

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}
