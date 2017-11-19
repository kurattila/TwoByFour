using TwoByFour.Utils;

namespace TwoByFour.MultiplicationTable
{
    public interface IMultiplicationRandomGenerator
    {
        void Init(int[] multiplicationBasesToUse);
        Multiplication Generate();
    }

    public class MultiplicationRandomGenerator : IMultiplicationRandomGenerator
    {
        IRandomWrapper _randomWrapper;
        int[] _multiplicationBasesToUse;

        public MultiplicationRandomGenerator(IRandomWrapper randomWrapper)
        {
            _randomWrapper = randomWrapper;
        }

        public void Init(int[] multiplicationBasesToUse)
        {
            _multiplicationBasesToUse = multiplicationBasesToUse;
        }

        public Multiplication Generate()
        {
            var baseNumberIndex = _randomWrapper.Next(0, _multiplicationBasesToUse.Length - 1 + 1);
            return new Multiplication
            {
                BaseNumber = _multiplicationBasesToUse[baseNumberIndex],
                Multiplier = _randomWrapper.Next(2, 10 + 1)
            };
        }
    }
}
