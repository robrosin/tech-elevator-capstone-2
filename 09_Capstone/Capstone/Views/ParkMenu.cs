using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class ParkMenu : CLIMenu
    {
        // Store any private variables, including DAOs here....
        ParkSqlDAO parkObj = new ParkSqlDAO();
        CampgroundSqlDAO campObj = new CampgroundSqlDAO();
        SiteSqlDAO siteObj = new SiteSqlDAO();
        ReservationSqlDAO reserveObj = new ReservationSqlDAO();

        Park park;

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public ParkMenu(Park park) :
            base("Sub-Menu 1")
        {
            // Store any values or DAOs passed in....
            this.park = park;
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "View Campgrounds");
            this.menuOptions.Add("2", "Return to Previous Screen");
            this.quitKey = "2";
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1": // Do whatever option 1 is
                    CampSearchMenu campMenu = new CampSearchMenu(park);
                    campMenu.Run();
                    return true;
                case "2": // Do whatever option 2 is
                    WriteError("Not yet implemented");
                    Pause("");
                    return false;
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
            Console.WriteLine(park.Name + " National Park");
            Console.WriteLine($"{"Location:",-18} {park.Location,-20}");
            Console.WriteLine($"{"Established:",-18} {park.EstablishDate.ToShortDateString(),-20}");
            Console.WriteLine($"{"Area:",-18} {park.Area,-20}");
            Console.WriteLine($"{"Annual Visitors:",-18} {park.Visitors,-20}");
            Console.WriteLine();
            Console.WriteLine(park.Description);
        }

        protected override void AfterDisplayMenu()
        {
            base.AfterDisplayMenu();
            SetColor(ConsoleColor.Cyan);
            Console.WriteLine("Display some data here, AFTER the sub-menu is shown....");
            ResetColor();
        }

        private void PrintHeader()
        {
            SetColor(ConsoleColor.Magenta);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render(park.Name));
            ResetColor();
        }

    }
}
