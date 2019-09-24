using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Wheel_of_Azure
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var game = new Game();

            if (args.Length > 0)
            {
                game.phraseBoard = new PhraseBoard(args[0]);
                StringReader reader = new StringReader(args[1]);
                Console.SetIn(reader);
            }

            game.Start();

        }

    }
}
