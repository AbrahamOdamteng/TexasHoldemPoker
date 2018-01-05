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
        [TestCase("2C 3C 4C 5C 9C ", "3D 6C", "2C 3C 4C 5C 6C", true, Description = "Straigh-Flush")]
        //[TestCase("2C 3C 4C 5C 9C ", "3D AC", "AC 2C 3C 4C 5C", true, Description = "Straigh-Flush Low-Ace")]
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
    }
}
