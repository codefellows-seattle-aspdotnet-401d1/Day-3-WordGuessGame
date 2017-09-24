using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace WordGuessGame
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;
            string filePath = @"wordsFile.txt";
            string mystery = NewGame(filePath);
            string mysteryKey = new string((char)95,mystery.Length);
            string wrong = "";

            string[] menu = new string[] {
                "You selected: ",
                "1. Guess a letter",
                "2. guess a series of letters",
                "3. New Game",
                "4. View words in the game",
                "5. Add a word to the game",
                "6. Remove a word from the game",
                "7. Remove all words from the game",
                "8. Exit the game"
            };

            while (playing)
            {
                if (WinCondition(mystery, mysteryKey))
                {
                    Console.Write(Environment.NewLine + Environment.NewLine+ $"You win! The word was {mysteryKey}! ");
                    playing = false;
                    break; 
                }

                Console.WriteLine("   Game Menu                            Administrator Menu" + Environment.NewLine);
                Console.WriteLine($"{menu[1]}                     {menu[5]}");
                Console.WriteLine($"{menu[2]}          {menu[6]}");
                Console.WriteLine($"{menu[3]}                           {menu[7]}");
                Console.WriteLine($"{menu[4]}             {menu[8]}" + Environment.NewLine);

                PrintProgress(mysteryKey, wrong);
                
                switch (NumberInput())
                {
                    case 1: // get a character as input and check against the answer
                        Console.WriteLine(menu[0] + menu[1] + Environment.NewLine);
                        char guess = GuessLetter(mystery);
                        if (mystery.Contains(guess.ToString()))
                            mysteryKey = AddCorrect(mystery, mysteryKey, guess);
                        else
                            wrong = AddIncorrect(wrong, guess);
                        break;
                    case 2: // get a string as input and check against the answer
                        Console.WriteLine(menu[0] + menu[2] + Environment.NewLine);
                        string guesses = GuessLetters();
                        foreach (var k in guesses)
                        {
                            if (mystery.Contains(k.ToString()))
                            {
                                mysteryKey = AddCorrect(mystery, mysteryKey, k);
                            } else {
                                wrong = AddIncorrect(wrong, k);
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine(menu[0] + menu[3] + Environment.NewLine);
                        mystery = NewGame(filePath);
                        break;
                    case 4:
                        Console.WriteLine(menu[0] + menu[4] + Environment.NewLine);
                        ViewWords(filePath);
                        break;
                    case 5:
                        Console.WriteLine(menu[0] + menu[5] + Environment.NewLine);
                        AddWords(filePath);
                        break;
                    case 6:
                        Console.WriteLine(menu[0] + menu[6] + Environment.NewLine);
                        RemoveWord(filePath);
                        break;
                    case 7:
                        Console.WriteLine(menu[0] + menu[7] + Environment.NewLine);
                        DeleteWords(filePath);
                        break;
                    case 8:
                        Console.WriteLine(menu[0] + menu[8] + Environment.NewLine);
                        playing = false;
                        break;
                    default:
                        Console.WriteLine("Invalid selection!" + Environment.NewLine);
                        break;
                }
            }

            Console.WriteLine("Thanks for playing!");
            Console.Read();
        }

        /***** Menu items *****/

        // 1. Guess a letter
        static char GuessLetter(string answers) {
            char guess = Console.ReadKey().KeyChar;

            if ( answers.Contains(guess.ToString()))
            {
                return guess;
            }
            Console.WriteLine();
            return guess;
        }
        // 2. guess a series of letters
        static string GuessLetters() => WordInput(5);
        // 3. start a new game
        static string NewGame(string filePath) => GetRandomWord(filePath);
        // 4. View words in the text file
        static int ViewWords(string filePath)
        {
            // just display all the words
            try
            {
                if (!File.Exists(filePath))
                {
                    CreateFile(filePath);
                }

                // no else required, I still want to print the one word if the file doesn't exist
                string[] words = ReadFile(filePath);
                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }
            }
            catch
            {
                Console.WriteLine("Whoops, something is broken! :(");
            }

            return 4;
        }
        // 5. add a word to the text file
        static int AddWords(string filePath)
        {
            // take text input
            string newWord = WordInput(20);
            try
            {
                if (!File.Exists(filePath))
                {
                    CreateFile(filePath);
                }
                else
                {
                    // send the filepath and the new word we just got to be appended
                    AppendFile(filePath, newWord);
                    Console.Write($" Added {newWord} to the game." + Environment.NewLine);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The Directory you suggested does not exist");
            }
            return 5;
        }
        // 6. Remove a word from a text file
        static int RemoveWord(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                { // if the file doesn't exist, just return and don't bother with this
                    return 6;
                }
                // get a word for deletion from input
                string oldWord = WordInput(20);
                // pull all the words into an array
                string[] words = ReadFile(filePath);
                // search for the word in the array
                foreach (string word in words)
                {
                    if (word == oldWord)
                    { // if we find the word, do the actual deletion
                        DeleteFileWord(filePath, oldWord, words);
                        return 6;
                    }
                }
                // word not found, print a message and return
                Console.WriteLine($"Sorry, {oldWord} was not found.");
            } // end try
            catch
            {
                Console.WriteLine("Whoops, something is broken! :(");
            }
            return 6;
        }
        // 7. delete the file containing the word bank
        static int DeleteWords(string filePath)
        {
            // delete the file containing all the words
            try
            {
                DeleteFile(filePath);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Sorry, there are no words yet.");
                return 7;
            }
            Console.WriteLine("Word list Deleted.");
            return 7;
        }

        /***** Game items *****/

        // helper method to grab a new random word from the file
        private static string GetRandomWord(string filePath)
        {
            string[] words = ReadFile(filePath);
            Random rnd = new Random();
            return words[rnd.Next(0, words.Length)];
        }

        private static bool WinCondition(string answers, string answerKey) => answers == answerKey ? true : false;

        private static string AddCorrect(string answers, string answerKey, char one)
        {
            char[] key = answerKey.ToCharArray();
            for (int i = 0; i < answers.Length; i++)
            {
                if (answers[i] == one)
                {
                    key[i] = one;
                }
            }
            return new string(key);
        }

        private static string AddIncorrect(string wrong, char one)
        {
            StringBuilder newWrong = new StringBuilder();
            newWrong.Append(wrong);
            newWrong.Append(one);
            return newWrong.ToString();
        }

        private static void PrintProgress(string answerKey, string wrong)
        {
            foreach (char i in answerKey)
            {
                Console.Write($"{i} ");
            }
            Console.Write("                    ");
            foreach (char j in wrong)
            {
                Console.Write($"{j} ");
            }
            Console.WriteLine(Environment.NewLine);
        }

        /***** private helper methods *****/
        /* The first 2 are integer and text input validation 
         * The rest are file I/O helper methods. The validation is done above so we should always get good data here */

        // this method is input validation to get an integer from the user
        private static int NumberInput()
        {
            bool badInput = true;
            Console.Write("Please enter an integer:  ");
            while (badInput)
            {
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
                }
            }
            return 0;
        }

        // this method is input validation to get text from the user
        // I looked at MSDN for some string validation help
        private static string WordInput(int limit)
        {
            bool badInput = true;
            while (badInput)
            {
                Console.Write("Please enter a word to add.  ");
                // I had to look up a couple regex checking strings online, but was not satisfied
                // TODO fix this: Regex regex1 = new Regex(@"^[^a-zA-Z0-9_@.-]*$");
                try
                {
                    string newWord = Console.ReadLine();
                    // I'm just checking for null, but ideally would have better validation (see above)
                    if (newWord == "null" || newWord == "")
                    {
                        Console.WriteLine("Bad input found!");
                        continue;
                    } else if(newWord.Length > limit)
                    {
                        Console.WriteLine("Word too long!!");
                        continue;
                    }
                    else
                    {
                        badInput = false;
                        return newWord;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please make another attempt to enter a string");
                }
                catch (Exception monkeybutt)
                {
                    Console.WriteLine($"You threw an {monkeybutt}! I'm not sure how you messed up a string!");
                }
                Console.WriteLine("Please make another attempt to enter a string");
            }
            return "default";
        }
        
        // create the file if it doesn't exist
        private static void CreateFile(string filePath)
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("fiddlesticks");
            }
            /* I can also use this, but I think my implementation is cleaner, byte is not the best
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] myText = new UTF8Encoding(true).GetBytes("fiddlesticks");
                fs.Write (myText, 0, myText.Length);
            }*/
        }

        // add a word to the file C in CRUD
        private static void AppendFile(string filePath, string newWord)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(newWord);
            }
        }

        // read the file R in CRUD
        private static string[] ReadFile(string filePath)
        {
            using (StreamReader sr = File.OpenText(filePath))
            {
                string[] words = File.ReadAllLines(filePath);
                return words;
            }
        }

        // delete the file D in CRUD
        private static void DeleteFile(string filePath) => File.Delete(filePath);

        // deletes a single file from the word list U in CRUD (kind of): this is the part where I really wish the client allowed a database
        private static void DeleteFileWord(string filePath, string oldWord, string[] words)
        {
            try
            {
                // delete the file
                DeleteFile(filePath);
                // recreate the file
                CreateFile(filePath);
                // add the words back minus the deleted word
                foreach (string word in words)
                {
                    if (word == "fiddlesticks" || word == oldWord)
                        continue;
                    else
                        AppendFile(filePath, word);
                }
            }
            catch
            {
                Console.WriteLine("Whoops, something is broken! :(");
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
        View words in the text file, DONE
        add a word to the text file,  DONE
        Remove words from a text file, DONE
        exit the game, DONE
        start a new game, DONE
        play the game DONE
    When playing a game, you should: 
        bring in all the words that exist in the text file, DONE
        and randomly select one of the words to output to the conole for the user to guess DONE
    You should have a record the letters they have attempted so far DONE
    If they guess a correct letter, display that letter in the console for them to refer back to when making guesses(i.e.C _ T S) DONE
    Errors should be handled through try/catch statements DONE
    You may use any shortcuts or 'helper' methods in this project.Do not create external classes to accomplish this task. DONE
 */