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
        /// Prompts plyaer to enter their name and returns the value entered.
        /// </summary>
        /// <returns>a string containing the player's name.</returns>
        internal string GetPlayerName()
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            return name;
        }

        /// <summary>
        /// Prompts the player to indicate whether they want to Spin(1) or Solve(2).
        /// </summary>
        /// <returns>1 if players wishes to spin, 2 if the player wishes to solve the puzzle.</returns>
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

            // Note: Console.ReadKey() won't work when redirecting the Input from a StringReader during unit testing
            //        Also, excluded from code coverage because it is not possible Console.ReadKey() doesn't work in the unit tests.

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
        /// Prompts the player to make a guess. In the event of bad input or a previously guessed letter the player is prompted again.
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
        /// Prompts the player to solve the phrase.
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
        internal void DisplayBoard(PhraseBoard board)
        {
            Console.WriteLine(board.GetBoardString());
        }

        /// <summary>
        /// Displays a congratulatory message to the winner.
        /// </summary>
        /// <param name="playerOne">The winning player.</param>
        internal void DisplayWinner(Player playerOne)
        {
            Console.WriteLine("You win!");
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
        /// Displays how many letters the player guessed correctly and the points earned for the correct guess.
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
        /// Displays a letter indicating that the attempt to solve the puzzle was unsuccessful.
        /// </summary>
        /// <param name="solveGuess"></param>
        internal void DisplaySolveGuessFailure(string solveGuess)
        {
            Console.WriteLine($"The phrase is not {solveGuess}. Please try again");
        }

        /// <summary>
        /// Repeatedly prompts the user to enter a single letter until the input is validated to be a single letter.
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

    }

}
