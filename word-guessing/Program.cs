using System;
using System.IO;
using System.Text;

namespace word_guessing
{
    class Program
    {   
        //Main program: Establishes filepath and initiates gameloop
        static void Main(string[] args)
        {
            try
            {
                //Creates unique filepath so program can run on any machine
                string fileName = "word-file.txt";
                string path = Path.Combine(Environment.CurrentDirectory, @"Game\", fileName);

                //Begins main program loop
                GameLoop(path);

            }
            catch (DirectoryNotFoundException)
            {

                Console.WriteLine("The Directory you suggested does not exist");
            }

        }

        //Presents main menu and navigation. Calls relevent methods depending on user input
        static void GameLoop(string path)
        {
            Init(path);
            Console.Clear();
            Console.WriteLine("Welcome to Uncle Twisty's word guessing challenge!");
            Console.WriteLine("Please choose an option. Enter a number and press ENTER.");
            Console.WriteLine("");
            Console.WriteLine("1. PLAY");
            Console.WriteLine("2. View all words.");
            Console.WriteLine("3. Add a word.");
            Console.WriteLine("4. Remove a word.");
            Console.WriteLine("5. EXIT");
            Console.WriteLine("");
            string input = Console.ReadLine();
            //User selection handler
            if (input == "1")
            {
                //PLAY
                Console.Clear();
                GameHandler(path);
            }
            if (input == "2")
            {
                //View Word Bank
                Console.Clear();
                GetAllWords(path);
                Console.ReadLine();
                GameLoop(path);
            }
            if (input == "3")
            {
                //Add a new word to Word Bank
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("Enter a word you would like to add.");
                string addWord = Console.ReadLine().ToLower();
                AddWord(addWord, path);
                GetAllWords(path);
                GameLoop(path);
            }
            if (input == "4")
            {
                //Remove a word from the Word Bank
                Console.Clear();
                GetAllWords(path);
                Console.WriteLine("Enter a word you would like to remove.");
                string rmWord = Console.ReadLine().ToLower();
                RemoveWord(rmWord, path);
                Console.Clear();
                GetAllWords(path);
                Console.ReadLine();
                GameLoop(path);
            }
            if (input == "5")
            {
                //Exits the program and deletes word file
                Console.Clear();
                Exit(path);
            }
            Console.Read();
        }

        //Creates the game file and populates it with a default list of words.
        static void Init(string path)
        {
            string[] defaultWords = new String[10];
            defaultWords[0] = "pickle";
            defaultWords[1] = "jumanji";
            defaultWords[2] = "carrot";
            defaultWords[3] = "hippo";
            defaultWords[4] = "jedi";
            defaultWords[5] = "harpoon";
            defaultWords[6] = "stonehenge";
            defaultWords[7] = "whiskey";
            defaultWords[8] = "tango";
            defaultWords[9] = "foxtrot";

            //If file doesn't exist: Create it and Populate it with the default words.
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Byte[] myText = new UTF8Encoding(true).GetBytes(defaultWords[i]);
                        fs.Write(myText, 0, myText.Length);
                        byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                        fs.Write(newline, 0, newline.Length);
                    }
                }
            }
        }

        //Returns a random word from the game file for the player to guess.
        static string GetWord(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                //creates an array of words from the game file
                string[] words = File.ReadAllLines(path);
                Random rand = new Random();
                int roll = rand.Next(0, words.Length -1);
                return words[roll];
            }
        }

        //Returns all words from the game file and displays them to user.
        static string[] GetAllWords(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                //creates an array of words from the game file
                string[] words = File.ReadAllLines(path);

                int length = words.Length;

                //Prints all words to console for user
                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }
                return words;
            }
        }
    
        //Adds a word chosen by the user to the game file
        static void AddWord(string newWord, string path)
        {
            //Creates an array of all the words currently in the game file
            string[] all = GetAllWords(path);

            //Creates another array, larger by one index, to make room for the new word
            string[] newAll = new string[all.Length + 1];

            //Assigns new word to the first position of the larger array
            newAll[0] = newWord;

            //Assigns every word in the original array to the remaining indexes of the larger array
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i] == newWord)
                {
                    Console.WriteLine("The word you added is already on the list.");
                    return;
                }
                newAll[i + 1] = all[i];
            }
            //Writes the new, larger array to the game file
            using (FileStream fs = File.Create(path))
            {
                for (int i = 0; i < newAll.Length; i++)
                {
                    Byte[] myText = new UTF8Encoding(true).GetBytes(newAll[i]);
                    fs.Write(myText, 0, myText.Length);
                    byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                    fs.Write(newline, 0, newline.Length);
                }
            }

        }

        //Removes a word specified by the user from the game file
        static void RemoveWord(string remove, string path)
        { 
            //Original array read in from game file
            string[] all = GetAllWords(path);

            //Smaller array to write back to game file once specified word is removed
            string[] newAll = new string[all.Length - 1];

            //A switch to indicate when a deletion has occured inside the below loop
            bool removed = false;

            //Identifies the index at which the deletion should occur
            int removeAt = Array.IndexOf(all, remove);

            //Copies the old array into the new, minus the deletion
            for (int i = 0; i < all.Length; i++)
            {

                if (removed == true)
                {
                    newAll[i - 1] = all[i];
                }
                if (i != removeAt && removed == false)
                {
                    newAll[i] = all[i];
                }
                else
                {
                    removed = true;
                }

            }

            //Writes newly shortened array to game file
            using (FileStream fs = File.Create(path))
            {
                for (int i = 0; i < newAll.Length; i++)
                {
                    Byte[] myText = new UTF8Encoding(true).GetBytes(newAll[i]);
                    fs.Write(myText, 0, myText.Length);
                    byte[] newline = Encoding.ASCII.GetBytes(Environment.NewLine);
                    fs.Write(newline, 0, newline.Length);
                }
            }

        }

        //Handles the behavior of the actual gameplay
        static void GameHandler(string path)
        {
            string guessThis = GetWord(path); ;
            string check = "";
            string incorrect = "";
            int guesses = 0;

            //Holds the randomly generated word as an array
            char[] guessArr = guessThis.ToCharArray();

            //Holds the current state of successful guesses to display for the user
            char[] returnArr = new char[guessArr.Length];

            //Holds the users current guess
            char[] userGuessArr = new char[guessArr.Length];

            //Gameplay loop, Exits upon success
            while (true)
            {
                //Win Condition
                if (check == guessThis)
                {
                    Console.WriteLine("");
                    Console.WriteLine("WINNER WINNER CHICKEN DINNER!");
                    break;
                }
                //Prompts user and accepts a new guess
                Console.WriteLine("");
                Console.WriteLine($"GUESS A LETTER OR WORD.");
                userGuessArr = Console.ReadLine().ToCharArray();

                //Tracks number of guesses
                guesses++;
                Console.Clear();

                //Checks guess for matches and stores them
                for (int j = 0; j < guessArr.Length; j++)
                {
                    if (returnArr[j] != guessArr[j])
                    {
                        returnArr[j] = '-';
                    }
                    for (int k = 0; k < userGuessArr.Length; k++)
                    {
                        if (userGuessArr[k] == guessArr[j])
                        {
                            returnArr[j] = guessArr[j];
                            check = new string(returnArr);
                        }
                    }

                    Console.Write("");
                }

                //Adds incorrect guesses to a string to diplay to the user
                foreach (char letter in userGuessArr)
                {
                    if (Array.IndexOf(returnArr, letter) < 0)
                    {
                        incorrect += letter + " ";
                    }
                }
                //Displays success/failure feedback to user
                Console.WriteLine($"GUESSES: {guesses}");
                Console.WriteLine($"Guessed Wrong: {incorrect} ");
                Console.WriteLine("");
                Console.WriteLine(check);
            }
            Console.ReadLine();
            //returns to main menu
            GameLoop(path);

        }

        //Deletes game file and exits program
        static void Exit(string Path)
        {
            File.Delete(Path);
            Console.WriteLine("your file has been deleted");
            Console.Read();
            Environment.Exit(0);
        }

    }
}
