using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class OurMenu
    {
        private const string CONNECTION_STRING = "Server=.\\SQLExpress;Database=npcampground;Trusted_Connection=True;";
        ParkSqlDAO parkObj = new ParkSqlDAO();
        CampgroundSqlDAO campObj = new CampgroundSqlDAO();
        SiteSqlDAO siteObj = new SiteSqlDAO();
        ReservationSqlDAO reserveObj = new ReservationSqlDAO();
        //Define the menu, call methods as certain parts of the menu, create functionality between different methods and classes
        IList<Park> parkList = new List<Park>();
        IList<Campground> campList = new List<Campground>();
        IList<Site> siteList = new List<Site>();
        IList<Reservation> reserveList = new List<Reservation>();
        string campInput;
        int parkId;
        int campId;
        DateTime startDate;
        DateTime endDate;
        int numOfDays;
        int dailyFee;

        public void GetParkList()
        {
            parkList = parkObj.GetAllParks();
        }
        public void GetCampList()
        {
            campList = campObj.GetAllCampgrounds(parkId);
        }
        public void GetSiteList()
        {
            siteList = siteObj.GetSites(campId, startDate, endDate);
            numOfDays = Convert.ToInt32((endDate - startDate).TotalDays);
        }
        public void GetReserveList()
        {
            reserveList = reserveObj.GetAllReservations();
        }

        // Ask about adding reservation information to the reservation table


        public void DisplayMainMenu()
        {
                             // Where an invalid value is entered, continue the loop, displaying a message to the user and redisplaying the main menu

            Console.WriteLine(@"
  _____           _      _____            _     _              
 |  __ \         | |    |  __ \          (_)   | |             
 | |__) |_ _ _ __| | __ | |__) |___  __ _ _ ___| |_ _ __ _   _ 
 |  ___/ _` | '__| |/ / |  _  // _ \/ _` | / __| __| '__| | | |
 | |  | (_| | |  |   <  | | \ \  __/ (_| | \__ \ |_| |  | |_| |
 |_|   \__,_|_|  |_|\_\ |_|  \_\___|\__, |_|___/\__|_|   \__, |
                                     __/ |                __/ |
                                    |___/                |___/ ");

            ListParks();
            string parkInput = Console.ReadLine();

            int i = 1;
            foreach (Park park in parkList)
            {
                try
                {
                    if (parkInput.ToLower() == "q")
                    {
                        Environment.Exit(0);
                    }
                    else if (Convert.ToInt32(parkInput) == i)
                    {
                        parkId = Convert.ToInt32(parkInput);
                        GetCampList();
                        Console.Clear();
                        ListParkInformation(i);
                        ParkCommands();
                    }
                    else
                    {
                        i++;
                        continue;
                    }

                }
                catch (System.FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please only enter the value next to the desired option");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }
            }

        }

        public void ParkCommands()
        {
            string input = Console.ReadLine();


            switch (input)
            {
                case "1":
                    //ListCampgrounds();
                    CampCommands();
                    break;
                case "2":
                    // This will call ReservationSearch()
                    ReservationSearch();
                    break;
                case "3":
                    DisplayMainMenu();
                    break;
            }
        }

        public void CampCommands()
        {
            Console.Clear();
            ListCampgrounds();
            Console.WriteLine();
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return to Previous Screen");
            Console.WriteLine();
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    // This will call reservation search
                    ReservationSearch();
                    break;
                case "2":
                    Console.WriteLine("Returning to menu, hit enter to confirm" +
                        "");
                    ParkCommands();
                    break;

            }
        }

        public void ReservationSearch() // find all of the sites that do have issues
        {
            Console.Clear();
            ListCampgrounds();

            Console.WriteLine();
            Console.Write("Which Campground (enter 0 to cancel)? ");
            campInput = Console.ReadLine();
            if (campInput == "0")
            {
                CampCommands();
            }
            else
            {
                Console.Write("What is the arrival date? Year-Month-Date ");
                string arrival = Console.ReadLine();
                startDate = Convert.ToDateTime(arrival).Date;
                Console.Write("What is the departure date? Year-Month-Date ");
                string depart = Console.ReadLine();
                endDate = Convert.ToDateTime(depart).Date;
                GetSiteList();
                ListSites(numOfDays);
                Console.ReadLine();
            }

        }

        //public void RegisterReservation() // TODO 07: Add functionality to actually register the reservation.
        //{
        //    Console.Write("What is the arrival date? Year-Month-Date ");
        //    string arrival = Console.ReadLine();
        //    startDate = Convert.ToDateTime(arrival).Date;
        //    Console.Write("What is the departure date? Year-Month-Date ");
        //    string depart = Console.ReadLine();
        //    endDate = Convert.ToDateTime(depart).Date;
        //    GetSiteList();
        //    Console.ReadLine();
        //}






        public void ListParks()
        {
            int i = 1;
            Console.WriteLine("Select a Park For Further Details");
            foreach (Park park in parkList)
            {
                Console.WriteLine($"{i}) {park.Name}");
                i++;
            }
            Console.WriteLine($"Q) Quit");
        }

        public void ListParkInformation(int parkId) // in MainMenu, ask what park to select, save that selection as parkName
        {
            foreach (Park park in parkList)
            {
                if (park.ParkId == parkId)
                {
                    Console.WriteLine(park.Name + " National Park");
                    Console.WriteLine($"{"Location:",-18} {park.Location,-20}");
                    Console.WriteLine($"{"Established:",-18} {park.EstablishDate.ToShortDateString(),-20}");
                    Console.WriteLine($"{"Area:",-18} {park.Area,-20}");
                    Console.WriteLine($"{"Annual Visitors:",-18} {park.Visitors,-20}");
                    Console.WriteLine();
                    Console.WriteLine(park.Description);
                } 
            }
            Console.WriteLine();
            Console.WriteLine("Select a Command");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Search for Reservation");
            Console.WriteLine("3) Return to Previous Screen");
            // in MainMenu Method, if park does not exist, return to park select screen with "park not exist" message displaying above/below park list
        }

        public void ListCampgrounds()
        {
            
            Console.WriteLine($" {"",-5} {"Name",-40} {"Open",-10} {"Close",-10} {"DailyFee",-10}");
            foreach (Campground camp in campList)
            {
                Console.WriteLine($"#{camp.CampgroundId,-5} {camp.Name,-40} {camp.OpenFrom,-10} {camp.OpenTo,-10} {camp.DailyFee,-10:C}");
                
            }
        }

        public void ListSites(int numOfDays)
        {
            Console.WriteLine($"{"Site No.",-10} {"Max Occup.",-12} {"Accessible",-12} {"Max RV Length",-15} {"Utility",-10} {"Cost",-10}");
            if (siteList.Count > 0)
            {
                foreach (Site site in siteList)
                {
                    Console.WriteLine($"{site.SiteId,-10} {site.MaxOccupancy,-12} {site.Accessible,-12} {site.MaxRVLength,-15} {site.Utilities,-10} {"Cost",-10:C}");
                    
                }

            }
            else
            {
                Console.WriteLine("There are no available sites, would you like to enter an alternative date-range? y/n ");
                string input = Console.ReadLine().ToLower();
                if(input == "y")
                {
                    ReservationSearch();
                }
                else
                {
                    CampCommands();
                }
            }

        }


    }
}
