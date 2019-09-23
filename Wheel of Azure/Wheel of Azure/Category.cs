using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_of_Azure
{
    class Category
    {
        /*<summary>
        
             */
        //list of category strings
        private List<string> categories = new List<string>
        {
            "Holidays",
            "Movies",
            "Microsoft",
            "Beattles",
            "City"
        };
        public string GetCat()
        {
            Random random = new Random();
            int i = random.Next(categories.Count);
            string resCat = categories[i];
            return resCat;

        }

        
    }
}
