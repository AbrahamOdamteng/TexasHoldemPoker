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
    class FourOfAKindUnitTests
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
        [TestCase("4C 4D 4H 4S 5C", true, Description = "Four of a Kind, fours")]
        [TestCase("AH AD 7D AS AC", true, Description = "Four of a Kind, aces")]
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
        public void Test_FourOfAKind_CreateInstance(string strCards, bool isValid)
        {
            var cards = Utils.ParseCards(strCards);
            var fourOfAKind = FourOfAKind.CreateInstance(cards);

            if (isValid)
            {
                Assert.NotNull(fourOfAKind);
                Assert.AreEqual(HandRanks.FourOfAKind, fourOfAKind.HandRank);
                CollectionAssert.AreEquivalent(cards, fourOfAKind.Cards);
            }
            else
            {
                Assert.IsNull(fourOfAKind);
            }
        }


        //[TestCase("4C 4D 4H 4S 5C","5C 5S", "", false, Description = "FourOfAKind in the community cards")]
        //[TestCase("4C 4D 4H 5C 6C", "4S 5H", "4C 4D 4H 4S 6C", true, Description = "Valid FourOfAKind")]
        //public void Test_FourOfAKind_CreateInstance(string strCommunityCards, string strHoleCards, string strFourOfAKind, bool isValid)
        //{
        //    Assert.Fail();
        //    var communityCards = Utils.ParseCards(strCommunityCards);
        //    var holeCards = Utils.ParseCards(strHoleCards);

        //    var fourOfAKind = FourOfAKind.CreateInstance(communityCards, holeCards);

        //    if (isValid)
        //    {
        //        Assert.NotNull(fourOfAKind);
        //        var fourOfAKindCards = Utils.ParseCards(strFourOfAKind);
        //        CollectionAssert.AreEquivalent(fourOfAKindCards, fourOfAKind.Cards);
        //    }
        //    else
        //    {
        //        Assert.Null(fourOfAKind);
        //    }
        //}

        [TestCase("5C 5C 5C 5C 9C", "5C 5C 5C 5C 9C", true)]
        [TestCase("5C 5C 5C 5C 9C", "6C 6C 6C 6C 9C", false)]
        public void Test_FourOfAKind_EqualityOperators(string strInputA, string strInputB, bool areEqual)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);

            var FourOfAKindOne = FourOfAKind.CreateInstance(cardsA);
            var FourOfAKindTwo = FourOfAKind.CreateInstance(cardsB);

            Assert.AreEqual(areEqual, FourOfAKindOne.Equals(FourOfAKindTwo));
            Assert.AreEqual(areEqual, FourOfAKindOne.Equals((object)FourOfAKindTwo));
            Assert.AreEqual(areEqual, FourOfAKindOne == FourOfAKindTwo);
            Assert.AreEqual(!areEqual, FourOfAKindOne != FourOfAKindTwo);
        }

        [TestCase("7S 7S 7S 7S 8H", "7S 7S 7S 7S 8H", 0, Description = "Four-of-a-kind comparable zero test")]
        [TestCase("7S 7S 7S 7S 8H", "7S 7S 7S 7S 9S", -1, Description = "Four-of-a-kind kicker comparable test ")]
        [TestCase("7S 7S 7S 7S 9H", "7S 7S 7S 7S 8S", 1, Description = "Four-of-a-kind kicker comparable test inverse")]
        [TestCase("7S 7S 7S 7S 8H", "8D 8D 8D 8D 9H", -1, Description = "Four-of-a-kind quad comparable test ")]
        [TestCase("8D 8D 8D 8D 9H", "7S 7S 7S 7S 8H", 1, Description = "Four-of-a-kind quad comparable test inverse")]
        public void Test_FourOfAKind_ComparableTests(string strInputA, string strInputB, int comp)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);
            var fourOfAKindOne = FourOfAKind.CreateInstance(cardsA);
            var fourOfAKindTwo = FourOfAKind.CreateInstance(cardsB);

            Assert.AreEqual(comp, fourOfAKindOne.CompareTo(fourOfAKindTwo));

            if (comp == 0)
            {
                Assert.IsTrue(fourOfAKindOne >= fourOfAKindTwo);
                Assert.IsTrue(fourOfAKindOne <= fourOfAKindTwo);
                Assert.IsFalse(fourOfAKindOne > fourOfAKindTwo);
                Assert.IsFalse(fourOfAKindOne < fourOfAKindTwo);
            }
            else if (comp == 1)
            {
                Assert.IsTrue(fourOfAKindOne >= fourOfAKindTwo);
                Assert.IsFalse(fourOfAKindOne <= fourOfAKindTwo);
                Assert.IsTrue(fourOfAKindOne > fourOfAKindTwo);
                Assert.IsFalse(fourOfAKindOne < fourOfAKindTwo);
            }
            else if (comp == -1)
            {
                Assert.IsFalse(fourOfAKindOne >= fourOfAKindTwo);
                Assert.IsTrue(fourOfAKindOne <= fourOfAKindTwo);
                Assert.IsFalse(fourOfAKindOne > fourOfAKindTwo);
                Assert.IsTrue(fourOfAKindOne < fourOfAKindTwo);
            }
            else
            {
                throw new ArgumentException("the value of comp can only be -1,0,1");
            }
        }



        [Test]
        public void Test_FourOfAKind_EqualityOperators_ForNull()
        {
            var cards = Utils.ParseCards("7S 7S 7S 7S 8H");
            var straintFlush = FourOfAKind.CreateInstance(cards);

            Assert.False(straintFlush.Equals(null));

            Assert.True((FourOfAKind)null == (FourOfAKind)null);
            Assert.False((FourOfAKind)null == straintFlush);
            Assert.False(straintFlush == (FourOfAKind)null);

            Assert.False((FourOfAKind)null != (FourOfAKind)null);
            Assert.True((FourOfAKind)null != straintFlush);
            Assert.True(straintFlush != (FourOfAKind)null);
        }
    }
}
