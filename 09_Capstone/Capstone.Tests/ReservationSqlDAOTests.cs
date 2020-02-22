using Capstone.DAL;
using Capstone.Models;
using Capstone.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationSqlDAOTests
    {
        private TransactionScope transaction = null;
        private const string CONNECTION_STRING = "Server=.\\SQLExpress;Database=npcampground;Trusted_Connection=True;";
        private int newResId;

        [TestInitialize]
        public void SetupDatabase()
        {
            // Start a transaction, so we can roll back when we are finished with this test
            transaction = new TransactionScope();

            // Open Setup.Sql and read in the script to be executed
            string setupSQL;
            using (StreamReader rdr = new StreamReader("Setup.sql"))
            {
                setupSQL = rdr.ReadToEnd();
            }

            // Connect to the DB and execute the script
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(setupSQL, conn);
                SqlDataReader rdr = cmd.ExecuteReader();

                // Get the result (new id) and save it for use later in a test.
                if (rdr.Read())
                {
                    newResId = Convert.ToInt32(rdr["newResId"]); // use @identity for this value
                }
            }
        }

        [TestCleanup]
        public void CleanupDatabase()
        {
            // Rollback the transaction to get our good data back
            transaction.Dispose();
        }

        [TestMethod]
        public void GetAllReservationsTest()
        {
            ReservationSqlDAO res = new ReservationSqlDAO();

            IList<Reservation> actualResult = res.GetAllReservations();

            Assert.IsTrue(actualResult.Count > 0);

        }

    }
}
