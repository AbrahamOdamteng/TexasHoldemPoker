using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TexasHoldem.Hands;
using TexasHoldem.Utilities;
namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class RoyalFlushUnitTests
    {
        #region Constructor tests
        [TestCase("AS 6H 5C JD TD", "KS 2H", false, Description = "Test HighCard ")]
        [TestCase("3C 2D 5C JD TD", "KS KH", false, Description = "Test OnePair")]
        [TestCase("5C 6D 3C AD AC", "KS KH", false, Description = "Test TwoPair")]
        [TestCase("5C 7H AC AD AS", "KS 4H", false, Description = "Test ThreeOfAKind")]
        [TestCase("KH 2C 5C 6D 8S", "9C 7H", false, Description = "Test Straight")]
        [TestCase("6D 6H 6C TC QC", "2C 4C", false, Description = "Test Flush")]
        [TestCase("3S 4S 2D TC TS", "2S 2H", false, Description = "Test FullHouse")]
        [TestCase("AS 4S 2D 2C TS", "2S 2H", false, Description = "Test FoutOfAKind")]
        [TestCase("9C 3H 4C 6C 8C", "7C 5C", false, Description = "Test StraightFlush")]
        [TestCase("TD JD QD KD AD", "2H 7C", false, Description = "Test RoyalFlush in community")]
        [TestCase("TD JD QD KD 9S", "AD 7C", true, Description = "Test RoyalFlush")]
        [TestCase("TD JD KD", "AD QD", true, Description = "Test RoyalFlush")]
        public void Test_RoyalFlush_CreateInstance(string communityCardsStr, string holeCardsStr, bool isValid)
        {
            var communityCards = Utils.ParseCards(communityCardsStr);
            var holeCards = Utils.ParseCards(holeCardsStr);
            var royalFlush = RoyalFlush.CreateInstance(communityCards, holeCards);
            if (isValid)
            {
                Assert.NotNull(royalFlush);
                Assert.AreEqual(HandRanks.RoyalFlush, royalFlush.HandRank);
            }
            else
            {
                Assert.IsNull(royalFlush);
            }
        }

        [Test]
        public void Test_RoyalFlush_CreateInstance_Exception()
        {
            Assert.Fail();
        }
        #endregion

        #region Test Equality Operators


        [TestCase("TC JC QC KC AC", "TC JC QC KC AC", true, Description ="A RoyalFlush is equal to another Royalflush")]
        [TestCase("TC JC QC KC AC", "AC KC QC JC TC", true, Description = "A RoyalFlush is equal to another Royalflush")]
        [TestCase("TC JC QC KC AC", "TS JS QS KS AS", false, Description = "A RoyalFlush is NOT equal to another Royalflush of a different suit")]
        public void Test_RoyalFlush_EqualityOperators(string royalFlushA , string royalFlushB, bool areEqual)
        {
            var cardsA = Utils.ParseCards(royalFlushA);
            var cardsB = Utils.ParseCards(royalFlushB);
            var royalA = RoyalFlush.CreateInstance(cardsA);
            var royalB = RoyalFlush.CreateInstance(cardsB);

            Assert.AreEqual(areEqual, royalA.Equals(royalB));
            Assert.AreEqual(areEqual, royalA.Equals((object)royalB));
            Assert.AreEqual(areEqual, royalA == royalB);
            Assert.AreEqual(!areEqual, royalA != royalB);
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
