using Moq;
using TwoByFour.MultiplicationTable;
using TwoByFour.Pages;
using Xunit;

namespace TwoByFour.Tests.Pages
{
    public class IndexModel_Test
    {
        [Fact]
        public void Get_PresentsNewChallenge()
        {
            // arrange
            var dummyCourse = new Mock<ITrainingCourse>();
            var dummyMult = new Multiplication { BaseNumber = 3, Multiplier = 4 };
            var mockSmartFactory = new Mock<IMultiplicationSmartFactory>();
            mockSmartFactory
                .Setup(f => f.CreateMultiplication())
                .Returns(dummyMult);
            var sut = new IndexModel(dummyCourse.Object, mockSmartFactory.Object);

            // act
            sut.OnGet();

            // assert
            Assert.Equal(dummyMult.TextualChallenge, sut.ChallengeText);
        }

        [Fact]
        public void Get_AddsChallenge_ToAlreadySeenOnes()
        {
            // arrange
            var mockCourse = new Mock<ITrainingCourse>();
            var dummyMult = new Multiplication { BaseNumber = 3, Multiplier = 4 };
            var mockSmartFactory = new Mock<IMultiplicationSmartFactory>();
            mockSmartFactory
                .Setup(f => f.CreateMultiplication())
                .Returns(dummyMult);
            var sut = new IndexModel(mockCourse.Object, mockSmartFactory.Object);

            // act
            sut.OnGet();

            // assert
            mockCourse.Verify(c => c.SetAlreadySeen(dummyMult));
        }
    }
}
