using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Wheel_of_Azure;
using Xunit;
using System.Diagnostics;
using Moq;

namespace UnitTests
{
    public class TestGame
    {
        [Theory]
        [InlineData("dog", "1; Diane; 2;cat; 2;dog;;", 100, 0)]        
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

            // setup the mock ICategorizedPhrases so the phrase is our test value
            var mock = new Mock<ICategorizedPhrases>();
            mock.Setup(x => x.category).Returns("Test");
            mock.Setup(x => x.GetPhrase("Test")).Returns(phrase);

            // Instantiate a new Game object, overwrite the wheel with our fixed amount
            var game = new Game(mock.Object)
            {
                wheel = new Wheel(new int[] { fixedWheelAmount })
            };

            // Start the game
            game.Start();

            // Examine the state of the player's score to see if the game ran correctly
            int expected = fixedWheelAmount*lettersGuessedCorrectly + PhraseBoard.PointsEarnedForSolving;
            int actual = game.players[0].TurnScore;
            Assert.Equal(expected, actual);
        }



        [Fact(Skip = "randomized phrases, will add test later") ]
        public void TestProgramMain_OnePlayer()
        {
            // This is just to get code coverage on our main program :)
            Program.Main(new string[] {"abc", "1\r\nDiane\r\n2\r\nabc\r\n\r\n" });
        }
    }
}
