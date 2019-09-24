using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_of_Azure
{
    public class Game
    {
        public Wheel wheel = new Wheel();
        public PhraseBoard phraseBoard;
        public Player playerOne;

        private GameUI ui = new GameUI();
        private const string HardCodedPhrase = "abc";


        public Game()
        {
            phraseBoard = new PhraseBoard(HardCodedPhrase);
        }

        /// <summary>
        /// Starts the executions of the Wheel of Fortune console app game
        /// </summary>
        public void Start()
        {

            playerOne = new Player(ui.GetPlayerName());
            ui.DisplayWelcomeMessage(playerOne);

            while (!phraseBoard.IsGameOver())
            {
                ui.DisplayPlayerScore(playerOne);
                ui.DisplayBoard(phraseBoard);

                if (ui.GetUserChoice() == 1)
                {
                    Spin(playerOne);
                }
                else
                {
                    Solve(playerOne);
                }
            }

            ui.DisplayPlayerScore(playerOne);
            ui.DisplayBoard(phraseBoard);
            ui.DisplayWinner(playerOne);

            ui.GetExit();

        }

        /// <summary>
        /// Spins the wheel and asks the user to make a guess.
        /// </summary>
        private void Spin(Player playerOne)
        {
            int wheelAmount = wheel.WheelSpin();
            ui.DisplayWheelAmount(wheelAmount);

            if (wheelAmount > 0)
            {
                char spinGuessLetter = ui.GetSpinGuessLetter(phraseBoard, playerOne);

                //if the phrase contains the character, the program tells the user of this and tell them how much they won. 
                //It then goes back to the spin or solve function.
                int pointsEarned = phraseBoard.MakeGuess(wheelAmount, spinGuessLetter);
                if (pointsEarned > 0)
                {
                    ui.DisplaySpinGuessLetterSuccess(spinGuessLetter, pointsEarned);
                    playerOne.AddCurrentScore(pointsEarned);
                }
                //if it does not contain the character, it tells the user it has not have it and to guess again.
                //It then goes back to the spin or solve function.
                else
                {
                    ui.DisplaySpinGuessetterFailure(spinGuessLetter);
                }
            }
            else
            {
                ui.DisplayLoseTurn();
            }
        }

        /// <summary>
        /// Asks the user to solve the phrase.
        /// </summary>
        private void Solve(Player playerOne)
        {
            //Prompts the user to solve the phrase. If they get it right, they win. 
            //If not, then it goes back to the spin or solve function.
            string solveGuess = ui.GetSolveGuess();
            int pointsEarned = phraseBoard.MakeGuess(solveGuess);
            if (pointsEarned <= 0)
            {
                ui.DisplaySolveGuessFailure(solveGuess);
            }
            else
            {
                playerOne.AddCurrentScore(pointsEarned);
            }
        }


    }
}
