using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GuessingGame
{
    class Program
    {
        //TODO Change all press Anykey to continue to system pauses to feel less clunky
        static void Main(string[] args)
        {
            string filePath = $@"{Directory.GetCurrentDirectory()}\words_alpha.txt";
            try
            {
                GameInitialize(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType()}: an Exception has occured. Contact System admin for more details...");
            }
        }

        static void CreateFile(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] info = new UTF8Encoding().GetBytes("");
                fs.Write(info, 0, info.Length);
            }
        }

        static string[] ReadDictionary(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string[] words = File.ReadAllLines(filePath);
                string[] dictionaryList = new string[words.Length];
                for (int line = 0; line < words.Length; line++)
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
        }

        static void DeleteWord(string filePath, string userInput)
        {
            string[] buffer = ReadDictionary(filePath);
            if (buffer.Contains(userInput.ToLower().Trim()))
            {
                for (int line = 1; line < buffer.Length; line++)
                {
                    if (buffer[line] == userInput.ToLower().Trim())
                    {
                        buffer[line] = null;
                    }
                }
                string[] newBuffer = buffer.Where(word => !string.IsNullOrEmpty(word)).ToArray();
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
        }
    
        static void ViewDictionary(string filePath)
        {
            foreach (string word in ReadDictionary(filePath))
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("Press Anykey to continue ...");
            Console.Read();
        }

        static string PickRandomWord(string filePath)
        {
            Random randomWord = new Random();
            return ReadDictionary(filePath)[randomWord.Next(0, ReadDictionary(filePath).Length)];
        }

        static string[] RandomWordIntoArray(string sessionWord)
        {
            string[] sessionWordArray = new string[sessionWord.Length];
            for (int wordIndex = 0; wordIndex < sessionWord.Length; wordIndex++)
            {
                sessionWordArray[wordIndex] += sessionWord[wordIndex];
            }
            return sessionWordArray;
        }

        static string[] RandomWordBuffer(string sessionWord)
        {
            string[] sessionWordBuffer = new string[sessionWord.Length];
            for (int wordIndex = 0; wordIndex < sessionWord.Length; wordIndex++)
            {
                sessionWordBuffer[wordIndex] += "_";
            }
            return sessionWordBuffer;
        }

        //TODO Refacter GameLogic() method. Create method to handle compairing user input && updating buffer
        static void GameLogic(string randomWord)
        {
            string[] characterArray = RandomWordIntoArray(randomWord);
            string[] buffer = RandomWordBuffer(randomWord);

            int strikes = 3;
            List<string> inputLog = new List<string>();
            for (int remainingGuesses = characterArray.Length + strikes; remainingGuesses > 0; remainingGuesses--)
            {
                Console.Clear();
                Console.WriteLine($"You have {remainingGuesses} remaining...");
                foreach (string index in buffer)
                { 
                    Console.Write(index);
                }
                Console.WriteLine();

                Console.Write("Please guess a letter: ");
                string userInput = Console.ReadLine();
                inputLog.Add(userInput);
                if (characterArray.Contains(userInput.ToLower()))
                {

                    for (int index = 0; index < characterArray.Length; index++)
                    {
                        if (userInput == characterArray[index])
                        {
                            buffer[index] = userInput;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{userInput} is not part of the word");
                    Console.WriteLine("Press Anykey to continue ...");
                    Console.Read();
                }
                if (characterArray.SequenceEqual(buffer))
                {
                    remainingGuesses = 0;
                    Console.Clear();
                    Console.WriteLine($"Congratulations you've guessed the word {randomWord}!");
                    Console.WriteLine("Press Anykey to continue ...");
                    Console.Read();
                }
            }
        }

        static void GameInitialize(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateFile(filePath);
            }


            Console.Clear();

            Console.WriteLine("THREE STRIKES AND YOU'RE OUT!");

            Console.WriteLine("1. View Current Dictionary List");
            Console.WriteLine("2. Add word to Dictionary List");
            Console.WriteLine("3. Remove Word from Dictionary List");
            Console.WriteLine("4. Play Game");
            Console.WriteLine("5. Exit Game");
            Console.WriteLine();
            Console.Write("Please Select an option: ");

            string userInput;
            do
            {
            userInput = Console.ReadLine();
            } while (string.IsNullOrEmpty(userInput));
            Console.Clear();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Here is your current Dictionary...");
                    ViewDictionary(filePath);
                    break;
                case "2":
                    Console.WriteLine("Which word would you like to add?");
                    var wordToBeAdded = Console.ReadLine();
                    if (string.IsNullOrEmpty(wordToBeAdded))
                    {
                        Console.WriteLine("You didn't enter a word. Please try again ...");
                        GameInitialize(filePath);
                    }
                    AddWord(filePath, wordToBeAdded);
                    break;
                case "3":
                    Console.WriteLine("Which word would you like to try and remove?");
                    var wordToBeRemoved = Console.ReadLine();
                    DeleteWord(filePath, wordToBeRemoved);
                    break;
                case "4":
                    if (ReadDictionary(filePath).Length == 0)
                    {
                        Console.WriteLine("You must add words to your dictionary beofore you play. Press Anykey to try again...");
                        Console.Read();
                        GameInitialize(filePath);
                    }
                    Console.Clear();
                    GameLogic(PickRandomWord(filePath));
                    break;
                case "5":
                    Console.WriteLine("Thank you for playing my game. Have a good day!");
                    Console.WriteLine("Press Anykey to exit...");
                    Console.Read();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("You made an invalid choice, press Anykey to try again...");
                    Console.Read();
                    GameInitialize(filePath);
                    break;
            }
            GameInitialize(filePath);
        }
    }
}

