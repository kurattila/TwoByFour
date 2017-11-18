
using System.Linq;
using TwoByFour.Utils;

namespace TwoByFour.MultiplicationTable
{
    public interface IMultiplicationSmartFactory
    {
        Multiplication CreateMultiplication();
    }

    public class MultiplicationSmartFactory : IMultiplicationSmartFactory
    {
        IMultiplicationRandomGenerator _randomGenerator;
        ITrainingCourse _course;
        IRandomWrapper _randomWrapper;

        public MultiplicationSmartFactory(IMultiplicationRandomGenerator randomGenerator, ITrainingCourse course, IRandomWrapper randomGeneratorWrapper)
        {
            _randomGenerator = randomGenerator;
            _course = course;
            _randomWrapper = randomGeneratorWrapper;
        }

        public Multiplication CreateMultiplication()
        {
            Multiplication uniqueMult = null;

            while (true)
            {
                var letUsRepeatNow = _randomWrapper.Next(0, 1 + 1) == 1;
                if (letUsRepeatNow && _course.NeedMoreLearningChallenges.Any())
                {
                    var whichOneToRepeat = _randomWrapper.Next(0, _course.NeedMoreLearningChallenges.Length - 1 + 1);
                    uniqueMult = _course.NeedMoreLearningChallenges[whichOneToRepeat];
                }
                else
                {
                    uniqueMult = _randomGenerator.Generate();
                }
                if (!_course.AlreadySeenChallenges.ToList().Contains(uniqueMult))
                    break;
            }

            _course.SetAlreadySeen(uniqueMult);
            return uniqueMult;
        }
    }
}
