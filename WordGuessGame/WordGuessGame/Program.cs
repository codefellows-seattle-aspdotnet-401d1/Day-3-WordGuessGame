using System;
using System.IO;
using System.Text;

namespace WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;
            string[] menu = new string[] {
                "You selected: ",
                "1. Play the game",
                "2. New Game",
                "3. View words in the game",
                "4. Add a word to the game",
                "5. Remove words from the game",
                "6. Exit the game"
            };
            // this is a while loop so a player can keep asking questions
            // this is the main admin menu
            while (playing)
            {
                int playCheck = MenuPrompt(menu);
                if (playCheck == 6)
                {
                    playing = false;
                    break;
                }
                else if (playCheck == -2)
                {
                    continue;
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Thanks for playing!");
            Console.Read();
        }

        // This creates a menu using the array passed in
        // It handles Home navigation as well as game navigation
        static int MenuPrompt(string[] menu)
        {
            string filePath = @"D:\Lab03\wordsFile.txt";
            Console.WriteLine();
            Console.WriteLine("Guess Game Menu");
            Console.WriteLine();
            Console.WriteLine(menu[1]);
            Console.WriteLine(menu[2]);
            Console.WriteLine(menu[3]);
            Console.WriteLine(menu[4]);
            Console.WriteLine(menu[5]);
            Console.WriteLine(menu[6]);
            Console.WriteLine();
            // TODO: additional logic for the main or game menu per option
            switch (NumberInput())
            {
                case 1:
                    Console.WriteLine(menu[0] + menu[1]);
                    return PlayGame();
                case 2:
                    Console.WriteLine(menu[0] + menu[2]);
                    return ExitGame();
                case 3:
                    Console.WriteLine(menu[0] + menu[3]);
                    return ViewWords(filePath);
                case 4:
                    Console.WriteLine(menu[0] + menu[4]);
                    return AddWords(filePath);
                case 5:
                    Console.WriteLine(menu[0] + menu[5]);
                    return RemoveWords(filePath);
                case 6:
                    Console.WriteLine(menu[0] + menu[6]);
                    return ExitGame();
                default:
                    Console.WriteLine("Invalid selection!");
                    return -2;
            }
        }

        // this method is input validation to get an integer from the user
        static int NumberInput()
        {
            bool badInput = true;
            Console.WriteLine("Please enter an integer");
            while (badInput)
            {// exception block on my only really fragile code
                string y = Console.ReadLine();
                try
                {
                    int z = Convert.ToInt32(y);
                    return z;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please make another attempt to enter an integer");
                }
                catch (Exception monkeybutt)
                {
                    Console.WriteLine($"You threw an {monkeybutt}! This is probably really bad!");
                    throw (monkeybutt);
                }
            }
            return 0;
        }

        // play the game
        static int PlayGame()
        {
            bool playing = true;
            string[] menu = new string[] {
                "You selected: ",
                "1. Guess a letter",
                "2. guess a series of letters",
                "3. Show number of correct letters",
                "4. Show correct guesses",
                "5. Show incorrect guesses",
                "6. Exit the game"
            };
            // this is a while loop so a player can keep asking questions
            // this is the menu while playing the game
            while (playing)
            {
                int playCheck = MenuPrompt(menu);
                if (playCheck == 2)
                {
                    playing = false;
                    break;
                }
                else if (playCheck == -2)
                {
                    continue;
                }
                else
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Game Over!");
            return 1;
        }

        // start a new game
        static int NewGame()
        {
            Console.WriteLine("Sorry, this part of the game is still under construction!");
            // wipe out where the current word is
            // also wipe out the current guesses
            // search for a new random word
            // write the random word to the file
            return 3;
        }

        // View words in the text file
        static int ViewWords(string filePath)
        {
            // just display all the words
            Console.WriteLine("Sorry, this part of the game is still under construction!");
            return 4;
        }

        // add a word to the text file
        static int AddWords(string filePath)
        {

        return 5;
        }



        // Remove words from a text file
        static int RemoveWords(string filePath)
        {
            // take text input for the word to be deleted 
            // search for the word in the file and delete it
            Console.WriteLine("Sorry, this part of the game is still under construction!");
            return 6;
        }
        // exit the game
        static int ExitGame()
        {
            // this just breaks the loop in the controlling function
            return 6;
        }

        /***** private helper methods *****/

        // create the file if it doesn't exist
        private static void NoFile(string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                // TODO make this better, byte isn't good
                Byte[] myText = new UTF8Encoding(true).GetBytes("fiddlesticks");

                fs.Write(myText, 0, myText.Length);
            }
        }
    }
}

/*  Directions

    The directions below mock what an actual client requirements document may contain.
        It is your job, as a developer, to interpret these directions and create a program based on what is stated below.
        
    Josie Cat has requested that a "Word Guess Game" be built.
        The main idea of the game is she must guess what a mystery word is by inputitng either (1) letter or a sequence of letters at a time.
        The game should save all of her guesses(both correct and incorrect) throughout each session of the game,
        along with the ability to show her how many letters out of the word she has guessed correctly.
    Each time a new game session starts, the mystery word chosen should come from an external text file that randomly selects one of the words listed.
        This file should be editable by Josie so that she may view, add, and delete words as she wishes.
        She expects the game to have a simple user interface that is easy to navigate.    
    Using everything you've learned and researched up to this point, create a word guess game that will meet all of the requiements described in the user story above.

    Components

    The program(should) contain the following
    Methods for each action(
        Home navigation, DONE
        View words in the text file, 
        add a word to the text file, 
        Remove words from a text file, 
        exit the game, DONE
        start a new game, 
        play the game)
    When playing a game, you should: 
        bring in all the words that exist in the text file, 
        and randomly select one of the words to output to the conole for the user to guess
    You should have a record the letters they have attempted so far
    If they guess a correct letter, display that letter in the console for them to refer back to when making guesses(i.e.C _ T S)
    Errors should be handled through try/catch statements
    You may use any shortcuts or 'helper' methods in this project.Do not create external classes to accomplish this task.

    2 hours in: 188 lines of code without doing any file I/O, some good scaffolding

    relied heavily on the demo from class 3
 */