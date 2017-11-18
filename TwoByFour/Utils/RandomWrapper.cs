using System;

namespace TwoByFour.Utils
{
    public interface IRandomWrapper
    {
        int Next(int minValue, int maxValue);
    }

    public class RandomWrapper : IRandomWrapper
    {
        Random _random = new Random();

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
