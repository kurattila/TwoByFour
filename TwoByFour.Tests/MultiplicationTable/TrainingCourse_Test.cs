
using TwoByFour.MultiplicationTable;
using Xunit;

namespace TwoByFour.Tests.MultiplicationTable
{
    public class TrainingCourse_Test
    {
        [Fact]
        public void AlreadySeenChallenges_Added()
        {
            // arrange
            var sut = new TrainingCourse();

            // act
            sut.SetAlreadySeen(new Multiplication { BaseNumber = 2, Multiplier = 3 });
            sut.SetAlreadySeen(new Multiplication { BaseNumber = 4, Multiplier = 5 });

            // assert
            Assert.Equal(2, sut.AlreadySeenChallenges.Length);
            Assert.Equal(new Multiplication { BaseNumber = 2, Multiplier = 3 }, sut.AlreadySeenChallenges[0]);
            Assert.Equal(new Multiplication { BaseNumber = 4, Multiplier = 5 }, sut.AlreadySeenChallenges[1]);
        }

        [Fact]
        public void NewSession_Clears_AlreadySeenChallenges()
        {
            // arrange
            var sut = new TrainingCourse();
            sut.SetAlreadySeen(new Multiplication { BaseNumber = 2, Multiplier = 3 });

            // act
            sut.StartNewSession();

            // assert
            Assert.Empty(sut.AlreadySeenChallenges);
        }

        [Fact]
        public void NeedsMoreLearningChallenges_Added()
        {
            // arrange
            var sut = new TrainingCourse();

            // act
            sut.SetNeedsMoreLearning(new Multiplication { BaseNumber = 4, Multiplier = 5 });

            // assert
            Assert.Single(sut.NeedMoreLearningChallenges);
        }

        [Fact]
        public void NewSession_WontClear_NeedsMoreLearningChallenges()
        {
            // arrange
            var sut = new TrainingCourse();
            sut.SetNeedsMoreLearning(new Multiplication { BaseNumber = 4, Multiplier = 5 });

            // act
            sut.StartNewSession();

            // assert
            Assert.Single(sut.NeedMoreLearningChallenges);
        }
    }
}
