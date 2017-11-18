using Moq;
using TwoByFour.MultiplicationTable;
using TwoByFour.Utils;
using Xunit;

namespace TwoByFour.Tests.MultiplicationTable
{
    public class MultiplicationRandomGenerator_Test
    {
        [Fact]
        public void BaseNumberSet()
        {
            // arrange
            var stubRandomWrapper = new Mock<IRandomWrapper>();
            stubRandomWrapper
                .Setup(r => r.Next(0, 2 + 1))
                .Returns(2);
            var sut = new MultiplicationRandomGenerator(stubRandomWrapper.Object);

            // act
            sut.Init(new[] { 2, 3, 4 });
            var mult = sut.Generate();

            // assert
            Assert.Equal(4, mult.BaseNumber);
        }

        [Fact]
        public void MultiplierSet()
        {
            // arrange
            var stubRandomWrapper = new Mock<IRandomWrapper>();
            stubRandomWrapper
                .SetupSequence(r => r.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(2) // used for BaseNumber
                .Returns(8);// used for Multiplier
            var sut = new MultiplicationRandomGenerator(stubRandomWrapper.Object);

            // act
            sut.Init(new[] { 2, 3, 4 });
            var mult = sut.Generate();

            // assert
            Assert.Equal(8, mult.Multiplier);
        }
    }
}
