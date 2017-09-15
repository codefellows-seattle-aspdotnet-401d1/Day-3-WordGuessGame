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
                throw;
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
            return ReadDictionary(filePath);
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
        //        static string ViewDictionary()
        //        {
        //            return string;
        //        }
    }
}
//
