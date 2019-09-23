using System;
using System.IO;
using Wheel_of_Azure;
using Xunit;

namespace UnitTests
{
    public class TestBoard
    {

        [Theory]
        [InlineData("dog", new char[] { 'a', 'b', 'c' }, "***")]
        [InlineData("dog", new char[] { 'g' }, "**g")]
        [InlineData("dog", new char[] { 'o','g'}, "*og")]
        [InlineData("dog", new char[] { 'd', 'o', 'g' }, "dog")]
        public void TestGetBoardString(string phrase, char[] guesses, string expected)
        {
            var sut = new PhraseBoard(phrase);
            foreach (var guess in guesses) sut.MakeGuess(100, guess);
            string actual = sut.GetBoardString();
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void TestMakeGuess()
        {
            PhraseBoard b = new PhraseBoard("h i");
            Assert.Equal(100, b.MakeGuess(100, 'h'));
            Assert.Equal(0, b.MakeGuess(100, 'a'));

            b = new PhraseBoard("a aaa a");
            Assert.Equal(0, b.MakeGuess(100, 'b'));
            Assert.Equal(500, b.MakeGuess(100, 'a'));
        }

        [Fact]
        public void TestIsGameOver()
        {
            PhraseBoard b = new PhraseBoard("h i");
            Assert.False(b.IsGameOver());
            b.MakeGuess(500, 'h');
            Assert.False(b.IsGameOver());
            b.MakeGuess(500, 'i');
            Assert.True(b.IsGameOver());

            b = new PhraseBoard("h i");
            Assert.False(b.IsGameOver());
            b.MakeGuess("h i");
            Assert.True(b.IsGameOver());

            b = new PhraseBoard("abc");
            Assert.False(b.IsGameOver());
            b.MakeGuess("abc");
            Assert.True(b.IsGameOver());
        }

        [Fact]
        public void TestHasGuessed()
        {
            PhraseBoard b = new PhraseBoard("h i");
            Assert.False(b.HasGuessed('h'));
            b.MakeGuess(500, 'h');
            Assert.True(b.HasGuessed('h'));
        }
    }
}
