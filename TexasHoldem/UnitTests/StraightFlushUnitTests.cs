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
    class StraightFlushUnitTests
    {
        [TestCase("2C 6C 4C 8C TC ", "3D 6D", "", false, Description = "No Straigh-Flush")]
        [TestCase("TD JD QD KD 9H ", "3D AD", "", false, Description = "RoyalFlush")]
        [TestCase("3H 4H 5H 6H 7H ", "3D AD", "", false, Description = "Community Card Straight-Flush")]
        [TestCase("2C 3C 4C 5C 9C ", "3D 6C", "2C 3C 4C 5C 6C", true, Description = "Straigh-Flush")]
        [TestCase("2C 3C 4C 5C 9C ", "3D AC", "AC 2C 3C 4C 5C", true, Description = "Straigh-Flush Low-Ace")]
        [TestCase("AC 2C 3C 4C 5C", "6C KC", "6C 5C 4C 3C 2C", true, Description = "Community Cards Low-Ace Straight-Flush 1")]
        [TestCase("AC 2C 3C 4C 5C", "6C 7C", "7C 6C 5C 4C 3C", true, Description = "Community Cards Low-Ace Straight-Flush 2")]
        [TestCase("4C 5C 6C 7C 8C", "2C 3C", "3C 4C 5C 6C 7C", true, Description = "Community Cards Mid-Range Straight-Flush 1")]
        [TestCase("4C 5C 6C 7C 8C", "3C KC", "3C 4C 5C 6C 7C", true, Description = "Community Cards Mid-Range Straight-Flush 2")]
        [TestCase("4C 5C 6C 7C 8C", "9C KC", "5C 6C 7C 8C 9C", true, Description = "Community Cards Mid-Range Straight-Flush 3")]
        [TestCase("4C 5C 6C 7C 8C", "9C TC", "6C 7C 8C 9C TC", true, Description = "Community Cards Mid-Range Straight-Flush 4")]
        public void Test_StraightFlush_CreateInstance(string strCommunityCards, string strHoleCards, string strStraightFlushCards, bool isValid)
        {
            var communityCards = Utils.ParseCards(strCommunityCards);
            var holeCards = Utils.ParseCards(strHoleCards);

            var straightFlush = StraightFlush.CreateInstance(communityCards, holeCards);

            if (isValid)
            {
                var straightFlushCards = Utils.ParseCards(strStraightFlushCards);

                Assert.IsNotNull(straightFlush);
                Assert.AreEqual(HandRanks.StraightFlush, straightFlush.HandRank);
                CollectionAssert.AreEquivalent(straightFlushCards, straightFlush.Cards);
            }
            else
            {
                Assert.IsNull(straightFlush);
            }
        }


        [TestCase("2D 3D 4D 5D 6D", "2D 3D 4D 5D 6D", true)]
        [TestCase("2D 3D 4D 5D 6D", "3D 4D 5D 6D 7D", false)]
        public void Test_StraightFlush_EqualityOperators(string strStraightFlushOne, string strStraightFlushTwo, bool areEqual)
        {
            var cardsA = Utils.ParseCards(strStraightFlushOne);
            var cardsB = Utils.ParseCards(strStraightFlushTwo);

            var straightFlushOne = StraightFlush.CreateInstance(cardsA);
            var straightFlushTwo = StraightFlush.CreateInstance(cardsB);

            Assert.AreEqual(areEqual, straightFlushOne.Equals(straightFlushTwo));
            Assert.AreEqual(areEqual, straightFlushOne.Equals((object)straightFlushTwo));
            Assert.AreEqual(areEqual, straightFlushOne == straightFlushTwo);
            Assert.AreEqual(!areEqual, straightFlushOne != straightFlushTwo);

            
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
    }
}
