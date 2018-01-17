using NUnit.Framework;
using System;
using TexasHoldem.Hands;
using TexasHoldem.Utilities;

namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class FlushUnitTests: BaseUnitTests
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
        [TestCase("2C 4C TC 6C 8C", true, Description = "8 High Flush")]
        [TestCase("AD 3D 5D 2D 4D", true, Description = "Ace high Flush")]
        [TestCase("3D JD 5D 7D 9D", true, Description = "9 High Flush")]
        //Straight=============================================================
        [TestCase("AC KD QH JS TC", false, Description = "Ace high Straight")]
        [TestCase("2C 4D 6H 5S 3C", false, Description = "six high Straight")]
        //Three of a kind=======================================================
        [TestCase("2H 2C 2S 5S 9C ", false, Description = "three of a kind, twos")]
        [TestCase("QH QD QS 5C 9C ", false, Description = "three of a kind, queens")]
        //Two Pair===============================================================
        [TestCase("TC TD JC JD 9C ", false, Description = "Two Pair, Jacks over tens")]
        [TestCase("7C 9D 7S 9S TC ", false, Description = "Two Pair, nines over sevens")]
        //One Pair================================================================
        [TestCase("9C 9H 4C 5C 6C ", false, Description = "One Pair, nines")]
        [TestCase("KC 9H 4C 5C KD ", false, Description = "One Pair, kings")]
        //High Card===============================================================
        [TestCase("KH JH 8S 7D 4S", false, Description = "High Card, king")]
        [TestCase("QS JD 6C 5H 3C", false, Description = "High Card, Queen")]
        public void Test_Flush_CreateInstance(string strCards, bool isValid)
        {
            var cards = Utils.ParseCards(strCards);
            var flush = Flush.CreateInstance(cards);

            CreateInstanceHelper(flush, HandRanks.Flush, cards, isValid);
        }

        //[TestCase("2C 3C 4C 5C 9C ", "3D 6C", "2C 3C 4C 5C 6C", true, Description = "Description")]
        //public void Test_Flush_CreateInstance(string strCommunityCards, string strHoleCards, string strCards, bool isValid)
        //{
        //    Assert.Fail();
        //    var communityCards = Utils.ParseCards(strCommunityCards);
        //    var holeCards = Utils.ParseCards(strHoleCards);

        //    var pokername = StraightFlush.CreateInstance(communityCards, holeCards);

        //    if (isValid)
        //    {
        //        var straightFlushCards = Utils.ParseCards(strCards);

        //        Assert.IsNotNull(pokername);
        //        Assert.AreEqual(HandRanks.Flush, pokername.HandRank);
        //        CollectionAssert.AreEquivalent(straightFlushCards, pokername.Cards);
        //    }
        //    else
        //    {
        //        Assert.IsNull(pokername);
        //    }
        //}


        [TestCase("2D 4D 6D 8D TD", "2D 4D 6D 8D TD", true)]
        [TestCase("3D 5D 7D 9D JD", "2D 4D 6D 8D TD", false)]
        public void Test_Flush_EqualityOperators(string strInputA, string strInputB, bool areEqual)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);

            var flushOne = Flush.CreateInstance(cardsA);
            var flushTwo = Flush.CreateInstance(cardsB);

            EqualityOperatorsHelper(flushOne, flushTwo, areEqual);
        }


        [Test]
        public void Test_Flush_EqualityOperators_ForNull()
        {
            var cards = Utils.ParseCards("3D 5D 7D 9D JD");
            var flush = Flush.CreateInstance(cards);

            Assert.False(flush.Equals(null));

            Assert.True((Flush)null == (Flush)null);
            Assert.False((Flush)null == flush);
            Assert.False(flush == (Flush)null);

            Assert.False((Flush)null != (Flush)null);
            Assert.True((Flush)null != flush);
            Assert.True(flush != (Flush)null);
        }

        [TestCase("4H 6H 8H TH QH", "4D 6D 8D TD QD", 0)]
        [TestCase("4H 6H 8H TH QH", "6D 8D TD QD KD", -1)]
        [TestCase("6H 8H TH QH KH", "4D 6D 8D TD QD", 1)]
        public void Test_Flush_ComparableTests(string strInputA, string strInputB, int comp)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);

            var flushOne = Flush.CreateInstance(cardsA);
            var flushTwo = Flush.CreateInstance(cardsB);

            ComparableTestsHelper(flushOne, flushTwo, comp);
        }
    }
}
