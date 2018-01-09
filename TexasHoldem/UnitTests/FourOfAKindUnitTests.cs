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

        [TestCase("4C 4D 4H 4S 5C","5C 5S", "", false, Description = "FourOfAKind in the community cards")]
        [TestCase("4C 4D 4H 5C 6C", "4S 5H", "4C 4D 4H 4S 6C", true, Description = "Valid FourOfAKind")]
        public void Test_FourOfAKind_CreateInstance(string strCommunityCards, string strHoleCards, string strFourOfAKind, bool isValid)
        {
            var communityCards = Utils.ParseCards(strCommunityCards);
            var holeCards = Utils.ParseCards(strHoleCards);

            var fourOfAKind = FourOfAKind.CreateInstance(communityCards, holeCards);

            if (isValid)
            {
                Assert.NotNull(fourOfAKind);
                var fourOfAKindCards = Utils.ParseCards(strFourOfAKind);
                CollectionAssert.AreEquivalent(fourOfAKindCards, fourOfAKind.Cards);
            }
            else
            {
                Assert.Null(fourOfAKind);
            }
        }

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
