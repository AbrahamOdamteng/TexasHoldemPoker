using NUnit.Framework;
using System;
using TexasHoldem.Hands;
using TexasHoldem.Utilities;

namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class ThreeOfAkindUnitTests
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
        [TestCase("2H 2C 2S 5S 9C ", true, Description = "three of a kind, twos")]
        [TestCase("QH QD QS 5C 9C ", true, Description = "three of a kind, queens")]
        //Two Pair===============================================================
        [TestCase("TC TD JC JD 9C ", false, Description = "Two Pair, Jacks over tens")]
        [TestCase("7C 9D 7S 9S TC ", false, Description = "Two Pair, nines over sevens")]
        //One Pair================================================================
        [TestCase("9C 9H 4C 5C 6C ", false, Description = "One Pair, nines")]
        [TestCase("KC 9H 4C 5C KD ", false, Description = "One Pair, kings")]
        //High Card===============================================================
        [TestCase("KH JH 8S 7D 4S", false, Description = "High Card, king")]
        [TestCase("QS JD 6C 5H 3C", false, Description = "High Card, Queen")]
        public void Test_ThreeOfAkind_CreateInstance(string strCards, bool isValid)
        {
            var cards = Utils.ParseCards(strCards);
            var threeOfAKind = ThreeOfAkind.CreateInstance(cards);

            if (isValid)
            {
                Assert.NotNull(threeOfAKind);
                Assert.AreEqual(HandRanks.ThreeOfAkind, threeOfAKind.HandRank);
                CollectionAssert.AreEquivalent(cards, threeOfAKind.Cards);
            }
            else
            {
                Assert.IsNull(threeOfAKind);
            }
        }

        //[TestCase("2C 3C 4C 5C 9C ", "3D 6C", "2C 3C 4C 5C 6C", true, Description = "Description")]
        //public void Test_ThreeOfAkind_CreateInstance(string strCommunityCards, string strHoleCards, string strCards, bool isValid)
        //{
        //    Assert.Fail();
        //    var communityCards = Utils.ParseCards(strCommunityCards);
        //    var holeCards = Utils.ParseCards(strHoleCards);

        //    var pokername = StraightFlush.CreateInstance(communityCards, holeCards);

        //    if (isValid)
        //    {
        //        var straightFlushCards = Utils.ParseCards(strCards);

        //        Assert.IsNotNull(pokername);
        //        Assert.AreEqual(HandRanks.StraightFlush, pokername.HandRank);
        //        CollectionAssert.AreEquivalent(straightFlushCards, pokername.Cards);
        //    }
        //    else
        //    {
        //        Assert.IsNull(pokername);
        //    }
        //}


        [TestCase("2D 2C 2S 5D 6D", "2D 2C 2S 5D 6D", true)]
        [TestCase("QH QD QC 5D 6D", "3D 4D 3C 6D 3H", false)]
        public void Test_ThreeOfAkind_EqualityOperators(string strInputA, string strInputB, bool areEqual)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);

            var threeOfAKindOne = ThreeOfAkind.CreateInstance(cardsA);
            var threeOfAKindTwo = ThreeOfAkind.CreateInstance(cardsB);

            Assert.NotNull(threeOfAKindOne);
            Assert.NotNull(threeOfAKindTwo);

            Assert.AreEqual(areEqual, threeOfAKindOne.Equals(threeOfAKindTwo));
            Assert.AreEqual(areEqual, threeOfAKindOne.Equals((object)threeOfAKindTwo));
            Assert.AreEqual(areEqual, threeOfAKindOne == threeOfAKindTwo);
            Assert.AreEqual(!areEqual, threeOfAKindOne != threeOfAKindTwo);
        }

        [Test]
        public void Test_ThreeOfAkind_EqualityOperators_ForNull()
        {
            var cards = Utils.ParseCards("2D 2C 2S 5D 6D");
            var threeOfAKind = ThreeOfAkind.CreateInstance(cards);

            Assert.False(threeOfAKind.Equals(null));

            Assert.True((ThreeOfAkind)null == (ThreeOfAkind)null);
            Assert.False((ThreeOfAkind)null == threeOfAKind);
            Assert.False(threeOfAKind == (ThreeOfAkind)null);

            Assert.False((ThreeOfAkind)null != (ThreeOfAkind)null);
            Assert.True((ThreeOfAkind)null != threeOfAKind);
            Assert.True(threeOfAKind != (ThreeOfAkind)null);
        }

        [TestCase("2H 3H 4H 5H 6H", "2D 3D 4D 5D 6D", 0)]
        [TestCase("AH 2H 3H 4H 5H", "2D 3D 4D 5D 6D", -1)]
        [TestCase("2D 3D 4D 5D 6D", "AH 2H 3H 4H 5H", 1)]
        public void Test_ThreeOfAkind_ComparableTests(string strInputA, string strInputB, int comp)
        {
            throw new NotImplementedException();
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
