﻿using NUnit.Framework;
using System.Collections.Generic;
using TexasHoldem.Deck;
using TexasHoldem.Hands;
using TexasHoldem.Interfaces;
namespace TexasHoldem.UnitTests
{
    class BaseUnitTests
    {
        public void CreateInstanceHelper(IPokerHand pokerHand,HandRanks expectedHandRank,IEnumerable<Card> cards, bool isValid)
        {
            if (isValid)
            {
                Assert.NotNull(pokerHand);
                Assert.AreEqual(expectedHandRank, pokerHand.HandRank);
                CollectionAssert.AreEquivalent(cards, pokerHand.Cards);
            }
            else
            {
                Assert.IsNull(pokerHand);
            }
        }

        protected void EqualityOperatorsHelper(BaseHand pokerHandA, BaseHand pokerHandB, bool areEqual)
        {
            Assert.NotNull(pokerHandA);
            Assert.NotNull(pokerHandB);
            Assert.IsFalse(object.ReferenceEquals(pokerHandA, pokerHandB));

            Assert.AreEqual(areEqual, pokerHandA.Equals(pokerHandB));
            Assert.AreEqual(areEqual, pokerHandA.Equals((object)pokerHandB));
            Assert.AreEqual(areEqual, pokerHandA == pokerHandB);
            Assert.AreEqual(!areEqual, pokerHandA != pokerHandB);
        }
    }
}
