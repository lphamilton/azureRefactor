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
        [InlineData("abc", "Diane;bad;1;x;1;bad;b;1;b;a;2;abc;;", 100, 2)]
        [InlineData("blues clues","Diane;1;b;2;blues clues;;", 100, 1)]
        [InlineData("dog", "Diane;2;cat;2;dog;;", 100, 0)]
        [InlineData("cat", "Diane;1;2;cat;;", Wheel.LoseATurn, 0)]
        public void TestGameStart(string phrase, string consoleInput, int fixedWheelAmount, int lettersGuessedCorrectly)
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
            int actual = game.playerOne.TurnScore;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestProgramMain()
        {
            // This is just to get code coverage on our main program :)
            Program.Main(new string[] { "Diane\r\n2\r\nabc\r\n" });
        }
    }
}
