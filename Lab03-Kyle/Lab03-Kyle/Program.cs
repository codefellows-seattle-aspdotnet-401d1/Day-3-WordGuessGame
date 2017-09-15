using System;
using System.IO;
using System.Text;

namespace Lab03_Kyle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's play a Word Guess Game!");

            //Using Demo Code from Class3 as a starting point, will refactor as needed.

            //Need a method to randomly select one of the words
            //Need a method to input the user guesses 
            //Need a method to store and output correct and incorrect answers
            //Need a method to create exit the game once finished
            //Need a method to start a new game with a new word
            //Need to be able to handle error exceptions
            //Need to create a method to be able to CRUD words


            //Need a method to create a file to store the words to be guessed
            try
            {
                string filePath = @"C:\Users\Kyler\source\repos\Day-3-WordGuessGame\Lab03-Kyle\Lab03-Kyle\assets\key.txt";

                Init(filePath);

            }
            catch (DirectoryNotFoundException)
            {

                Console.WriteLine("The Directory you suggested does not exist");
            }

        }

        static void Init(string path)

        {
            string[] mysteryWords = new String[7];
            mysteryWords[0] = "cat";
            mysteryWords[1] = "dog";
            mysteryWords[2] = "bird";
            mysteryWords[3] = "fish";
            mysteryWords[4] = "frog";
            mysteryWords[5] = "lizard";
            mysteryWords[6] = "turtle";

            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    for (int i = 0; i < 7; i++)
                    {
                        Byte[] myText = new UTF8Encoding(true).GetBytes(mysteryWords[i]);
                        fs.Write(myText, 0, myText.Length);
                        byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                        fs.Write(newline, 0, newline.Length);
                    }
                }
            }


        }



        static void ReadFile(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string[] mysteryWords = File.ReadAllLines(filePath);

                int length = mysteryWords.Length;
                foreach (string line in mysteryWords)
                {
                    Console.WriteLine(line);
                }

                Random rand = new Random();
                int x = (rand.Next(0, mysteryWords.Length));

                string y = mysteryWords[x];
            

                    Console.WriteLine("Guess a letter or a word to solve the Mystery Word.");

                    string UserGuess = (Console.ReadLine()).ToLower();

                    int incorrect = 0;

                    string incorrectGuess = " ";
                
            }

            AddText(filePath);
            //DeleteText(filePath);
        }


        static void AddText(string filePath)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.Write(Environment.NewLine);
                sw.WriteLine("");
            }
        }

        static void DeleteText(string filePath)
        {
            File.Delete(filePath);
            Console.WriteLine("Your word has been deleted");
            Console.Read();
        }
    }
}
