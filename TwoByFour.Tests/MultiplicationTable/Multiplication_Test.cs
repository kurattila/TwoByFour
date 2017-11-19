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
    }
}
