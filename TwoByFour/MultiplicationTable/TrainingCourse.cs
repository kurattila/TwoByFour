
using System.Collections.Generic;
using System.Linq;

namespace TwoByFour.MultiplicationTable
{
    public interface ITrainingCourse
    {
        void StartNewSession();
        void SetAlreadySeen(Multiplication challenge);
        void SetNeedsMoreLearning(Multiplication challenge);

        Multiplication[] AlreadySeenChallenges { get; }
        Multiplication[] NeedMoreLearningChallenges { get; }
    }

    public class TrainingCourse : ITrainingCourse
    {
        private HashSet<Multiplication> _alreadySeenChallenges = new HashSet<Multiplication>();
        private HashSet<Multiplication> _needMoreLearningChallenges = new HashSet<Multiplication>();

        public Multiplication[] AlreadySeenChallenges => _alreadySeenChallenges.ToArray();
        public Multiplication[] NeedMoreLearningChallenges => _needMoreLearningChallenges.ToArray();

        public void SetAlreadySeen(Multiplication mult)
        {
            _alreadySeenChallenges.Add(mult);
        }

        public void SetNeedsMoreLearning(Multiplication mult)
        {
            _needMoreLearningChallenges.Add(mult);
        }

        public void StartNewSession()
        {
            _alreadySeenChallenges.Clear();
        }
    }
}
