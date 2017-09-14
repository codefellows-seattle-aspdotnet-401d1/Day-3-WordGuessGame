using System;
using System.IO;
using System.Linq;

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
            catch (FormatException fe)
            {
                Console.WriteLine("Invalid text entered.");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType()} has occurred. Press Enter to exit.");
                Console.Read();
            }
        }

        static void GameMenu()
        {
            Console.Clear();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) Start New Game");
            Console.WriteLine("2) Resume Game");
            Console.WriteLine("0) Return to Main Menu");
            try
            {
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        string randomWord = SelectRandomWord();
                        StartNewGame(randomWord);
                        break;
                    case 2:
                        ResumeGame();
                        break;
                    case 0:
                        MainMenu();
                        break;
                    default:
                        GameMenu();
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        static string[] ReadWordFile()
        {
            string filePath = @"C:\Users\akure\Desktop\CodeFellows\WordGame.txt";

            if (!File.Exists(filePath))
            {
                CreateWordFile(filePath);
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string[] words = File.ReadAllLines(filePath);
                    return words;
                }
            }
            else
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string[] words = File.ReadAllLines(filePath);
                    return words;
                }
            }
        }

        static void EditWordFile()
        {

        }

        static void CreateWordFile(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] newWords = new System.Text.UTF8Encoding(true).GetBytes("potato");
                fs.Write(newWords, 0, newWords.Length);
            }
            
        }

        static void AddWordToFile()
        {

        }

        static void RemoveWordFromFile()
        {

        }

        static void StartNewGame(string randomWord)
        {
            Console.Clear();
            Console.WriteLine("A random word has been chosen!");
            Console.WriteLine($"The word is {randomWord.Length} letters long.");
            char[] progress = new char[randomWord.Length];
            Console.WriteLine("Please guess one or more letters:");
            string guess = Console.ReadLine().ToLower();
            char[] randomWordArray = randomWord.ToArray();
            foreach (var letter in guess.ToArray())
            {
                if (randomWordArray.Contains(letter))
                {
                    Console.WriteLine($"The word contains a {letter}.");
                    for (int i = 0; i < randomWord.Length; i++)
                    {
                        if (letter == randomWordArray[i])
                        {
                            progress[i] = letter;
                        }
                    }
                }
            }
        }

        static void ResumeGame()
        {

        }

        static string SelectRandomWord()
        {
            string[] words = ReadWordFile();
            Random rnd = new Random();
            string randomWord = words[rnd.Next(0, (words.Length - 1))];
            return randomWord;
        }


    }
}
