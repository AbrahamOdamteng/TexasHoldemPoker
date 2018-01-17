using NUnit.Framework;
using System;
using TexasHoldem.Hands;
using TexasHoldem.Utilities;

namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class TwoPairsUnitTests: BaseUnitTests
    {
        //Royal Flush==========================================================
        [TestCase("TC JC QC KC AC", false, Description = "Royal Flush Clubs")]
        [TestCase("TD QD AD JD KD", false, Description = "Royal Flush Diamonds")]
        [TestCase("AH KH QH JH TH", false, Description = "Royal Flush Hearts")]
        [TestCase("TS JS QS KS AS", false, Description = "Royal Flush Spades")]
        //Straight Flush=======================================================
        [TestCase("6C 3C 2C 4C 5C", false, Description = "Straight Flush 6 high")]
        [TestCase("9H TH KH JH QH", false, Description = "Straight Flush King high")]
        //Four Of a Kind=======================================================
        [TestCase("4C 4D 4H 4S 5C", false, Description = "Four of a Kind, fours")]
        [TestCase("AH AD 7D AS AC", false, Description = "Four of a Kind, aces")]
        //Full House===========================================================
        [TestCase("3C 3D 3S 6H 6C", false, Description = "Full house, Three over sixes")]
        [TestCase("KC TS KD TC TD", false, Description = "Full house, Ten over Kings")]
        //Flush================================================================
        [TestCase("2C 4C TC 6C 8C", false, Description = "8 High Flush")]
        [TestCase("AD 3D 5D 2D 4D", false, Description = "Ace high Flush")]
        [TestCase("3D JD 5D 7D 9D", false, Description = "9 High Flush")]
        //Straight=============================================================
        [TestCase("AC KD QH JS TC", false, Description = "Ace high Straight")]
        [TestCase("2C 4D 6H 5S 3C", false, Description = "six high Straight")]
        //Three of a kind=======================================================
        [TestCase("2H 2C 2S 5S 9C ", false, Description = "three of a kind, twos")]
        [TestCase("QH QD QS 5C 9C ", false, Description = "three of a kind, queens")]
        //Two Pair===============================================================
        [TestCase("TC TD JC JD 9C ", true, Description = "Two Pair, Jacks over tens")]
        [TestCase("7C 9D 7S 9S TC ", true, Description = "Two Pair, nines over sevens")]
        //One Pair================================================================
        [TestCase("9C 9H 4C 5C 6C ", false, Description = "One Pair, nines")]
        [TestCase("KC 9H 4C 5C KD ", false, Description = "One Pair, kings")]
        //High Card===============================================================
        [TestCase("KH JH 8S 7D 4S", false, Description = "High Card, king")]
        [TestCase("QS JD 6C 5H 3C", false, Description = "High Card, Queen")]
        public void Test_TwoPairs_CreateInstance(string strCards, bool isValid)
        {
            var cards = Utils.ParseCards(strCards);
            var twoPairs = TwoPairs.CreateInstance(cards);

            CreateInstanceHelper(twoPairs, HandRanks.TwoPairs, cards, isValid);
        }

        [TestCase("8D 8C 5C 5H 2S", "8D 8C 5C 5H 2S", true)]
        [TestCase("QS QD 5H 5C KS", "AS AD 3S 3H JC", false)]
        public void Test_TwoPairs_EqualityOperators(string strInputA, string strInputB, bool areEqual)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);

            var twoPairOne = TwoPairs.CreateInstance(cardsA);
            var twoPairTwo = TwoPairs.CreateInstance(cardsB);

            EqualityOperatorsHelper(twoPairOne, twoPairTwo, areEqual);
        }

        [Test]
        public void Test_TwoPairs_EqualityOperators_ForNull()
        {
            var cards = Utils.ParseCards("4H 4D 6H 6S 8H");
            var twoPairs = TwoPairs.CreateInstance(cards);

            Assert.False(twoPairs.Equals(null));

            Assert.True((TwoPairs)null == (TwoPairs)null);
            Assert.False((TwoPairs)null == twoPairs);
            Assert.False(twoPairs == (TwoPairs)null);

            Assert.False((TwoPairs)null != (TwoPairs)null);
            Assert.True((TwoPairs)null != twoPairs);
            Assert.True(twoPairs != (TwoPairs)null);
        }


        [TestCase("8D 8C 5C 5H 3S", "8D 8C 5C 5H 3S", 0)]
        //-------------------------------
        [TestCase("7C 7D 5H 5S 3C", "8D 8H 5S 5C 3D", -1, Description = "Compare High Pair")]
        [TestCase("8D 8H 4S 4C 3D", "8H 8S 5C 5D 3H", -1, Description = "Compare Low Pair")]
        [TestCase("8H 8S 5C 5D 2H", "8S 8C 5D 5H 3S", -1, Description = "Compare kicker")]
        //-------------------------------
        [TestCase("7C 7D 5H 5S 4C", "6D 6H 5S 5C 4D", 1, Description = "Compare High Pair")]
        [TestCase("7D 7H 6S 6C 4D", "7H 7S 5C 5D 4H", 1, Description = "Compare Low Pair")]
        [TestCase("7H 7S 5C 5D 4H", "7S 7C 5D 5H 3S", 1, Description = "Compare kicker")]
        public void Test_TwoPairs_ComparableTests(string strInputA, string strInputB, int comp)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);

            var twoPairsOne = TwoPairs.CreateInstance(cardsA);
            var twoPairsTwo = TwoPairs.CreateInstance(cardsB);

            ComparableTestsHelper(twoPairsOne, twoPairsTwo, comp);
        }
    }
}
