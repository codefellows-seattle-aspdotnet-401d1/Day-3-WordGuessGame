using System;
using System.IO;
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

        static string[] UpdateFile(string filePath, string userInput)
        {
            string[] currentStrings = ReadDictionary(filePath);
            return currentStrings;
        }

        //        static bool DeleteWord()
        //        {
        //            return true;
        //        }
        //
        //        static string ChooseWord()
        //        {
        //            return string;
        //        }
        //
        private static void ViewDictionary(string filePath)
        {
            var currentStrings = ReadDictionary(filePath);
            foreach (var line in currentStrings)
            {
                Console.WriteLine(line);
            }
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
            Console.WriteLine("Please Select an option:");
            Console.WriteLine();
            Console.WriteLine("1. View Current Dictionary List");
            Console.WriteLine("2. Update Current Dictionary List");
            Console.WriteLine("3. Remove Word from Dictionary List");
            Console.WriteLine("4. Play Game");
            Console.WriteLine("5. Exit Game");

            string playersChoice = Console.ReadLine();
        }

    }
}
//
