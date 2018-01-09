using NUnit.Framework;
using System;
using TexasHoldem.Hands;
using TexasHoldem.Utilities;

namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class FullHouseUnitTests
    {
        [TestCase("2C 3C 4C 5C 9C ", "3D 6C", "2C 3C 4C 5C 6C", true, Description = "Description")]
        public void Test_FullHouse_CreateInstance(string strCommunityCards, string strHoleCards, string strCards, bool isValid)
        {
            var communityCards = Utils.ParseCards(strCommunityCards);
            var holeCards = Utils.ParseCards(strHoleCards);

            var pokername = StraightFlush.CreateInstance(communityCards, holeCards);

            if (isValid)
            {
                var straightFlushCards = Utils.ParseCards(strCards);

                Assert.IsNotNull(pokername);
                Assert.AreEqual(HandRanks.FullHouse, pokername.HandRank);
                CollectionAssert.AreEquivalent(straightFlushCards, pokername.Cards);
            }
            else
            {
                Assert.IsNull(pokername);
            }
        }


        [TestCase("2D 3D 4D 5D 6D", "2D 3D 4D 5D 6D", true)]
        [TestCase("2D 3D 4D 5D 6D", "3D 4D 5D 6D 7D", false)]
        public void Test_FullHouse_EqualityOperators(string strInputA, string strInputB, bool areEqual)
        {
            throw new NotImplementedException();
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);

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
        public void Test_FullHouse_ComparableTests(string strInputA, string strInputB, int comp)
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

        [Test]
        public void Test_FullHouse_EqualityOperators_ForNull()
        {
            throw new NotImplementedException();

            var cards = Utils.ParseCards("4H 5H 6H 7H 8H");
            var straintFlush = StraightFlush.CreateInstance(cards);

            Assert.False(straintFlush.Equals(null));

            Assert.True((FullHouse)null == (FullHouse)null);
            Assert.False((FullHouse)null == straintFlush);
            Assert.False(straintFlush == (FullHouse)null);

            Assert.False((FullHouse)null != (FullHouse)null);
            Assert.True((FullHouse)null != straintFlush);
            Assert.True(straintFlush != (FullHouse)null);
        }
    }
}

