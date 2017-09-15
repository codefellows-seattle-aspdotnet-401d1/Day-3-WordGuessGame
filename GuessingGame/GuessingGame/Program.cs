using System;
using System.IO;

namespace GuessingGame
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public bool CreateFile()
        {

            return true;
        }

        public static string[] ReadDictionary()
        {
            var filePath = @"C:\projects\401d1\Day-3-WordGuessGame\GuessingGame\GuessingGame\assets\words_alpha.txt";
            if (!File.Exists(filePath))
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string[] words = File.ReadAllLines(filePath);
                    return words;
                }
            }
        }

        public static bool UpdateFile()
        {
            return true;
        }
        public bool DeleteWord()
        {
            return true;
        }

        public static string ChooseWord()
        {
            return string;
        }

        public static string ViewDictionary()
        {
            return string;
        }
    }
}

