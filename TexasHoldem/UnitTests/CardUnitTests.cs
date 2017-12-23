using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TexasHoldem.Deck;
namespace TexasHoldem.UnitTests
{
    [TestFixture]
    class CardUnitTests
    {
        [TestCase("2H", CardRanks._2, CardSuits.H)]
        [TestCase("AH", CardRanks.A, CardSuits.H)]
        [TestCase("   TC     ", CardRanks.T, CardSuits.C)]
        [TestCase("   J  D     ", CardRanks.J, CardSuits.D)]
        public void Test_Card_Constructor(string strCard, CardRanks rank, CardSuits suit)
        {
            var card2h = new Card(strCard);
            Assert.AreEqual(rank, card2h.Rank);
            Assert.AreEqual(suit, card2h.Suit);
        }

        [TestCase("", "The string representation of a card must be exactly two characters")]
        [TestCase("abc", "The string representation of a card must be exactly two characters")]
        [TestCase("1H", "Enum 'CardRanks' does not contain value: 1")]
        [TestCase("2Z", "Enum 'CardSuits' does not contain value: Z")]
        [TestCase("0Y", "Enum 'CardRanks' does not contain value: 0")]
        public void Test_Card_Constructor_ErrorMessages(string strCard,string strErrorMsg)
        {
            Assert.That(() => new Card(strCard),
                Throws.ArgumentException.With.Message.EqualTo(strErrorMsg));
        }

        [TestCase("2H", ExpectedResult = false)]
        [TestCase("9D", ExpectedResult = false)]
        [TestCase("TC", ExpectedResult = true)]
        [TestCase("JD", ExpectedResult = true)]
        [TestCase("QH", ExpectedResult = true)]
        [TestCase("KS", ExpectedResult = true)]
        [TestCase("AC", ExpectedResult = true)]
        public bool Test_IsRoyal(string strCard)
        {
            var card = new Card(strCard);
            return card.IsRoyal();
        }
    }
}
