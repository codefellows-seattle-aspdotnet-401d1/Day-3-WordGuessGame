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
            Console.WriteLine("2) Add/Remove Words");
            Console.WriteLine("0) Exit Game");
            string filePath = @"C:\Users\akure\Desktop\CodeFellows\WordGame.txt";
            try
            {
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        GameMenu(filePath);
                        break;
                    case 2:
                        EditWordFile(filePath);
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

        static void GameMenu(string filePath)
        {
            Console.Clear();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) Start New Game");
            Console.WriteLine("0) Return to Main Menu");
            try
            {
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        string randomWord = SelectRandomWord(filePath);
                        StartNewGame(randomWord);
                        break;
                    case 0:
                        MainMenu();
                        break;
                    default:
                        GameMenu(filePath);
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("You did something wrong. Shutting down.");
                Console.Read();
            }
        }

        static string[] ReadWordFile(string filePath)
        {

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

        static void EditWordFile(string filePath)
        {
            Console.Clear();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1) Add a word");
            Console.WriteLine("2) Remove a word");
            Console.WriteLine("0) Return to Main Menu");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    AddWordToFile(filePath);
                    EditWordFile(filePath);
                    break;
                case 2:
                    RemoveWordFromFile(filePath);
                    EditWordFile(filePath);
                    break;
                case 0:
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("I didn't understand, please try again.");
                    EditWordFile(filePath);
                    break;
            }
        }

        static void CreateWordFile(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] newWords = new System.Text.UTF8Encoding(true).GetBytes("potato");
                fs.Write(newWords, 0, newWords.Length);
            }

        }

        static void AddWordToFile(string filePath)
        {
            Console.WriteLine("What word would you like to add to the file?");
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(Console.ReadLine().Trim().ToLower() + Environment.NewLine);
            }
        }

        static void RemoveWordFromFile(string filePath)
        {
            Console.WriteLine("What word would you like to remove from the file?");
            string wordToRemove = Console.ReadLine();
            string oldWords;
            string n = "";
            using (StreamReader sr = File.OpenText(filePath))
            {
                while ((oldWords = sr.ReadLine()) != null)
                {
                    if (!oldWords.Contains(wordToRemove))
                    {
                        n += oldWords + Environment.NewLine;
                    }
                }
            }
            File.WriteAllText(filePath, n);
        }

        static void StartNewGame(string randomWord)
        {
            Console.Clear();
            Console.WriteLine("A random word has been chosen!");
            Console.WriteLine($"The word is {randomWord.Length} letters long.");
            char[] progress = new char[randomWord.Length];
            do
            {
                Console.WriteLine("Please guess one or more letters:");
                string guess = Console.ReadLine().ToLower();
                char[] randomWordArray = randomWord.ToArray();
                foreach (var letter in guess.ToArray())
                {
                    if (randomWordArray.Contains(letter))
                    {
                        Console.WriteLine($"The word contains {letter}.");
                        for (int i = 0; i < randomWord.Length; i++)
                        {
                            if (letter == randomWordArray[i])
                            {
                                progress[i] = letter;
                            }
                            else if (progress[i] == '\0')
                            {
                                progress[i] = '_';
                            }
                        }
                    }
                }
                Console.WriteLine($"You've gotten {new string(progress.ToArray())} so far!");
            } while (new string(progress.ToArray()) != new string(randomWord.ToArray()));
            Console.WriteLine("You did it!");
            Console.Read();
        }

        static string SelectRandomWord(string filePath)
        {
            string[] words = ReadWordFile(filePath);
            Random rnd = new Random();
            string randomWord = words[rnd.Next(0, (words.Length - 1))];
            return randomWord;
        }


    }
}
