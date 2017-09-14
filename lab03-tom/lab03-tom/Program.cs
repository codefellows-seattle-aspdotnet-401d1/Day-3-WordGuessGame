using System;
using System.IO;

namespace lab03_tom
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Josie's Word Guess Game!");
            InputHandler();
            Console.Read();
        }

        static public int HomeNav()
        {
            try
            {
                Console.WriteLine("What would you like to do? Enter a number:");
                Console.WriteLine("1. Play");
                Console.WriteLine("2. View text file");
                Console.WriteLine("3. Add text");
                Console.WriteLine("4. Delete text");
                Console.WriteLine("5. Exit");

                int input = Convert.ToInt32(Console.ReadLine());
                return input;
            }
            catch(FormatException)
            {
                Console.WriteLine("Enter a number");
            }
            return 0;
        }

        static public void InputHandler()
        {
            string filePath = @"C:\Users\Tom\source\repos\Day-3-WordGuessGame\MysteryWords.txt";


            if (!File.Exists(filePath))
            {
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("hippo");
                    sw.WriteLine("code");
                    sw.WriteLine("meow");
                }
                //using (FileStream fs = File.Create(filePath))
                //{
                //    Byte[] myText = new System.Text.UTF8Encoding(true).GetBytes("This is my text");
                //    fs.Write(myText, 0, myText.Length);
                //}
            }

            int selection = HomeNav();

            switch (selection)
            {
                case 1:
                    Game(filePath);
                    break;
                case 2:
                    ViewText(filePath);
                    break;
                case 3:
                    AddText(filePath);
                    break;
                case 4:
                    DeleteText(filePath);
                    break;
                case 5:
                    ExitGame();
                    break;
                default:
                    Console.WriteLine("Please Choose from available options");
                    InputHandler();
                    break;
            }
        }

        static public void Game(string filePath)
        {
            string[] words = File.ReadAllLines(filePath);
            int length = words.Length;
            Console.WriteLine("Guess the mystery word!");
            string guess = Console.ReadLine();
            foreach (string line in words)
            {
                if (guess == line)
                {
                    Console.WriteLine("You got it!");
                    break;
                }

            }


        }

        static public void ExitGame()
        {
            Console.WriteLine("Goodbye! Press return to exit.");
        }

        static public void ViewText(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {

                string[] words = File.ReadAllLines(filePath);

                int length = words.Length;
                foreach (string line in words)
                {
                    Console.WriteLine(line);
                }

            }
            InputHandler();


        }

        static public void AddText(string filePath)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                Console.WriteLine("Enter a new mystery word!");
                //sw.Write(Environment.NewLine);
                sw.WriteLine(Console.ReadLine());
            }
            InputHandler();

        }

        static public void DeleteText(string filePath)
        {
            File.Delete(filePath);
            Console.WriteLine("Your file has been deleted. Press Return to exit.");
            Console.Read();

        }

    }
}

//Josie Cat has requested that a "Word Guess Game" be built. The main idea of the game is she must guess what a mystery word is by 
//inputitng either (1) letter or a sequence of letters at a time. The game should save all of her guesses (both correct and incorrect) 
//throughout each session of the game, along with the ability to show her how many letters out of the word she has guessed correctly.

//Each time a new game session starts, the mystery word chosen should come from an external text file that randomly selects one 
//of the words listed.This file should be editable by Josie so that she may view, add, and delete words as she wishes.
//She expects the game to have a simple user interface that is easy to navigate.

//Using everything you've learned and researched up to this point, create a word guess game that will meet all of the requiements 
//described in the user story above.

