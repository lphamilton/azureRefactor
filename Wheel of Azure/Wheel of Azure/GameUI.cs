using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Diagnostics.CodeAnalysis;

namespace Wheel_of_Azure
{
    /// <summary>
    /// Contains the UI methods for getting input from the user and displaying data to the user.
    /// </summary>
    public class GameUI
    {
        /// <summary>
        /// Prompts player to enter their name and returns the value entered.
        /// </summary>
        /// <returns>a string containing the player's name.</returns>
        //internal string GetPlayerName()
        //{
        //    Console.Write("Please enter your name: ");
        //    string name = Console.ReadLine();
        //    return name;
        //}

        /// <summary>
        /// Prompts user to input the number of players and enter their names. Returns the list of player names.
        /// </summary>
        /// <returns>A string list containing the player names.</returns>
        public List<string> GetPlayerNames()
        {
            bool valid = false;
            int totalPlayers;
            List<string> names = new List<string>();

            do
            {
                Console.Write("How many players? ");
                string input = Console.ReadLine();
                valid = Int32.TryParse(input, out totalPlayers) && totalPlayers >= 1 ? Int32.TryParse(input, out totalPlayers) : false;
            } while (!valid);


            for (int i = 0; i < totalPlayers; i++)
            {
                Console.Write("Please enter player {0} name: ", i + 1);
                string name = Console.ReadLine();
                names.Add((name == "") ? "Player" + i : name);
            }
            return names;
        }


        /// <summary>
        /// Prompts the player to indicate whether they want to Spin(1) or Solve(2).
        /// </summary>
        /// <returns>1 if players wishes to spin, 2 if the player wishes to solve.</returns>
        internal int GetUserChoice()
        {
            bool isNumeric = false;
            int userChoice;
            do
            {
                Console.WriteLine("Enter 1 to Spin, or 2 to Solve");
                string choice = Console.ReadLine();
                isNumeric = int.TryParse(choice, out userChoice);
            } while (!isNumeric || (userChoice != 1 && userChoice != 2));

            return userChoice;
        }
        /// <summary>
        /// Prompts user to press a key so that the console remains displayed until the user wishes to exit.
        /// </summary>
        [ExcludeFromCodeCoverage]
        internal void GetExit()
        {
            Console.Write("\nPress any key to exit: ");

            // Note: Console.ReadKey() won't work when redirecting the Input from a StringReader during unit testing.
            //       Therefore excluded from code coverage because Console.ReadKey() is untestable.

            // When this code is being executed during unit testing:
            //      Console.IsOutpuRedirected = true 
            //      Console.IsInputRedirected = false
            //
            // When executed from the command line:
            //      Console.IsOutpuRedirected = false
            //      Console.IsInputRedirected = false

            if (Console.IsOutputRedirected)
                Console.ReadLine(); // during testing
            else
                Console.ReadKey();  // command line
        }

        /// <summary>
        /// Prompts the player to guess a letter and returns the response as a char. If the input is invalid or a previously guessed letter, the player is prompted again.
        /// </summary>
        /// <param name="phraseBoard">The current PhraseBoard being guessed.</param>
        /// <returns>The letter the player has guessed.</returns>
        internal char GetSpinGuessLetter(PhraseBoard phraseBoard)
        {
            Console.Write("Guess a letter: ");
            string spinGuess = Console.ReadLine().ToLower();

            char spinGuessLetter = SingleLettersOnly(spinGuess); ;

            //If the character has already been guessed, then it will prompt the user to type in one that has not.
            while (phraseBoard.HasGuessed(spinGuessLetter))
            {
                Console.Write($"{spinGuessLetter} has already been guessed. Guess again: ");
                spinGuess = Console.ReadLine().ToLower();

                spinGuessLetter = SingleLettersOnly(spinGuess); ;
            }
            return spinGuessLetter;

        }

        /// <summary>
        /// Prompts the player to solve the phrase and returns the response as a string.
        /// </summary>
        /// <returns>The player's guess for the phrase.</returns>
        internal string GetSolveGuess()
        {
            Console.Write("Solve the phrase: ");
            string solveGuess = Console.ReadLine().ToLower();
            return solveGuess;
        }

        /// <summary>
        /// Displays a friendly greeting to the player.
        /// </summary>
        /// <param name="playerOne">The player.</param>
        internal void DisplayWelcomeMessage(Player playerOne)
        {
            Console.WriteLine($"Welcome to Wheel of Azure {playerOne.Name}!");
        }

        /// <summary>
        /// Displays a message indicating the current player's turn.
        /// </summary>
        /// <param name="playerOne"></param>
        internal void DisplayPlayerTurn(Player playerOne)
        {
            Console.WriteLine($"\nPlayer {playerOne.Name}, it's now your turn!!!\n");
        }

        /// <summary>
        ///  Displays the player's Turn Score.
        /// </summary>
        /// <param name="playerOne">The current player.</param>
        internal void DisplayPlayerScore(Player playerOne)
        {
            Console.WriteLine($"Total Score: ${playerOne.TurnScore} ");
        }

        /// <summary>
        ///  Displays the phrase board. Unsolved letters are displayed as asterisks.
        /// </summary>
        /// <param name="board">The PhraseBoard to be displayed.</param>
        [ExcludeFromCodeCoverage]
        internal void DisplayBoardSimple(PhraseBoard board)
        {
            Console.WriteLine("\n" + board.GetBoardString() + "\n");
        }

        internal void DisplayBoard(PhraseBoard board)
        {
            var chars = board.GetBoardString();

            string topAndBottom = "+" + String.Join("+", chars.Select(c =>(c==' ')? "   ":"---").ToArray()) + "+";
            string middle = "|" + String.Join("|", chars.Select(c => (c=='*') ? "   ":" " + c + " ").ToArray()) + "|";
            Console.WriteLine();
            Console.WriteLine(topAndBottom);
            Console.WriteLine(middle);
            Console.WriteLine(topAndBottom);
            Console.WriteLine();
        }


        /// <summary>
        /// Displays a congratulatory message to the winner.
        /// </summary>
        /// <param name="playerOne">The winning player.</param>
        internal void DisplayWinner(List<Player> players, int roundWinner)
        {
            
            if (players.Count == 1)
                Console.WriteLine($"\nYou win!");
            else
                Console.WriteLine($"\nPlayer {players[roundWinner].Name} wins the round and ${players[roundWinner].TurnScore}!!!");
        }


        /// <summary>
        /// Displays the amount resulting from spinning the wheel.
        /// </summary>
        /// <param name="wheelAmount"></param>
        internal void DisplayWheelAmount(int wheelAmount)
        {
            Console.WriteLine($"The wheel landed at ${wheelAmount}");
        }

        /// <summary>
        /// Displays message indicating the player guessed correctly and the points earned for the correct guess.
        /// </summary>
        /// <param name="spinGuessLetter">The letter the player guessed.</param>
        /// <param name="pointsEarned">The points earned for guessing correctly.</param>
        internal void DisplaySpinGuessLetterSuccess(char spinGuessLetter, int pointsEarned)
        {
            Console.WriteLine($"The phrase indeed includes {spinGuessLetter}. You won ${pointsEarned}!");
        }

        /// <summary>
        /// Displays a message indicating that the player's guess was unsuccessful.
        /// </summary>
        /// <param name="spinGuessLetter">The letter the player guessed.</param>
        internal void DisplaySpinGuessetterFailure(char spinGuessLetter)
        {
            Console.WriteLine($"The phrase does not include {spinGuessLetter}. Try again next turn!");
        }

        /// <summary>
        /// Displays a message indicating that the player's spin landed on "Lose A Turn".
        /// </summary>
        internal void DisplayLoseTurn()
        {
            Console.WriteLine("Sorry, you lose a turn");
        }

        /// <summary>
        /// Displays a message indicating that the attempt to solve the puzzle was unsuccessful.
        /// </summary>
        /// <param name="solveGuess"></param>
        internal void DisplaySolveGuessFailure(string solveGuess)
        {
            Console.WriteLine($"The phrase is not {solveGuess}. Please try again");
        }

        /// <summary>
        /// Displays a message announcing that the player's attempt to solve the puzzle was successful.
        /// </summary>
        /// <param name="solveGuess"></param>
        internal void DisplaySolveGuessSuccess(string solveGuess)
        {
            Console.WriteLine($"You are correct! The answer is {solveGuess}.");
        }

        /// <summary>
        /// Repeatedly prompts the user to enter a single letter until the input is valid. Returns the single letter as a char.
        /// </summary>
        /// <param name="spinGuess"></param>
        /// <returns>A char value for the single letter entered by the player.</returns>
        public static char SingleLettersOnly(string spinGuess)
        {
            while (spinGuess.Length != 1 || !Regex.IsMatch(spinGuess, @"^[a-z]+$"))
            {
                Console.Write("Type in a single character only, please: ");
                spinGuess = Console.ReadLine().ToLower();
            }
            return spinGuess[0];
        }
        /// <summary>
        /// Sets the text color to a different color for each player
        /// </summary>
        /// <param name="playerNumber"></param>
        public void SetPlayerTextColor(int playerNumber)
        {
            switch (playerNumber % 4)
            {
                case 0: Console.ForegroundColor = ConsoleColor.Cyan; break;
                case 1: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 2: Console.ForegroundColor = ConsoleColor.Green; break;
                default: Console.ForegroundColor = ConsoleColor.Magenta; break;
            }
        }
        /// <summary>
        /// Resets the text color to white
        /// </summary>
        public void ResetTextColorToWhite()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

    }

}
