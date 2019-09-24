using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Wheel_of_Azure;
using Xunit;
using System.Diagnostics;

namespace UnitTests
{
    public class TestGame
    {
        [Theory]
        [InlineData("abc", "1; Diane; bad;1;x;1;bad;b; 1;b;a; 2;abc;;", 100, 2)]
        [InlineData("blues clues","1; Diane; 1;b; 2;blues clues;;", 100, 1)]
        [InlineData("dog", "1; Diane; 2;cat; 2;dog;;", 100, 0)]
        [InlineData("cat", "1; Diane; 1; 2;cat;;", Wheel.LoseATurn, 0)]
        [InlineData("abc", "2; Diane; Wolf; 1;a; 1;x; 1;z; 2;abc;;", 100, 1)]
        [InlineData("abc", "a; 4; ; ; ; ; 2;x; 2;x; 2;x; 2;x; 2; abc;;", 100, 0)]
        public void TestGameStart_PlayerOneWins(string phrase, string consoleInput, int fixedWheelAmount, int lettersGuessedCorrectly)
        {
            // Redirect the console input to a string, ';' is used to separate line inputs
            var stringBuilder = new StringBuilder();
            foreach (var str in consoleInput.Split(';') )
            {
                stringBuilder.AppendLine(str.Trim());
            }
            var stringReader = new StringReader(stringBuilder.ToString());
            Console.SetIn(stringReader);

            // Instantiate a new Game object, overwrite the board and wheel with our test values
            var game = new Game()
            {
                phraseBoard = new PhraseBoard(phrase),
                wheel = new Wheel(new int[] { fixedWheelAmount })
            };

            // Start the game
            game.Start();

            // Examine the state of the player's score to see if the game ran correctly
            int expected = fixedWheelAmount*lettersGuessedCorrectly + PhraseBoard.PointsEarnedForSolving;
            int actual = game.players[0].TurnScore;
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void TestProgramMain_OnePlayer()
        {
            // This is just to get code coverage on our main program :)
            Program.Main(new string[] {"abc", "1\r\nDiane\r\n2\r\nabc\r\n\r\n" });
        }
    }
}
