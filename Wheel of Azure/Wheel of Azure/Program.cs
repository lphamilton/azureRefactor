using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Wheel_of_Azure
{
    public class Program
    {

        public static void Main(string[] args)
        {

            if (args.Length > 0)
            {
                StringReader reader = new StringReader(args[0]);
                Console.SetIn(reader);
            }

            var game = new Game();
            game.Start();

        }

    }
}
