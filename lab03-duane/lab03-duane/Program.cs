using System;
using System.IO;

namespace lab03_duane
{
    class Program
    {

        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Duane Rush\source\repos\wordguess.txt";


            //AddWords(filePath);
            //UserInterface();
            //ViewWords(filePath);
            
            //RandomWord(filePath);
            //WordGame(filePath);
        }

        static int UserInterface()
        {
            try
            {
                Console.WriteLine("Welcome to the Word Guessing Game: ");
                Console.WriteLine("1. Start a New Game ");
                Console.WriteLine("2. Add a Word to the game");
                Console.WriteLine("3. Delete a word from the game");
                int input = Convert.ToInt32(Console.ReadLine());
                return input;
            }
            catch (FormatException fe)
            {
                Console.WriteLine("enter a number option");
            }
            return 0;
        }

        static void ViewWords(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string[] allWords = File.ReadAllLines(filePath);
                Console.WriteLine("Here are all the words in the game");
                foreach (string line in allWords)
                {
                    Console.WriteLine(line);
                }
                Console.Read();
            }
        }


        static void AddWords(string filePath)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                Console.WriteLine("Add a new word to the game");
                sw.WriteLine(Console.ReadLine());

            }
        }

        static public int RandomWord(string filePath)
        {
            string[] wordsArray = File.ReadAllLines(filePath);

            Random word = new Random();
            int result = word.Next(0, wordsArray.Length);

            Console.WriteLine(wordsArray[result]);
            Console.Read();

            return result;
        }

        static void WordGame(string result)
        {
            string randomWord = result;
            Console.WriteLine("Lets play a word guessing game enter a letter");
            string userGuess = Console.ReadLine();

            if (randomWord.Contains(userGuess.ToLower()))
            {
                Console.WriteLine("Guess Right");
            }
            else
            {
                Console.WriteLine("try again");
            }

            Console.Read();
        }
    }
}


