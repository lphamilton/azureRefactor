using System;

namespace Wheel_of_Azure
{
    public class Wheel
    {
        public const int LoseATurn = 0;
        private Random random;
        public readonly int[] WheelOfAzure = {
                2500,
                600,
                700,
                600,
                650,
                500,
                700,
                0,
                0,
                0,
                0,
                0,
                600,
                550,
                500,
                600,
                650,
                700,
                800,
                500,
                800,
                500,
                650,
                500
        };

        public Wheel()
        {
            random = new Random();
        }

        public Wheel(int[] wheelAmounts) : this()
        {
            WheelOfAzure = wheelAmounts;
        }


        /// <summary>
        /// Randomly generates a number from the wheel
        /// </summary>
        /// <returns>The prize value of current spin</returns>
        public int WheelSpin()
        {
            int wheelPrizeDollarIndex = random.Next(WheelOfAzure.Length);
            int wheelPrizeDollarAmount = WheelOfAzure[wheelPrizeDollarIndex];
            return wheelPrizeDollarAmount;
        }
    }
 }
