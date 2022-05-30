using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagementApp
{
    public class MainMenu
    {
        static string[] availableOptions = {
            "Add new car",
            "Find cars",
            "Display all the cars",
            "Remove a car",
            "Change car's property",
            "Exit the program" };

        static int currentMode = 0;
        public static void StartMainMenu()
        {
            while (true)
            {
                Console.CursorVisible = false;

                DisplayMenu();
                ChangeMode();
                RunChosenMode();
            }
        }

        private static void RunChosenMode()
        {
            switch (currentMode)
            {
                /*case 0: Console.Clear(); CarsService.AddNewCar(); break;*/
                case 0: Console.Clear(); DataBase.AppendToDb(); break;
                case 1: Console.Clear(); DataBase.FindCar(); break;
                case 2: Console.Clear(); DataBase.DisplayDbData(); break;
                case 3: Console.Clear(); DataBase.RemoveFromDB(); break;
                case 4: Console.Clear(); PropertyChangeMenu.StartPropertyChangeMenu(); break;
                case 5: Environment.Exit(0); break;
            }
        }
        private static void ChangeMode()
        {
            do
            {
                ConsoleKeyInfo button = Console.ReadKey();
                if (button.Key == ConsoleKey.UpArrow)
                {
                    currentMode = (currentMode > 0) ? currentMode - 1 : availableOptions.Length - 1;
                    DisplayMenu();
                }
                else if (button.Key == ConsoleKey.DownArrow)
                {
                    currentMode = (currentMode + 1) % availableOptions.Length;
                    DisplayMenu();
                }
                else if (button.Key == ConsoleKey.Escape)
                {
                    currentMode = availableOptions.Length - 1;
                    break;
                }
                else if (button.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);
            

            }

        static void DisplayMenu()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(">>>> Choose option <<<<\n\n");

            for (int i = 0; i < availableOptions.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;

                if (i == currentMode)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.WriteLine(availableOptions[i]);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(availableOptions[i]);
                }
            }
        }
    }
}
