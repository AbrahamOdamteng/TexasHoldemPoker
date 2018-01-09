using NUnit.Framework;
using System;
using TexasHoldem.Hands;
using TexasHoldem.Utilities;

namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class HighCardUnitTests
    {
        [TestCase("2C 4D 6H 8S TC", "QD AC", "AC QD", true, Description = "Test HighCard Constructor")]
        [TestCase("TD JD QD KD AD", "3S 9S", "", false, Description = "Test HighCard CreateInstance with lower card staright")]
        [TestCase("2C 4D 6H 8S TC", "AD AC", "", false, Description = "Test HighCard CreateInstance with one pair in the holecards")]
        [TestCase("2C 4D 6H 8S TC", "TD AC", "", false, Description = "Test HighCard CreateInstance with one pair")]
        [TestCase("2D 4D 6D 8D TC", "QD AC", "", false, Description = "Test HighCard CreateInstance with Flush")]
        [TestCase("2D 4D 6D 8S TC", "QD AD", "", false, Description = "Test HighCard CreateInstance with Flush")]
        [TestCase("TD JD QD KD 9D", "AD 9S", "", false, Description = "Test HighCard CreateInstance with RoyalFlush")]
        [TestCase("JD QD KD 2S 3C", "AD TD", "", false, Description = "Test HighCard CreateInstance with RoyalFlush")]
        [TestCase("2C 3D 4H 5S 6C", "AD 7H", "", false, Description = "Test HighCard CreateInstance with Low Ace Straight")]
        public void Test_HighCard_CreateInstance(string strCommunityCards, string strHoleCards, string strCards, bool isValid)
        {
            var communityCards = Utils.ParseCards(strCommunityCards);
            var holeCards = Utils.ParseCards(strHoleCards);

            var highCard = HighCard.CreateInstance(communityCards, holeCards);

            if (isValid)
            {
                var cards = Utils.ParseCards(strCards);

                Assert.IsNotNull(highCard);
                Assert.AreEqual(HandRanks.HighCard, highCard.HandRank);
                CollectionAssert.AreEquivalent(cards, highCard.Cards);
            }
            else
            {
                Assert.IsNull(highCard);
            }
        }

        
        [TestCase("2D 3D", "2D 3D", true)]
        [TestCase("2D 4D", "3D 4D", false)]
        public void Test_HighCard_EqualityOperators(string strInputA, string strInputB, bool areEqual)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);

            var highCardOne = HighCard.CreateInstance(cardsA);
            var highCardTwo = HighCard.CreateInstance(cardsB);

            Assert.AreEqual(areEqual, highCardOne.Equals(highCardTwo));
            Assert.AreEqual(areEqual, highCardOne.Equals((object)highCardTwo));
            Assert.AreEqual(areEqual, highCardOne == highCardTwo);
            Assert.AreEqual(!areEqual, highCardOne != highCardTwo);
        }


        [TestCase("2H 3H", "2D 3D", 0)]
        [TestCase("2H 3H", "3D 4D", -1)]
        [TestCase("6D 3D", "4H 2H", 1)]
        [TestCase("AD 3D", "AH 2H", 1)]
        [TestCase("AD 3D", "AH 4H", -1)]
        public void Test_HighCard_ComparableTests(string strInputA, string strInputB, int comp)
        {
            var cardsA = Utils.ParseCards(strInputA);
            var cardsB = Utils.ParseCards(strInputB);
            var hightCardOne = HighCard.CreateInstance(cardsA);
            var hightCardTwo = HighCard.CreateInstance(cardsB);

            Assert.AreEqual(comp, hightCardOne.CompareTo(hightCardTwo));

            if (comp == 0)
            {
                Assert.IsTrue(hightCardOne >= hightCardTwo);
                Assert.IsTrue(hightCardOne <= hightCardTwo);
                Assert.IsFalse(hightCardOne > hightCardTwo);
                Assert.IsFalse(hightCardOne < hightCardTwo);
            }
            else if (comp == 1)
            {
                Assert.IsTrue(hightCardOne >= hightCardTwo);
                Assert.IsFalse(hightCardOne <= hightCardTwo);
                Assert.IsTrue(hightCardOne > hightCardTwo);
                Assert.IsFalse(hightCardOne < hightCardTwo);
            }
            else if (comp == -1)
            {
                Assert.IsFalse(hightCardOne >= hightCardTwo);
                Assert.IsTrue(hightCardOne <= hightCardTwo);
                Assert.IsFalse(hightCardOne > hightCardTwo);
                Assert.IsTrue(hightCardOne < hightCardTwo);
            }
            else
            {
                throw new ArgumentException("the value of comp can only be -1,0,1");
            }
        }

        [Test]
        public void Test_HighCard_EqualityOperators_ForNull()
        {
            var cards = Utils.ParseCards("4H 5H");
            var highCard = HighCard.CreateInstance(cards);

            Assert.False(highCard.Equals(null));

            Assert.True((HighCard)null == (HighCard)null);
            Assert.False((HighCard)null == highCard);
            Assert.False(highCard == (HighCard)null);

            Assert.False((HighCard)null != (HighCard)null);
            Assert.True((HighCard)null != highCard);
            Assert.True(highCard != (HighCard)null);
        }
    }
}
