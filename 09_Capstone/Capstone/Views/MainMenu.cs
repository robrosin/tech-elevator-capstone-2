using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
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
            this.menuOptions.Add("1", "Add 2 integers");
            this.menuOptions.Add("2", "Menu option 2");
            this.menuOptions.Add("3", "Go to a sub-menu");
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
            
            switch (choice)
            {
                case "1": // Do whatever option 1 is
                    int i1 = GetInteger("Enter the first integer: ");
                    int i2 = GetInteger("Enter the second integer: ");
                    Console.WriteLine($"{i1} + {i2} = {i1+i2}");
                    Pause("Press enter to continue");
                    return true;    // Keep running the main menu
                case "2": // Do whatever option 2 is
                    WriteError("Not yet implemented");
                    Pause("");
                    return true;    // Keep running the main menu
                case "3": // Create and show the sub-menu
                    SubMenu1 sm = new SubMenu1();
                    sm.Run();
                    return true;    // Keep running the main menu
            }
            return true;
        }

        protected override void BeforeDisplayMenu()
        {
            PrintHeader();
        }


        private void PrintHeader()
        {
            SetColor(ConsoleColor.Yellow);
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Campsite Reservation"));
            ResetColor();
        }
    }
}
