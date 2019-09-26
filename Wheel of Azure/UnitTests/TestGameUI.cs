using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wheel_of_Azure;
using Xunit;


namespace UnitTests
{
    [ExcludeFromCodeCoverage]
    public class TestGameUI
    {
        [Theory(Skip="Skip")]
        [InlineData("1;Diane", new string[]{ "Diane" })]
        [InlineData("a;2;;Wolf", new string[] { "Player 1","Wolf" })]
        public void GetPlayerNamesTests(string consoleInput, string[] expected)
        {

            // Arrange
            var stringReader = new StringReader(FormatConsoleInput(consoleInput));
            Console.SetIn(stringReader);
            var sut = new GameUI();

            // Act
            var actual = sut.GetPlayerNames().ToArray();

            // Assert
            Assert.Equal(expected, actual);

        }

        [Theory(Skip="Skip")]
        [InlineData("1", 1)]
        [InlineData("a;2", 2)]
        public void GetUserChoice_Tests(string consoleInput, int expected)
        {

            // Arrange
            var stringReader = new StringReader(FormatConsoleInput(consoleInput));
            Console.SetIn(stringReader);
            var sut = new GameUI();

            // Act
            var actual = sut.GetUserChoice(); ;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(Skip="Skip")]
        [InlineData("c","abc", new char[] { },'c')]
        [InlineData("a;c", "abc", new char[] {}, 'c')]
        [InlineData("a;c", "abc", new char[] { 'a' }, 'c')]
        [InlineData("a", "abc", new char[] { 'c' }, 'a')]
        public void GetSpinGuessLetter_Tests(string consoleInput, string phraseString, char[] guesses, char expected)
        {

            // Arrange
            var stringReader = new StringReader(FormatConsoleInput(consoleInput));
            Console.SetIn(stringReader);
            var sut = new GameUI();
            var phraseBoard = new PhraseBoard(phraseString);
            var player = new Player("Player");
            foreach (char guess in guesses)
            {
                var points = phraseBoard.MakeGuess(1000, guess);
                player.AddCurrentScore(points);
            }

            // Act
            var actual = sut.GetSpinGuessLetter(phraseBoard, player);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory(Skip="Skip")]
        [InlineData("ABC", "abc")]
        [InlineData("Hello World", "hello world")]
        public void GetSolveGuess_Tests(string consoleInput, string expected)
        {

            // Arrange
            var stringReader = new StringReader(FormatConsoleInput(consoleInput));
            Console.SetIn(stringReader);
            var sut = new GameUI();

            // Act
            var actual = sut.GetSolveGuess(); ;

            // Assert
            Assert.Equal(expected, actual);
        }


        private string FormatConsoleInput(string consoleInput)
        {
            var stringBuilder = new StringBuilder();
            foreach (var str in consoleInput.Split(';'))
            {
                stringBuilder.AppendLine(str.Trim());
            }
            return stringBuilder.ToString();
        }
    }
}
