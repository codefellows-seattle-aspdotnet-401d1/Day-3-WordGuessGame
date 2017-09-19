using System;
using System.IO;

namespace lab03_miya
{
    class Program
    {
        //this is my main entry point for the project
        //it sets a reference to a file path that I've set and it will open a text file called word-bank
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\shami\source\repos\Day-3-WordGuessGame\word-bank.txt";

            //if the text file does not already exist, the system will create a text file and write "Hi Josie, welcome to your guessing game.", in the text file
            if (!File.Exists(filePath))
            {
                using (FileStream fs = File.Create(filePath))
                {
                    Byte[] myText = new System.Text.UTF8Encoding(true).GetBytes("Hi Josie, welcome to your guessing game.");

                    fs.Write(myText, 0, myText.Length);

                }
            }
            else
            {
                //if the text file does already exist, the system will read the text and show it to the user
                string[] words = File.ReadAllLines(filePath);

                int length = words.Length;
            }
            //the InputHandler() method refers to my main menu and saves the user's input as a number representing what the user would like to do
            InputHandler(filePath);
        }
        static public int NavigateGame()
        {
            try
            {
                Console.WriteLine("Welcome to your game's homepage! How would you like to proceed?");
                Console.WriteLine("1. Start a new game.");
                Console.WriteLine("2. Add words to your word bank.");
                Console.WriteLine("3. View all of the words in your word bank.");
                Console.WriteLine("4. Delete the word bank. Think carefully before choosing this option.");
                Console.WriteLine("5. End your game.");
                int input = Convert.ToInt32(Console.ReadLine());
                return input;
            }
            catch(FormatException fe)
            {
                Console.WriteLine("Please enter a number.");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Great! /Moving on.");
            }
            return 0;
        }
        //this method handles the user's selection and assigns a number to each method that the user can access
        static public void InputHandler(string filePath)
        {
            int selection = NavigateGame();

            switch (selection)
            {
                case 1:
                    StartGame(filePath);
                    break;
                case 2:
                    AddText(filePath);
                    break;
                case 3:
                    ViewWords(filePath);
                    break;
                case 4:
                    DeleteText(filePath);
                    break;
                case 5:
                    ExitGame(filePath);
                    break;
                default:
                    Console.WriteLine("Please choose from available options.");
                    InputHandler(filePath);
                    break;
            }
        }

        //Josie can add words to the file as she wishes
        static void AddText(string filePath)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                Console.WriteLine("Please enter a word you'd like added to the word bank.");
                sw.Write(Environment.NewLine);
                sw.WriteLine(Console.ReadLine());
            }
            Console.WriteLine();
            InputHandler(filePath);
        }

        //Josie can delete the file if she wishes
        static void DeleteText(string filePath)
        {
            File.Delete(filePath);
            Console.WriteLine("Your file has been deleted.");
            Console.WriteLine();
            NavigateGame();
        }

        //Josie can view the file as she wishes
        static void ViewWords(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string[] words = File.ReadAllLines(filePath);

                int length = words.Length;
                foreach (string line in words)
                {
                    Console.WriteLine(line);
                }
            }
            Console.WriteLine();
            NavigateGame();
        }

        //Josie can exit the game as she wishes
        static void ExitGame(string filePath)
        {
            Console.WriteLine("Bye Bye.");
        }

        //need a method that saves all of her guesses(both correct and incorrect) throughout each session of the game, as well as keeping a record of the letters they have attempted so far
        //along with the ability to show her how many letters out of the word she has guessed correctly

        public static void StartGame(string filePath)
        {
            string[] words = File.ReadAllLines(filePath);
            var r = new Random();
            var lineNumber = r.Next(0, words.Length - 1);
            string lines = words[lineNumber];

            int guesses = 0;

            Console.WriteLine("Guess a word, any word. If you enter a letter at this point, you will be incorrect.");

            string stringGuessed = (Console.ReadLine()).ToLower();

            guesses += guesses;

            if (lines == stringGuessed)
            {
                Console.WriteLine("You psychic, you! I mean, it's not like YOU created the word bank or anything.");
            }
            //working on a method that tests whether a letter guessed is contained within the random word that was generated
            else if (lines.ToLower().Contains(stringGuessed))
            {
                Console.WriteLine("That letter is a part of this word. Keep guessing.");
            }
            else
            {
                Console.WriteLine("Wrong!");
                Console.WriteLine($"The word you failed to guess was {lines}.");
            }             
            Console.WriteLine();
            InputHandler(filePath);
        }
    }
}

//# Directions
//The directions below mock what an actual client requirements document may contain.It is your job, as a developer, to interpret these directions and
//create a program based on what is stated below.

//Josie Cat has requested that a "Word Guess Game" be built. The main idea of the game is she must guess what a mystery word is by inputitng
//either (1) letter or a sequence of letters at a time.The game should save all of her guesses(both correct and incorrect) throughout each session of the game,
//along with the ability to show her how many letters out of the word she has guessed correctly.

//Each time a new game session starts, the mystery word chosen should
//come from an external text file that randomly selects one of the words listed.This file should be editable by Josie so that
//she may view, add, and delete words as she wishes.She expects the game to have a simple user interface that is easy to navigate.

//Using everything you've learned and researched up to this point, create a word guess
//game that will meet all of the requiements described in the user story above.
 

//## ReadMe
//- Your readme should include the following information:
//	- How long did it take you to complete this assignment?
//	- What did you struggle with? Why? How did you solve?
//	- What did you learn during this assignment?
//    - What resources did you utilize for this assingment?

//## Components
//- The program (should) contain the following
//    - Suggestion: include methods for each action (Home navigation, View words in the text file, add a word to the text file, Remove words from a text file, exit the game, start a new game, play the game, + any more you may see neccesary)
//    - When playing a game, you should bring in all the words that exist in the text file, and* randomly* select one of the words to output to the conole for the user to guess
//    - You should have a record the letters they have attempted so far
//    - If they guess a correct letter, display that letter in the console for them to refer back to when making guesses(i.e.C _ T S)
//    - Errors should be handled through try/catch statements
//    - You may use any shortcuts or 'helper' methods in this project.Do not create external classes to accomplish this task.

//## Rubric
//- 3pts: Application runs without any errors
//- 2pt: System.IO // CRUD file manipulation
//- 2pt: Output user response with game progress
//- 2pt: Readme included with answers to questions
//- 1pt: Simple user interface