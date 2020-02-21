using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        ParkSqlDAO parkObj = new ParkSqlDAO();
        CampgroundSqlDAO campObj = new CampgroundSqlDAO();
        SiteSqlDAO siteObj = new SiteSqlDAO();
        ReservationSqlDAO reserveObj = new ReservationSqlDAO();
        // DAOs - Interfaces to our data objects can be stored here...
        //protected ICityDAO cityDAO;
        //protected ICountryDAO countryDAO;

        /// <summary>
        /// Constructor adds items to the top-level menu. YOu will likely have parameters for one or more DAO's here...
        /// </summary>
        public MainMenu(/***ICityDAO cityDAO, ICountryDAO countryDAO***/) : base("Main Menu")
        {
            //this.cityDAO = cityDAO;
            //this.countryDAO = countryDAO;
            
        }

        protected override void SetMenuOptions() 
        {
            IList<Park> parkList = parkObj.GetAllParks();
            int i = 1;
            
            foreach (Park park in parkList)
            {
                menuOptions.Add(i.ToString(), park.Name);
                i++;
            }
            this.menuOptions.Add("Q", "Quit program");
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            IList<Park> parkList = parkObj.GetAllParks();
            int selection = int.Parse(choice);
            Park park = parkList[selection - 1];
            ParkMenu pMenu = new ParkMenu(park);
            pMenu.Run();
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
            Console.WriteLine("Select a Park For Further Details");
        }


        private void PrintHeader()
        {
            SetColor(ConsoleColor.Yellow);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Campsite Reservation"));
            ResetColor();
        }
    }
}
