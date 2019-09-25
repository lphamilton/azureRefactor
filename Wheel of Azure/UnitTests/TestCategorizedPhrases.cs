using System;
using System.Collections.Generic;
using System.Linq;
using Wheel_of_Azure;
using Xunit;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class TestCategorizedPhrases
    {
        [Fact]
        public void TestRandomizeCat()
        {
            CategorizedPhrases rndCat = new CategorizedPhrases();
            HashSet<string> allcategories = new HashSet<string>();
            foreach (string cat in rndCat.categories)
            {
                allcategories.Add(cat);
            }

            for (int i = 0; i < 4; i++)
            {
                bool res = allcategories.Contains(rndCat.category);
                Assert.True(res);
            }
        }
    }
}
