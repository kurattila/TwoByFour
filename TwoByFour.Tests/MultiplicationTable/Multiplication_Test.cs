using System.Collections.Generic;
using TwoByFour.MultiplicationTable;
using Xunit;

namespace TwoByFour.Tests.MultiplicationTable
{
    public class Multiplication_Test
    {
        [Fact]
        public void Multiplication_NotInHashSetYet()
        {
            // arrange
            var mult1 = new Multiplication
            {
                BaseNumber = 2,
                Multiplier = 4
            };
            var mult2 = new Multiplication
            {
                BaseNumber = 5,
                Multiplier = 7
            };
            var set = new HashSet<Multiplication>();

            // act
            set.Add(mult1);

            // assert
            Assert.DoesNotContain(mult2, set);
        }

        [Fact]
        public void Multiplication_AlreadyInHashSet()
        {
            // arrange
            var mult1 = new Multiplication
            {
                BaseNumber = 2,
                Multiplier = 4
            };
            var mult2 = new Multiplication
            {
                BaseNumber = 2,
                Multiplier = 4
            };
            var set = new HashSet<Multiplication>();

            // act
            set.Add(mult1);

            // assert
            Assert.Contains(mult2, set);
        }

        [Fact]
        public void TextualChallenge()
        {
            // arrange
            var sut = new Multiplication
            {
                BaseNumber = 4,
                Multiplier = 6
            };

            // act
            var textualResult = sut.TextualChallenge;

            // assert
            Assert.Equal("6 x 4 = ?", textualResult);
        }

        [Fact]
        public void TextualResult()
        {
            // arrange
            var sut = new Multiplication
            {
                BaseNumber = 4,
                Multiplier = 6
            };

            // act
            var textualResult = sut.TextualResult;

            // assert
            Assert.Equal("6 x 4 = 24", textualResult);
        }
    }
}
