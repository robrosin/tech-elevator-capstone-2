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
        //Define the menu, call methods as certain parts of the menu, create functionality between different methods and classes
        IList<Park> parkList = new List<Park>();
        IList<Campground> campList = new List<Campground>();
        IList<Site> siteList = new List<Site>();


        public void GetParkList()
        {
            parkList = parkObj.GetAllParks();
        }
        public void GetCampList()
        {
            campList = campObj.GetAllCampgrounds();
        }
        public void GetSiteList()
        {
            siteList = siteObj.GetTopFiveSites();
        }

        // Ask about adding reservation information to the reservation table


        public void DisplayMainMenu()
        {
            Console.Clear(); // TODO 04: Put entire method text in a while loop, when a correct value is entered, break the loop and follow command
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
                    Console.Clear();
                    ListCampgrounds();
                    CampCommands();
                    break;
                case "2":
                    // This will call ReservationSearch()
                    Console.WriteLine("Not Implemented");
                    Console.ReadLine();
                    break;
                case "3":
                    DisplayMainMenu();
                    break;
            }
        }

        public void CampCommands()
        {
            string input = Console.ReadLine();
            // TODO 05: See if you can display the name of the park
            switch (input)
            {
                case "1":
                    // This will call reservation search
                    Console.WriteLine("Not Implemented");
                    Console.ReadLine();
                    break;
                case "2":
                    ParkCommands();
                    break;

            }
        }














        public void ListParks()
        {
            // TODO 02: Make sure parks are all listed alphabetically
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
                } // TODO 01: See if there is a way to return to menu screen when park not found while still looping through the entire list
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
            int i = 1;
            Console.WriteLine($" {"",-5} {"Name",-40} {"Open",-10} {"Close",-10} {"DailyFee",-10}");
            foreach (Campground camp in campList)
            {
                Console.WriteLine($"#{i,-5} {camp.Name,-40} {camp.OpenFrom,-10} {camp.OpenTo,-10} {camp.DailyFee,-10:C}");
                i++;
            }
            Console.WriteLine();
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return to Previous Screen");

        }

        public void ListSites(int numOfDays)
        {
            int i = 1;
            Console.WriteLine($"{"Site No.",-10} {"Max Occup.",-12} {"Accessible",-12} {"Max RV Length",-15} {"Utility",-10} {"Cost",-10}");
            foreach (Site site in siteList
                )
            {
                Console.WriteLine($"{site.SiteId,-10} {site.MaxOccupancy,-12} {site.Accessible,-12} {site.MaxRVLength,-15} {site.Utilities,-10} {"Cost",-10:C}");
                i++;
                // TODO 03: Have to plug in variable containing DailyFree from campground multiplied by number of days reserved.
            }

        }


    }
}
