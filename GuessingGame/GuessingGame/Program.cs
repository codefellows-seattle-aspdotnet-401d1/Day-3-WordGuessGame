using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = $@"{Directory.GetCurrentDirectory()}\words_alpha.txt";
            try
            {
                GameInitialize(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType()}: an Exeption has occured. Contact System admin for more details...");
            }
        }

        static void CreateFile(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] info = new UTF8Encoding().GetBytes("This is the dictionary of words:");
                fs.Write(info, 0, info.Length);
            }
        }

        static string[] ReadDictionary(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string[] words = File.ReadAllLines(filePath);
                string[] dictionaryList = new string[words.Length];
                for (int line = 1; line < words.Length; line++)
                {
                    dictionaryList[line] += words[line];
                }
                return dictionaryList;
            }
        }

        static void AddWord(string filePath, string userInput)
        {
            if (ReadDictionary(filePath).Contains(userInput.Trim()))
                Console.WriteLine("This word is already part of the dictioanry...");
            else
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(userInput.ToLower());
                    Console.WriteLine($"You have added \"{userInput}\" to the dictionary list...");
                }
            ViewDictionary(filePath);
        }

        static void DeleteWord(string filePath, string userInput)
        {
            string[] buffer = ReadDictionary(filePath);
            if (buffer.Contains(userInput.ToLower()))
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == userInput.ToLower().Trim())
                    {
                        buffer[i] = null;
                    }
                }
                string[] newBuffer = buffer.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                File.Delete(filePath);
                CreateFile(filePath);
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    foreach (string word in newBuffer)
                    {
                        sw.WriteLine(word);
                    }
                }
                Console.WriteLine($"You have remove \"{userInput}\" from the dictionary.");
                Console.WriteLine("The current dictionary is...");
                ViewDictionary(filePath);
            }
            else
            {
                Console.WriteLine($"The word {userInput} is not part of the dictionary");
            }
            Console.WriteLine("Press Anykey to continue...");
            Console.Read();
            GameInitialize(filePath);
        }
    
        static void ViewDictionary(string filePath)
        {
            string[] currentStrings = ReadDictionary(filePath);
            foreach (string line in currentStrings)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("Press Anykey to continue ...");
            Console.Read();
            GameInitialize(filePath);
        }

        static void GameInitialize(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateFile(filePath);
                ReadDictionary(filePath);
            }
            else
            {
                ReadDictionary(filePath);
            }


            Console.Clear();
            Console.WriteLine("Please Select an option:");
            Console.WriteLine();
            Console.WriteLine("1. View Current Dictionary List");
            Console.WriteLine("2. Add word to Dictionary List");
            Console.WriteLine("3. Remove Word from Dictionary List");
            Console.WriteLine("4. Play Game");
            Console.WriteLine("5. Exit Game");

            string userInput = Console.ReadLine();
            if (userInput == null)
            {
                userInput = Console.ReadLine();

            }
            else
            {
                switch (userInput)
                {
                    case "1":
                        userInput = null;
                        ViewDictionary(filePath);
                        Console.WriteLine("Here is your current Dictionary");

                        break;
                    case "2":
                        userInput = null;
                        Console.WriteLine("Which word would you like to add?");
                        var wordToBeAdded = Console.ReadLine();
                        AddWord(filePath, wordToBeAdded);
                        break;
                    case "3":
                        userInput = null;
                        Console.WriteLine("Which word would you like to try and remove?");
                        var wordToBeRemoved = Console.ReadLine();
                        AddWord(filePath, wordToBeRemoved);
                        break;
                    case "4":
                        userInput = null;
                        Console.WriteLine("Coming Soon!");
                        Console.WriteLine("Press Anykey to continue ...");
                        Console.Read();
                        GameInitialize(filePath);
                        break;
                    case "5":
                        userInput = null;
                        Console.WriteLine("Thank you for playing my game. Have a good day!");
                        Console.WriteLine("Press Anykey to exit...");
                        Console.Read();
                        break;
                    default:
                        userInput = null;
                        Console.WriteLine("You made an invalid choice, press Anykey to try again");
                        Console.Read();
                        GameInitialize(filePath);
                        break;
                }

            }
        }
    }
}

