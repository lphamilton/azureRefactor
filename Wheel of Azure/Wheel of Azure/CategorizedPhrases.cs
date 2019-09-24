using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_of_Azure
{
    class CategorizedPhrases
    {

        public int r;
        private string resCat;
        /*<summary>
        
             */
        //list of category strings
        private List<string> categories = new List<string>
        {
            "Holiday",
            "Movies",
            "Microsoft",
            "City"
        };
        /* public string GetCat()
         {   
             Random random = new Random();
             r = random.Next(categories.Count);
             resCat = categories[r];
             Console.WriteLine(r);
             Console.WriteLine(resCat);
             return resCat;

         }*/
        public string[] GetCatPhrase()
        {
            string filePath;
            Random random = new Random();
            r = random.Next(categories.Count);
            resCat = categories[r];
            //   Console.WriteLine(resCat+"!!!");
            switch (resCat)
            {
                case "Holiday":
                    filePath = Path.GetFullPath(@"..\..\CategorizedPhrases\holiday.txt");
                    break;
                case "Movies":
                    filePath = Path.GetFullPath(@"..\..\CategorizedPhrases\movies.txt");
                    break;
                case "Microsoft":
                    filePath = Path.GetFullPath(@"..\..\CategorizedPhrases\microsoft.txt");
                    break;
                case "City":
                    filePath = Path.GetFullPath(@"..\..\CategorizedPhrases\city.txt");
                    break;
                default:
                    filePath = Path.GetFullPath(@"..\..\CategorizedPhrases\all.txt");
                    break;

            }

            string readText = File.ReadAllText(filePath);
            string[] words = readText.Split(',');
            Random rand = new Random();
            int i = rand.Next(words.Length);
            string phrase = words[i];
            return new string[] { resCat, phrase };
        }


    }
}
