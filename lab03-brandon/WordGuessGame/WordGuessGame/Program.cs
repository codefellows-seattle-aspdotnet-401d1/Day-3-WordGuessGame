using System;

namespace WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Please enter a number for your selection:");
            Console.WriteLine("1) Game Menu");
            Console.WriteLine("2) Read Word File");
            Console.WriteLine("3) Add/Remove Words");
            Console.WriteLine("0) Exit Game");
            try
            {
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        GameMenu();
                        break;
                    case 2:
                        ReadWordFile();
                        break;
                    case 3:
                        EditWordFile();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number");
                        MainMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.InnerException}: Exception has occurred. Press any key to return to Main Menu.");
                Console.Read();
                MainMenu();

            }
        }

        static void GameMenu()
        {

        }

        static void CreateWordFile()
        {

        }

        static void ReadWordFile()
        {

        }

        static void EditWordFile()
        {

        }

        static void AddWordToFile()
        {

        }

        static void RemoveWordFromFile()
        {

        }

        static void StartNewGame()
        {

        }

        static void ResumeGame()
        {

        }

        static void SelectRandomWord()
        {

        }


    }
}
