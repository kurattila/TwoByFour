using Microsoft.AspNetCore.Mvc;
using Moq;
using TwoByFour.MultiplicationTable;
using Xunit;

namespace TwoByFour.Pages.Tests
{
    public class ResultsModel_Test
    {
        [Fact]
        public void Get_ListsAllSeenChallenges()
        {
            // arrange
            var mults = new Multiplication[]
            {
                new Multiplication{BaseNumber=2, Multiplier=3},
                new Multiplication{BaseNumber=4, Multiplier=5},
                new Multiplication{BaseNumber=6, Multiplier=7}
            };
            var mockCourse = new Mock<ITrainingCourse>();
            mockCourse
                .Setup(c => c.AlreadySeenChallenges)
                .Returns(mults);
            var sut = new ResultsModel(mockCourse.Object);

            // act
            sut.OnGet();
            sut.OnPost();

            // assert
            Assert.Equal(3, sut.ResultEvaluations.Count);
            Assert.Equal(mults[0].TextualResult, sut.ResultEvaluations[0].Result);
            Assert.Equal(mults[1].TextualResult, sut.ResultEvaluations[1].Result);
            Assert.Equal(mults[2].TextualResult, sut.ResultEvaluations[2].Result);
        }

        [Fact]
        public void Get_AssumesAllResultsCorrect()
        {
            // arrange
            var mults = new Multiplication[]
            {
                new Multiplication{BaseNumber=2, Multiplier=3},
                new Multiplication{BaseNumber=4, Multiplier=5},
                new Multiplication{BaseNumber=6, Multiplier=7}
            };
            var mockCourse = new Mock<ITrainingCourse>();
            mockCourse
                .Setup(c => c.AlreadySeenChallenges)
                .Returns(mults);
            var sut = new ResultsModel(mockCourse.Object);

            // act
            sut.OnGet();

            // assert
            Assert.True(sut.ResultEvaluations[0].IsCorrect);
            Assert.True(sut.ResultEvaluations[1].IsCorrect);
            Assert.True(sut.ResultEvaluations[2].IsCorrect);
        }

        [Fact]
        public void Get_OffersResults_RatherThanNewSession()
        {
            // arrange
            var mults = new Multiplication[]{};
            var mockCourse = new Mock<ITrainingCourse>();
            mockCourse
                .Setup(c => c.AlreadySeenChallenges)
                .Returns(mults);
            var sut = new ResultsModel(mockCourse.Object);

            // act
            sut.OnGet();

            // assert
            Assert.False(sut.OfferNewSession);
        }

        [Fact]
        public void PostResults_OffersNewSession()
        {
            // arrange
            var mults = new Multiplication[]{};
            var mockCourse = new Mock<ITrainingCourse>();
            mockCourse
                .Setup(c => c.AlreadySeenChallenges)
                .Returns(mults);
            var sut = new ResultsModel(mockCourse.Object);

            // act
            sut.OnGet();
            sut.OnPost();

            // assert
            Assert.True(sut.OfferNewSession);
        }

        [Fact]
        public void Post_StartsNewSession()
        {
            // arrange
            var mockCourse = new Mock<ITrainingCourse>();
            var sut = new ResultsModel(mockCourse.Object);

            // act
            sut.OnGet();
            sut.OnPost();

            // assert
            mockCourse.Verify(c => c.StartNewSession());
        }

        [Fact]
        public void Post_RedirectsToSamePage()
        {
            // arrange
            var dummyCourse = new Mock<ITrainingCourse>();
            var sut = new ResultsModel(dummyCourse.Object);

            // act
            sut.OnGet();
            var result = sut.OnPost();

            // assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/Results", (result as RedirectToPageResult).PageName);
        }

        [Fact]
        public void Post_AddsWrongAnswers_AsNeedsMoreLearning()
        {
            // arrange
            var mults = new Multiplication[]
            {
                new Multiplication{BaseNumber=2, Multiplier=3},
                new Multiplication{BaseNumber=4, Multiplier=5}, // let's have this as wrongly answered challenge!
                new Multiplication{BaseNumber=6, Multiplier=7}  // let's have this as wrongly answered challenge!
            };
            var mockCourse = new Mock<ITrainingCourse>();
            mockCourse
                .Setup(c => c.AlreadySeenChallenges)
                .Returns(mults);
            var sut = new ResultsModel(mockCourse.Object);

            // act
            sut.OnGet();
            sut.ResultEvaluations[0].IsCorrect = true;
            sut.ResultEvaluations[1].IsCorrect = false;
            sut.ResultEvaluations[2].IsCorrect = false;
            var result = sut.OnPost();

            // assert
            mockCourse.Verify(c => c.SetNeedsMoreLearning(new Multiplication { BaseNumber = 4, Multiplier = 5 }));
            mockCourse.Verify(c => c.SetNeedsMoreLearning(new Multiplication { BaseNumber = 6, Multiplier = 7 }));
        }

        [Fact]
        public void OnFinished_RedirectsToNewSession()
        {
            // arrange
            var dummyCourse = new Mock<ITrainingCourse>();
            var sut = new ResultsModel(dummyCourse.Object);

            // act
            sut.OnGet();
            sut.OnPost();
            var result = sut.OnPostNewSession();

            // assert
            Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("/Index", (result as RedirectToPageResult)?.PageName);
        }
    }
}
