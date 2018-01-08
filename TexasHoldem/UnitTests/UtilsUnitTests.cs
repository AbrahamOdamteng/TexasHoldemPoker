using NUnit.Framework;
using System.Linq;
using TexasHoldem.Utilities;
using System;
using TexasHoldem.Deck;
namespace TexasHoldem.UnitTests
{
    [TestFixture]
    public class UtilsUnitTests
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
        public void Test_Utils_GenerateRandomCards()
        {
            var cardArray = Utils.GenerateRandomCards(10);
            Console.WriteLine( string.Join(" ", cardArray));
            
        }

        [TestCase("2C 3D 4H 5S 6C", "2C 3D 4H 5S 6C", Description = "Basic Test")]
        [TestCase("2C 3D 3H 4H 5S 6C 6S", "2C 3D 4H 5S 6C", Description = "Basic Test")]
        [TestCase("4H 5S 6C 7D 8H 9S TC", "6C 7D 8H 9S TC", Description = "High Straight")]
        [TestCase("4H 6C 8H TC 5S 7D 9S", "6C 7D 8H 9S TC", Description = "Test that card order is ignored")]
        [TestCase("2C 3D 4H 5S AC", "AC 2C 3D 4H 5S", Description = "Test Low Ace straight (5 cards)")]
        [TestCase("2C 3D 4H 5S AC 7C 8D", "AC 2C 3D 4H 5S", Description = "Test Low Ace straight (7 Cards)")]
        [TestCase("2C 3D 4H 5S AC AS AD", "AC 2C 3D 4H 5S",
                Description = "Test Low Ace straight (7 Cards) multiple aces PLEASE NOTE: Which Ace is returned is Irrelevant")]
        [TestCase("TC JD QH KS AC", "TC JD QH KS AC", Description = "Test High Ace straight")]
        public void Test_Utils_GetHighestStraight(string strInputHand, string strExpectedCards)
        {
            var cards = Utils.ParseCards(strInputHand);
            var highestStraight = Utils.GetHighestStraight(cards);

            var expectedCards = Utils.ParseCards(strExpectedCards);
            CollectionAssert.AreEquivalent(expectedCards, highestStraight);
        }

        [Test]
        public void Test_Utils_GetHighestStraight_TooManyCards()
        {
            var cards = Utils.ParseCards("4H 6C 8H TC 5S 7D 9S AS");
            Assert.That(() => Utils.GetHighestStraight(cards), Throws.ArgumentException.With.Message.EqualTo("The number of cards provided must be between 5 and 7 inclusive"));

            cards = Utils.ParseCards("5S 7D 9S AS");
            Assert.That(() => Utils.GetHighestStraight(cards), Throws.ArgumentException.With.Message.EqualTo("The number of cards provided must be between 5 and 7 inclusive"));
        }



        [TestCase("2C 3D 4H 5S 6C",ExpectedResult = true, Description = "Basic Straight")]
        [TestCase("AS 2C 3D 4H 5S", ExpectedResult = true, Description = "Low Ace Straight")]
        [TestCase("TD JC QD KC AS", ExpectedResult = true, Description = "Low Ace Straight")]
        [TestCase("2C 4D 6H 8S TC", ExpectedResult = false, Description = "Not a Straight")]
        [TestCase("2C 3D 4H AS AC", ExpectedResult = false, Description = "Not a Straight")]
        [TestCase("2C 4D 6H KS AC", ExpectedResult = false, Description = "Not a Straight With Ace")]
        public bool Test_Utils_IsStraight(string strCards)
        {
            var cards = Utils.ParseCards(strCards);
            return Utils.IsStraight(cards);
        }

        [Test]
        public void Test_Utils_IsStraight_Error()
        {
            var cards = Utils.ParseCards("2C 3D 4H 5S 6C 7S");
            Assert.That(() => Utils.IsStraight(cards), Throws.ArgumentException.With.Message.EqualTo("The number of cards provided must be 5"));
        }

        [TestCase("2C 3C 4C 5C 6C", "6C")]
        [TestCase("AC 2C 3C 4C 5C", "5C")]
        [TestCase("TD JD QD KD AD", "AD")]
        public void Test_Utils_GetHighestCard(string inputCards, string highCard)
        {
            var cards = Utils.ParseCards(inputCards);
            var hiCard = Utils.GetHighestCard(cards);
            Assert.AreEqual(highCard, hiCard.ToString());
        }
    }


}
