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
            string filePath = @"C:\Users\admin\Documents\Reference\GameWordList.txt";
            Console.WriteLine("Welcome to Josie Cat's word guessing game.");
            GameController();
        }

        static int GetStarted()
        { 

            Console.WriteLine("Let's get started.  What would you like to do?  Enter a number.");
            Console.WriteLine("1. Add words to the List");
            Console.WriteLine("2. Remove words from the List");
            Console.WriteLine("3. View the word List");
            Console.WriteLine("4. Play the game!");
            int userInput = Convert.ToInt32(Console.ReadLine());
            return userInput;
        }

        static public void GameController()
        {
            int selection = GetStarted();
            switch (selection)
            {
                case 1:
                    //add words
                    AddText(filePath);
                    break;
                case 2:
                    //remove words
                    break;
                case 3:
                    //view the word list
                    break;
                case 4:
                    //Play!
                    GetRandomWord();
                default:
                    Console.WriteLine("Please choose from available options.");
                    GetStarted();
                    break;
            }
        }


        static List<string> HandleWordList(string filePath)
        {
            //string filePath = @"C:\Users\admin\Documents\Reference\GameWordList.txt";
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    Byte[] myText = new UTF8Encoding(true).GetBytes("josiecat");
                    fs.Write(myText, 0, myText.Length);

                    string[] words = File.ReadAllLines(filePath);
                    List<string> wordsList = new List<string>(words);
                    return wordsList;
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
            }
        }

        static void AddText(string filePath)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                Console.WriteLine("Please type another word.");
                sw.Write(Environment.NewLine);
                sw.WriteLine(Console.ReadLine().ToLower());
                GetStarted();
            }
        }

        static void RemoveText(string filePath)
        {
            using (StreamWriter sw = File.AppendText(filePath))

        }

        static void DeleteWordList(string filePath)
        {
            File.Delete(filePath);

        }
        //Method to pull a random word from word list
        static string GetRandomWord(List<string> wordsList)
        {
            Random randNum = new Random();
            int aRandomPos = randNum.Next(wordsList.Count-1);
            string randomWord = wordsList[aRandomPos];
            Console.WriteLine($"This is the random word {randomWord}");
            Console.Read();
            CompareUserGuess(randomWord);
            return randomWord;
        }

        //Method to create an alphabet list
        static List<string> AlphaList()
        {
            string[] alphaList = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            List<string> alphabetSoup = new List<string>(alphaList);
            return alphabetSoup;
        }

        //Method to remove user guess from alphabet list

        //Method to compare user guess to char in random word
        static void CompareUserGuess(string randomWord)
        {
                int i = randomWord.Length-1;
            do
            {
                Console.WriteLine("Here we guess a letter");
                Console.WriteLine($"You have {i} guesses.");
                Console.Read();
                string userGuess = Console.ReadLine();
                if (randomWord.Contains(userGuess))
                {
                    Console.WriteLine("Bingo!");
                    Console.Read();
                }
                else
                {
                    Console.WriteLine("Uh oh, bad guess.");
                    i--;
                    Console.Read();
                }

            } while (i > 0);
            Console.WriteLine("Out of guesses");
            Console.Read();
        }

    }
}
