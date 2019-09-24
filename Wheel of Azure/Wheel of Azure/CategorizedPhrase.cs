using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Wheel_of_Azure
{
    public class CategorizedPhrase
    {
        public int r;
        
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
        public string GetCat()
        {
            Random random = new Random();
            r = random.Next(categories.Count);
            string resCat = categories[r];
            return resCat;

        }
        public string GetCatPhrase()
        {
            string filePath;
            switch (r)
            {
                case 0:
                     filePath = Path.GetFullPath(@"..\..\CategorizedPhrases\holiday.txt");
                    break;
                case 1:
                    filePath = Path.GetFullPath(@"..\..\CategorizedPhrases\movies.txt");
                    break;
                case 2:
                    filePath = Path.GetFullPath(@"..\..\CategorizedPhrases\microsoft.txt");
                    break;
                case 3:
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
            return phrase;
        }
        
    }
}
