using Moq;
using TwoByFour.MultiplicationTable;
using TwoByFour.Utils;
using Xunit;

namespace TwoByFour.Tests.MultiplicationTable
{
    public class MultiplicationSmartFactory_Test
    {
        [Fact]
        public void SmartFactory_WillGenerate_RandomMultiplications()
        {
            // arrange
            var stubCourse = new Mock<ITrainingCourse>();
            stubCourse
                .SetupGet(c => c.AlreadySeenChallenges)
                .Returns(new Multiplication[] { });
            var stubRandomGenerator = new Mock<IMultiplicationRandomGenerator>();
            stubRandomGenerator
                .Setup(g => g.Generate())
                .Returns(new Multiplication { BaseNumber = 2, Multiplier = 3 });
            var stubRandomWrapper = new Mock<IRandomWrapper>();
            var sut = new MultiplicationSmartFactory(stubRandomGenerator.Object, stubCourse.Object, stubRandomWrapper.Object);

            // act
            var mult = sut.CreateMultiplication();

            // assert
            Assert.Equal(2, mult.BaseNumber);
            Assert.Equal(3, mult.Multiplier);
        }

        [Fact]
        public void SmartFactory_WontGenerate_SameMultiplicationTwice()
        {
            // arrange
            var stubCourse = new Mock<ITrainingCourse>();
            stubCourse
                .SetupGet(c => c.AlreadySeenChallenges)
                .Returns(new Multiplication[] { new Multiplication { BaseNumber = 2, Multiplier = 3 } });
            var stubRandomGenerator = new Mock<IMultiplicationRandomGenerator>();
            stubRandomGenerator
                .SetupSequence(g => g.Generate())
                .Returns(new Multiplication { BaseNumber = 2, Multiplier = 3 }) // !was already used in session!
                .Returns(new Multiplication { BaseNumber = 4, Multiplier = 5 });
            var stubRandomWrapper = new Mock<IRandomWrapper>();
            var sut = new MultiplicationSmartFactory(stubRandomGenerator.Object, stubCourse.Object, stubRandomWrapper.Object);

            // act
            var mult = sut.CreateMultiplication();

            // assert
            Assert.Equal(4, mult.BaseNumber);
            Assert.Equal(5, mult.Multiplier);
        }

        [Fact]
        public void SmartFactory_ExtendsSessionHistory()
        {
            // arrange
            var mockCourse = new Mock<ITrainingCourse>();
            mockCourse
                .SetupGet(c => c.AlreadySeenChallenges)
                .Returns(new Multiplication[] { });
            var stubRandomGenerator = new Mock<IMultiplicationRandomGenerator>();
            stubRandomGenerator
                .Setup(g => g.Generate())
                .Returns(new Multiplication { BaseNumber = 4, Multiplier = 5 });
            var stubRandomWrapper = new Mock<IRandomWrapper>();
            var sut = new MultiplicationSmartFactory(stubRandomGenerator.Object, mockCourse.Object, stubRandomWrapper.Object);

            // act
            var mult = sut.CreateMultiplication();

            // assert
            mockCourse.Verify(c => c.SetAlreadySeen(new Multiplication { BaseNumber = 4, Multiplier = 5 }));
            mockCourse.Verify(c => c.SetAlreadySeen(It.IsAny<Multiplication>()), Times.Once);
        }

        [Fact]
        public void SmartFactory_Selects_WhatNeedsMoreLearning()
        {
            // arrange
            var mockCourse = new Mock<ITrainingCourse>();
            mockCourse
                .SetupGet(c => c.AlreadySeenChallenges)
                .Returns(new Multiplication[] { });
            mockCourse
                .SetupGet(c => c.NeedMoreLearningChallenges)
                .Returns(new Multiplication[] { new Multiplication { BaseNumber = 4, Multiplier = 6 } });
            var stubRandomGenerator = new Mock<IMultiplicationRandomGenerator>();
            stubRandomGenerator
                .Setup(g => g.Generate())
                .Returns(new Multiplication { BaseNumber = 2, Multiplier = 3 });
            var stubRandomWrapper = new Mock<IRandomWrapper>();
            stubRandomWrapper
                .Setup(r => r.Next(0, 1 + 1))
                .Returns(1); // '1' for repeated multiplication (among those registered for more learning, repeating)
            var sut = new MultiplicationSmartFactory(stubRandomGenerator.Object, mockCourse.Object, stubRandomWrapper.Object);

            // act
            var mult = sut.CreateMultiplication();

            // assert
            Assert.Equal(new Multiplication { BaseNumber = 4, Multiplier = 6 }, mult);
        }

        [Fact]
        public void SmartFactory_Selects_NewMultiplication()
        {
            // arrange
            var mockCourse = new Mock<ITrainingCourse>();
            mockCourse
                .SetupGet(c => c.AlreadySeenChallenges)
                .Returns(new Multiplication[] { });
            mockCourse
                .SetupGet(c => c.NeedMoreLearningChallenges)
                .Returns(new Multiplication[] { new Multiplication { BaseNumber = 4, Multiplier = 6 } });
            var stubRandomGenerator = new Mock<IMultiplicationRandomGenerator>();
            stubRandomGenerator
                .Setup(g => g.Generate())
                .Returns(new Multiplication { BaseNumber = 2, Multiplier = 3 });
            var stubRandomWrapper = new Mock<IRandomWrapper>();
            stubRandomWrapper
                .Setup(r => r.Next(0, 1 + 1))
                .Returns(0); // '0' for new multiplication
            var sut = new MultiplicationSmartFactory(stubRandomGenerator.Object, mockCourse.Object, stubRandomWrapper.Object);

            // act
            var mult = sut.CreateMultiplication();

            // assert
            Assert.Equal(new Multiplication { BaseNumber = 2, Multiplier = 3 }, mult);
        }
    }
}
