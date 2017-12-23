using NUnit.Framework;
using System.Linq;
using TexasHoldem.Utilities;
using System;

namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class UtilsUnitTests
    {
        [TestCase("", 0, ExpectedResult = "")]
        [TestCase("AS", 1, ExpectedResult = "AS")]
        [TestCase("3S       6C", 2, ExpectedResult = "3S,6C")]
        [TestCase("2C 4H 3D 5S", 4, ExpectedResult = "2C,3D,4H,5S")]
        [TestCase("6C 8H 7D 9S", 4, ExpectedResult = "6C,7D,8H,9S")]
        [TestCase("TC JH QD KS AS", 5, ExpectedResult = "AS,JH,KS,QD,TC")]
        [TestCase("AC KD QH JS TC 9D 8H 7S 6C 5D 4H 3S 2C", 13, ExpectedResult = "2C,3S,4H,5D,6C,7S,8H,9D,AC,JS,KD,QH,TC")]
        public string Test_Utils_ParseCards(string cards, int cardCount)
        {
            var cardArray = Utils.ParseCards(cards);
            Assert.AreEqual(cardCount, cardArray.Count());
            return string.Join(",", cardArray.Select(c => c.ToString()).OrderBy(s => s));
        }

        [Test]
        public void Test_GenerateRandomCards()
        {
            var cardArray = Utils.GenerateRandomCards(10);
            Console.WriteLine( string.Join(" ", cardArray));
            
        }

    }


}
