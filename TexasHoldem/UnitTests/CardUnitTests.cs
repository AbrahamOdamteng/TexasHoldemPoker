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


        #region Equality Tests -----------------------------------------
        [TestCase("2H", "2H", ExpectedResult = true)]
        [TestCase("2H", "5H", ExpectedResult = false)]
        [TestCase("AC","AH", ExpectedResult = false)]
        public bool Test_Card_Equals(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne.Equals(cardTwo);
        }

        [TestCase("2H", "2H", ExpectedResult = true)]
        [TestCase("2H", "5H", ExpectedResult = false)]
        [TestCase("AC", "AH", ExpectedResult = false)]
        public bool Test_Card_Object_Equals(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne.Equals((Object)cardTwo);
        }

        [Test]
        public void Test_Card_Object_Equals_Null()
        {
            var cardOne = new Card("2H");
            Assert.IsFalse(cardOne.Equals(null));
        }


        [TestCase("2H", "2H", ExpectedResult = true)]
        [TestCase("2H", "5H", ExpectedResult = false)]
        [TestCase("AC", "AH", ExpectedResult = false)]
        public bool Test_Card_DoubleEqualsOperator(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne == cardTwo;
        }

        [TestCase("2H", "2H", ExpectedResult = false)]
        [TestCase("2H", "5H", ExpectedResult = true)]
        [TestCase("AC", "AH", ExpectedResult = true)]
        public bool Test_Card_NotEqualsOperator(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne != cardTwo;
        }
        #endregion

        #region IComparable -----------------------------------------
        [TestCase("2H", "2H", ExpectedResult = 0)]
        [TestCase("2H", "5H", ExpectedResult = -1)]
        [TestCase("AC", "KH", ExpectedResult = 1)]
        public int Test_Card_CompareTo(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne.CompareTo(cardTwo);
        }

        [TestCase("2H", "2H", ExpectedResult = false)]
        [TestCase("2H", "5H", ExpectedResult = false)]
        [TestCase("AC", "KH", ExpectedResult = true)]
        public bool Test_Card_GreaterThanOperator(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne > cardTwo ;
        }

        [TestCase("2H", "2H", ExpectedResult = true)]
        [TestCase("2H", "5H", ExpectedResult = false)]
        [TestCase("AC", "KH", ExpectedResult = true)]
        public bool Test_Card_GreaterThanOrEqualToOperator(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne >= cardTwo;
        }


        [TestCase("2H", "2H", ExpectedResult = false)]
        [TestCase("2H", "5H", ExpectedResult = true)]
        [TestCase("AC", "KH", ExpectedResult = false)]
        public bool Test_Card_LessThanOperator(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne < cardTwo;
        }

        [TestCase("2H", "2H", ExpectedResult = true)]
        [TestCase("2H", "5H", ExpectedResult = true)]
        [TestCase("AC", "KH", ExpectedResult = false)]
        public bool Test_Card_LessThanOrEqualToOperator(string cardA, string cardB)
        {
            var cardOne = new Card(cardA);
            var cardTwo = new Card(cardB);
            return cardOne <= cardTwo;
        }
        #endregion
    }
}
