using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TexasHoldem.Hands;
using TexasHoldem.Utilities;
using TexasHoldem.Interfaces;
using TexasHoldem.Deck;
using Moq;
namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class RoyalFlushUnitTests: BaseUnitTests
    {
        #region Constructor tests
        //Royal Flush==========================================================
        [TestCase("TC JC QC KC AC", true, Description = "Royal Flush Clubs")]
        [TestCase("TD QD AD JD KD", true, Description = "Royal Flush Diamonds")]
        [TestCase("AH KH QH JH TH", true, Description = "Royal Flush Hearts")]
        [TestCase("TS JS QS KS AS", true, Description = "Royal Flush Spades")]
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
        public void Test_RoyalFlush_CreateInstance(string strCards, bool isValid)
        {
            var cards = Utils.ParseCards(strCards);
            var royalFlush = RoyalFlush.CreateInstance(cards);

            CreateInstanceHelper(royalFlush, HandRanks.RoyalFlush, cards, isValid);
        }

        //[TestCase("AS 6H 5C JD TD", "KS 2H", false, Description = "Test HighCard ")]
        //[TestCase("3C 2D 5C JD TD", "KS KH", false, Description = "Test OnePair")]
        //[TestCase("5C 6D 3C AD AC", "KS KH", false, Description = "Test TwoPair")]
        //[TestCase("5C 7H AC AD AS", "KS 4H", false, Description = "Test ThreeOfAKind")]
        //[TestCase("KH 2C 5C 6D 8S", "9C 7H", false, Description = "Test Straight")]
        //[TestCase("6D 6H 6C TC QC", "2C 4C", false, Description = "Test Flush")]
        //[TestCase("3S 4S 2D TC TS", "2S 2H", false, Description = "Test FullHouse")]
        //[TestCase("AS 4S 2D 2C TS", "2S 2H", false, Description = "Test FoutOfAKind")]
        //[TestCase("9C 3H 4C 6C 8C", "7C 5C", false, Description = "Test StraightFlush")]
        //[TestCase("TD JD QD KD AD", "2H 7C", false, Description = "Test RoyalFlush in community")]
        //[TestCase("TD JD QD KD 9S", "AD 7C", true, Description = "Test RoyalFlush")]
        //[TestCase("TD JD KD", "AD QD", true, Description = "Test RoyalFlush")]
        //public void Test_RoyalFlush_CreateInstance(string communityCardsStr, string holeCardsStr, bool isValid)
        //{
        //    Assert.Fail();
        //    var communityCards = Utils.ParseCards(communityCardsStr);
        //    var holeCards = Utils.ParseCards(holeCardsStr);

        //    var royalFlush = RoyalFlush.CreateInstance(communityCards, holeCards);
        //    if (isValid)
        //    {
        //        Assert.NotNull(royalFlush);
        //        Assert.AreEqual(HandRanks.RoyalFlush, royalFlush.HandRank);
        //    }
        //    else
        //    {
        //        Assert.IsNull(royalFlush);
        //    }
        //}




        [Test]
        public void Test_RoyalFlush_CreateInstance_Exception()
        {
            Assert.Fail();
        }

        [Test]
        public void Test_RoyalFlush_ToString()
        {
            Assert.Fail();
        }
        #endregion

        #region Test Equality Operators

        [TestCase("TC JC QC KC AC", "TC JC QC KC AC", true)]
        [TestCase("TC JC QC KC AC", "TS JS QS KS AS", false)]
        public void Test_RoyalFlush_EqualityOperators(string strInputA, string strInputB, bool areEqual)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);
            var royalFlushOne = RoyalFlush.CreateInstance(cardsA);
            var royalFlushTwo = RoyalFlush.CreateInstance(cardsB);

            Assert.NotNull(royalFlushOne);
            Assert.NotNull(royalFlushTwo);

            Assert.AreEqual(areEqual, royalFlushOne.Equals(royalFlushTwo));
            Assert.AreEqual(areEqual, royalFlushOne.Equals((object)royalFlushTwo));
            Assert.AreEqual(areEqual, royalFlushOne == royalFlushTwo);
            Assert.AreEqual(!areEqual, royalFlushOne != royalFlushTwo);
        }

        [Test]
        public void Test_RoyalFlush_EqualityOperators_ForNull()
        {
            var cards = Utils.ParseCards("TH JH KH QH AH");
            var royalflush = RoyalFlush.CreateInstance(cards);

            Assert.False(royalflush.Equals(null));

            Assert.True((RoyalFlush)null == (RoyalFlush)null);
            Assert.False((RoyalFlush)null == royalflush);
            Assert.False(royalflush == (RoyalFlush)null);

            Assert.False((RoyalFlush)null != (RoyalFlush)null);
            Assert.True((RoyalFlush)null != royalflush);
            Assert.True(royalflush != (RoyalFlush)null);
        }

        [TestCase("TC JC QC KC AC", "TC JC QC KC AC", 0, Description = "A RoyalFlush is equal to another Royalflush")]
        [TestCase("TC JC QC KC AC", "AC KC QC JC TC", 0, Description = "A RoyalFlush is equal to another Royalflush")]
        [TestCase("TC JC QC KC AC", "TS JS QS KS AS", 0, Description = "A RoyalFlush is equal to another Royalflush of a different suit ONLY when ordering")]
        public void Test_RoyalFlush_ComparableTests(string royalFlushA, string royalFlushB, int comp)
        {
            var cardsA = Utils.ParseCards(royalFlushA);
            var cardsB = Utils.ParseCards(royalFlushB);
            var royalA = RoyalFlush.CreateInstance(cardsA);
            var royalB = RoyalFlush.CreateInstance(cardsB);

            Assert.AreEqual(comp, royalA.CompareTo(royalB));

            if(comp == 0)
            {
                Assert.IsTrue(royalA >= royalB);
                Assert.IsTrue(royalA <= royalB);
                Assert.IsFalse(royalA > royalB);
                Assert.IsFalse(royalA < royalB);
            }else if (comp == 1)
            {
                Assert.IsTrue(royalA >= royalB);
                Assert.IsFalse(royalA <= royalB);
                Assert.IsTrue(royalA > royalB);
                Assert.IsFalse(royalA < royalB);
            }
            else if (comp == -1)
            {
                Assert.IsFalse(royalA >= royalB);
                Assert.IsTrue(royalA <= royalB);
                Assert.IsFalse(royalA > royalB);
                Assert.IsTrue(royalA < royalB);
            }
            else
            {
                throw new ArgumentException("the value of comp can only be -1,0,1");
            }
        }

        #endregion
    }
}
