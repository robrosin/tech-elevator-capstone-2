using Capstone.Models;
using Capstone.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class SiteSqlDAO : ISiteDAO
    {
        private const string CONNECTION_STRING = "Server=.\\SQLExpress;Database=npcampground;Trusted_Connection=True;";

        public IList<Site> GetSites(int campId, DateTime startDate, DateTime endDate)
        {
            List<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
                {
                    conn.Open();

                    string sql = @"
SELECT TOP 5 *
FROM site
WHERE campground_id = @campId AND site_id NOT IN
(SELECT site_Id 
FROM reservation
WHERE ((@startDate > from_date) AND (@startDate <= to_date))
OR ((@endDate > from_date) AND (@endDate <= to_date))
OR ((@startDate < from_date) AND (@endDate >= to_date)))";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@campId", campId);
                    cmd.Parameters.AddWithValue("@startDate", startDate);
                    cmd.Parameters.AddWithValue("@endDate", endDate);

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Site site = new Site()
                        {
                            SiteId = Convert.ToInt32(rdr["site_id"]),
                            CampgroundId = Convert.ToInt32(rdr["campground_id"]),
                            SiteNumber = Convert.ToInt32(rdr["site_number"]),
                            MaxOccupancy = Convert.ToInt32(rdr["max_occupancy"]),
                            Accessible = Convert.ToBoolean(rdr["accessible"]),
                            MaxRVLength = Convert.ToInt32(rdr["max_rv_length"]),
                            Utilities = Convert.ToBoolean(rdr["utilities"])
                        };
                        sites.Add(site);
                    };
                }
                return sites;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

    }
}
