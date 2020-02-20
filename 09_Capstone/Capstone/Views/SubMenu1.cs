using System;
using System.Collections.Generic;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class SubMenu1 : CLIMenu
    {
        // Store any private variables, including DAOs here....


        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public SubMenu1(/** DAOs may be passed in... ***/) :
            base("Sub-Menu 1")
        {
            // Store any values or DAOs passed in....
        }

        protected override void SetMenuOptions()
        {
            this.menuOptions.Add("1", "View Campgrounds");
            this.menuOptions.Add("2", "Search For Reservations");
            this.menuOptions.Add("3", "Return to Parks Interface");
            this.quitKey = "3";
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
                    WriteError("Not yet implemented");
                    Pause("");
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
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Sub-Menu 1"));
            ResetColor();
        }

    }
}
