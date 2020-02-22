using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    public class CampSearchMenu : CLIMenu
    {
        // Store any private variables, including DAOs here....
        ParkSqlDAO parkObj = new ParkSqlDAO();
        CampgroundSqlDAO campObj = new CampgroundSqlDAO();
        SiteSqlDAO siteObj = new SiteSqlDAO();
        ReservationSqlDAO reserveObj = new ReservationSqlDAO();
        
        
        Park park;
        Campground chosenCamp;

        int reserveId;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public CampSearchMenu(Park park) :
            base("Sub-Menu 2")
        {
            // Store any values or DAOs passed in....
            this.park = park;
        }

        protected override void SetMenuOptions()
        {
            IList<Campground> campList = campObj.GetAllCampgrounds(park.ParkId);
            int i = 1;
            foreach (Campground camp in campList)
            {
                menuOptions.Add(i.ToString(), $"{camp.Name,-40} {camp.OpenFrom,-10} {camp.OpenTo,-10} {camp.DailyFee,-10:C}");
                i++;
            }
            this.menuOptions.Add("0", "Return to Previous Screen");
            this.quitKey = "0";
        }

        public void GetReservationId()
        {
            reserveId = reserveObj.NewId;
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            int choiceInt = int.Parse(choice);
            IList<Campground> campList = campObj.GetAllCampgrounds(park.ParkId);
            chosenCamp = campList[choiceInt - 1];
            SearchReservation();
            return true;
        }

        private void SearchReservation()
        {
            Console.Write("What is the arrival date? Year-Month-Date ");
            string arrival = Console.ReadLine();
            DateTime startDate = Convert.ToDateTime(arrival).Date;
            Console.Write("What is the departure date? Year-Month-Date ");
            string depart = Console.ReadLine();
            DateTime endDate = Convert.ToDateTime(depart).Date;
            IList<Site> siteList = siteObj.GetSites(chosenCamp.CampgroundId, startDate, endDate);
            if (siteList.Count == 0)
            {
                Pause("There are no available sites");
                return;
            }
            CreateReservation(siteList, startDate, endDate);
        }

        public void CreateReservation(IList<Site> siteList, DateTime startDate, DateTime endDate)
        {
            DateTime fromDate = startDate;
            DateTime toDate = endDate;
            int numOfDays = (endDate - startDate).Days;
            List<int> chosenSites = new List<int>(); // create a list of ints that will contain the siteId's captured in the foreach loop below

            Console.WriteLine($"{"Site No.",-10} {"Max Occup.",-12} {"Accessible",-12} {"Max RV Length",-15} {"Utility",-10} {"Cost",-10}");

            foreach (Site site in siteList)
            {
                Console.WriteLine($"{site.SiteId,-10} {site.MaxOccupancy,-12} {site.Accessible,-12} {site.MaxRVLength,-15} {site.Utilities,-10} {numOfDays * chosenCamp.DailyFee,-10:C}");
                chosenSites.Add(site.SiteId);
            }

            Console.WriteLine("Which site would you like to reserve? (0 to cancel) ");
            string siteIdStr = Console.ReadLine(); // TODO 02: make sure only a valid SiteId can be selected (try/catch?)

            try
            {
                int siteId = Convert.ToInt32(siteIdStr);
                if (chosenSites.Contains(siteId))
                {
                    Console.WriteLine("What name should the reservation be made under? ");
                    string reserveName = Console.ReadLine();
                    reserveObj.SaveReservation(siteId, reserveName, startDate, endDate);
                    GetReservationId();
                    Console.WriteLine($"Your reservation has been created. Your reservation ID is {reserveId}");
                    Console.WriteLine("Hit enter to continue");
                    Console.ReadLine();
                    return;
                }
            }
            catch (System.FormatException ex)
            {
                Console.WriteLine(ex.Message);
                WriteError("Please input a valid site number");
                return;
            }

        }


        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
            Console.WriteLine($"{"Name",-43} {"Open",-10} {"Close",-10} {"DailyFee",-10}");

        }

        private void PrintHeader()
        {
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render(park.Name));
            ResetColor();
        }

    }

}
