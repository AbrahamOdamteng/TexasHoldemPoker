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


        [Test]
        public void Test_Card_Constructor_Error()
        {
            var sizeMsg = "The string representation of a card must be exactly two characters";

            Assert.That(() => new Card(string.Empty),
                Throws.ArgumentException.With.Message.EqualTo(sizeMsg));

            Assert.That(() => new Card("abc"),
                Throws.ArgumentException.With.Message.EqualTo(sizeMsg));

            Assert.That(() => new Card("1H"),
                Throws.ArgumentException.With.Message.EqualTo("Enum 'CardRanks' does not contain value: 1"));

            Assert.That(() => new Card("FH"),
                Throws.ArgumentException.With.Message.EqualTo("Enum 'CardRanks' does not contain value: F"));

            Assert.That(() => new Card("2Z"),
                Throws.ArgumentException.With.Message.EqualTo("Enum 'CardSuits' does not contain value: Z"));
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
