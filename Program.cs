using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab03_guessing_game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Josie Cat's word guessing game.");
            Console.WriteLine("Let's get started.  What would you like to do?  Enter a number.");
            Console.WriteLine("1. Add words to the List");
            Console.WriteLine("2. Remove words from the List");
            Console.WriteLine("3. View the word List");
            Console.WriteLine("4. Play the game!");

            HandleWordList();

        }
        static List<string> HandleWordList()
        {
            string filePath = @"C:\Users\admin\Documents\Reference\GameWordList.txt";
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    Byte[] myText = new UTF8Encoding(true).GetBytes("josiecat");
                    fs.Write(myText, 0, myText.Length);

                    string[] words = File.ReadAllLines(filePath);
                    List<string> wordsList = new List<string>(words);
                    return wordsList;
                    AddText(filePath);
                }
            }
            else
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    string[] words = File.ReadAllLines(filePath);
                    List<string> wordsList = new List<string>(words);
                    return wordsList;
                }
                AddText(filePath);
                //DeleteWordList(filePath);
            }
        }

        static void AddText(string filePath)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                Console.WriteLine("Please type another word.");
                sw.Write(Environment.NewLine);
                sw.WriteLine(Console.ReadLine().ToLower());
            }
        }

        static void DeleteWordList(string filePath)
        {
            File.Delete(filePath);

        }
        //Method to pull a random word from word list

        //Method to create an alphabet list
        static List<string> AlphaList()
        {
            string[] alphaList = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            List<string> alphabetSoup = new List<string>(alphaList);
            return alphabetSoup;
        }

        //Method to remove user guess from alphabet list

        //Method to compare user guess to char in random word
        
    }
}
