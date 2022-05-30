using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagementApp
{
    internal class PropertyChangeMenu
    {
        public static void StartPropertyChangeMenu()
        {
            string userChoice;
            Console.WriteLine("What property do you want to change? Press [1 - plate number, 2 - owner]");
            userChoice = Console.ReadLine();
            Console.Clear();

            while (userChoice != "1" && userChoice != "2")
            {
                Console.WriteLine($"You typed \"{userChoice}\". Insert [1 - plate number, 2 - owner]");
                userChoice = Console.ReadLine();
                Console.Clear();
            }

            switch(userChoice)
            {
                case "1":
                    DataBase.ChangePlateNumber();
                    break;
                case "2":
                    DataBase.ChangeOwner();
                    break;
            }
        }
    }
}
