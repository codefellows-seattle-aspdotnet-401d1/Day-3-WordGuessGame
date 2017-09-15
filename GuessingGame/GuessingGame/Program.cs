using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = @"C:\Projects\Day-3-WordGuessGame\GuessingGame\GuessingGame\assets\words_alpha.txt";
            try
            {
                GameInitialize(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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
            using (var sr = File.OpenText(filePath))
            {
                var words = File.ReadAllLines(filePath);
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
                using (var sw = File.AppendText(filePath))
                {
                    sw.WriteLine(userInput);
                    Console.WriteLine($"You have added \"{userInput}\" to the dictionary list...");
                }
            ViewDictionary(filePath);
        }

        static void DeleteWord(string filePath)
        {
            
        }
        //
        //        static string ChooseWord()
        //        {
        //            return string;
        //        }
        //
        static void ViewDictionary(string filePath)
        {
            var currentStrings = ReadDictionary(filePath);
            Console.WriteLine("Here is the current list of words in your dictionary");
            foreach (var line in currentStrings)
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

          
        }

    }
}
//
