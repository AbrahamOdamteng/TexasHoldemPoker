using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TexasHoldem.Utilities;
using TexasHoldem.Hands;

namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class StraightFlushUnitTests: BaseUnitTests
    {

        //Royal Flush==========================================================
        [TestCase("TC JC QC KC AC", false, Description = "Royal Flush Clubs")]
        [TestCase("TD QD AD JD KD", false, Description = "Royal Flush Diamonds")]
        [TestCase("AH KH QH JH TH", false, Description = "Royal Flush Hearts")]
        [TestCase("TS JS QS KS AS", false, Description = "Royal Flush Spades")]
        //Straight Flush=======================================================
        [TestCase("6C 3C 2C 4C 5C", true, Description = "Straight Flush 6 high")]
        [TestCase("9H TH KH JH QH", true, Description = "Straight Flush King high")]
        //Four Of a Kind=======================================================
        [TestCase("4C 4D 4H 4S 5C", false, Description = "Four of a Kind, fours")]
        [TestCase("AH AD 7D AS AC", false, Description = "Four of a Kind, aces")]
        //Full House===========================================================
        [TestCase("3C 3D 3S 6H 6C", false, Description = "Full house, Three over sixes")]
        [TestCase("KC TS KD TC TD", false, Description = "Full house, Ten over Kings")]
        //Flush================================================================
        [TestCase("2C 4C TC 6C 8C", false, Description = "8 High Flush")]
        [TestCase("AD 3D 5D 2D 4D", true, Description = "Ace high Flush")]
        [TestCase("3D JD 5D 7D 9D", false, Description = "9 High Flush")]
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
        public void Test_StraightFlush_CreateInstance(string strCards, bool isValid)
        {
            var cards = Utils.ParseCards(strCards);
            var straightFlush = StraightFlush.CreateInstance(cards);

            CreateInstanceHelper(straightFlush, HandRanks.StraightFlush, cards, isValid);
        }

        //[TestCase("2C 6C 4C 8C TC ", "3D 6D", "", false, Description = "No Straigh-Flush")]
        //[TestCase("TD JD QD KD 9H ", "3D AD", "", false, Description = "RoyalFlush")]
        //[TestCase("3H 4H 5H 6H 7H ", "3D AD", "", false, Description = "Community Card Straight-Flush")]
        //[TestCase("2C 3C 4C 5C 9C ", "3D 6C", "2C 3C 4C 5C 6C", true, Description = "Straigh-Flush")]
        //[TestCase("2C 3C 4C 5C 9C ", "3D AC", "AC 2C 3C 4C 5C", true, Description = "Straigh-Flush Low-Ace")]
        //[TestCase("AC 2C 3C 4C 5C", "6C KC", "6C 5C 4C 3C 2C", true, Description = "Community Cards Low-Ace Straight-Flush 1")]
        //[TestCase("AC 2C 3C 4C 5C", "6C 7C", "7C 6C 5C 4C 3C", true, Description = "Community Cards Low-Ace Straight-Flush 2")]
        //[TestCase("4C 5C 6C 7C 8C", "2C 3C", "3C 4C 5C 6C 7C", true, Description = "Community Cards Mid-Range Straight-Flush 1")]
        //[TestCase("4C 5C 6C 7C 8C", "3C KC", "3C 4C 5C 6C 7C", true, Description = "Community Cards Mid-Range Straight-Flush 2")]
        //[TestCase("4C 5C 6C 7C 8C", "9C KC", "5C 6C 7C 8C 9C", true, Description = "Community Cards Mid-Range Straight-Flush 3")]
        //[TestCase("4C 5C 6C 7C 8C", "9C TC", "6C 7C 8C 9C TC", true, Description = "Community Cards Mid-Range Straight-Flush 4")]
        //public void Test_StraightFlush_CreateInstance(string strCommunityCards, string strHoleCards, string strStraightFlushCards, bool isValid)
        //{
        //    Assert.Fail();
        //    var communityCards = Utils.ParseCards(strCommunityCards);
        //    var holeCards = Utils.ParseCards(strHoleCards);

        //    var straightFlush = StraightFlush.CreateInstance(communityCards, holeCards);

        //    if (isValid)
        //    {
        //        var straightFlushCards = Utils.ParseCards(strStraightFlushCards);

        //        Assert.IsNotNull(straightFlush);
        //        Assert.AreEqual(HandRanks.StraightFlush, straightFlush.HandRank);
        //        CollectionAssert.AreEquivalent(straightFlushCards, straightFlush.Cards);
        //    }
        //    else
        //    {
        //        Assert.IsNull(straightFlush);
        //    }
        //}



        [TestCase("2D 3D 4D 5D 6D", "2D 3D 4D 5D 6D", true)]
        [TestCase("2D 3D 4D 5D 6D", "3D 4D 5D 6D 7D", false)]
        public void Test_StraightFlush_EqualityOperators(string strStraightFlushOne, string strStraightFlushTwo, bool areEqual)
        {
            var cardsA = Utils.ParseCards(strStraightFlushOne);
            var cardsB = Utils.ParseCards(strStraightFlushTwo);

            var straightFlushOne = StraightFlush.CreateInstance(cardsA);
            var straightFlushTwo = StraightFlush.CreateInstance(cardsB);

            EqualityOperatorsHelper(straightFlushOne, straightFlushTwo, areEqual);
        }

        [Test]
        public void Test_StraightFlush_EqualityOperators_ForNull()
        {

            var cards = Utils.ParseCards("4H 5H 6H 7H 8H");
            var straintFlush = StraightFlush.CreateInstance(cards);

            Assert.False(straintFlush.Equals(null));

            Assert.True((StraightFlush)null == (StraightFlush)null);
            Assert.False((StraightFlush)null == straintFlush);
            Assert.False(straintFlush == (StraightFlush)null);

            Assert.False((StraightFlush)null != (StraightFlush)null);
            Assert.True((StraightFlush)null != straintFlush);
            Assert.True(straintFlush != (StraightFlush)null);
        }

        [TestCase("2H 3H 4H 5H 6H", "2D 3D 4D 5D 6D", 0)]
        [TestCase("AH 2H 3H 4H 5H", "2D 3D 4D 5D 6D", -1)]
        [TestCase("2D 3D 4D 5D 6D", "AH 2H 3H 4H 5H", 1)]
        public void Test_StraightFlush_ComparableTests(string strInputA, string strInputB, int comp)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);
            var straightFlushOne = StraightFlush.CreateInstance(cardsA);
            var straightFlushTwo = StraightFlush.CreateInstance(cardsB);

            Assert.AreEqual(comp, straightFlushOne.CompareTo(straightFlushTwo));

            if (comp == 0)
            {
                Assert.IsTrue(straightFlushOne >= straightFlushTwo);
                Assert.IsTrue(straightFlushOne <= straightFlushTwo);
                Assert.IsFalse(straightFlushOne > straightFlushTwo);
                Assert.IsFalse(straightFlushOne < straightFlushTwo);
            }
            else if (comp == 1)
            {
                Assert.IsTrue(straightFlushOne >= straightFlushTwo);
                Assert.IsFalse(straightFlushOne <= straightFlushTwo);
                Assert.IsTrue(straightFlushOne > straightFlushTwo);
                Assert.IsFalse(straightFlushOne < straightFlushTwo);
            }
            else if (comp == -1)
            {
                Assert.IsFalse(straightFlushOne >= straightFlushTwo);
                Assert.IsTrue(straightFlushOne <= straightFlushTwo);
                Assert.IsFalse(straightFlushOne > straightFlushTwo);
                Assert.IsTrue(straightFlushOne < straightFlushTwo);
            }
            else
            {
                throw new ArgumentException("the value of comp can only be -1,0,1");
            }
        }



    }
}
