
namespace TwoByFour.MultiplicationTable
{
    public class Multiplication
    {
        public int BaseNumber { get; set; }
        public int Multiplier { get; set; }
        public string TextualChallenge => $"{BaseNumber} x {Multiplier} = ?";
        public string TextualResult    => $"{BaseNumber} x {Multiplier} = {BaseNumber*Multiplier}";

        public override bool Equals(object other)
        {
            var otherMultiplication = other as Multiplication;
            return BaseNumber == otherMultiplication?.BaseNumber && Multiplier == otherMultiplication?.Multiplier;
        }

        public override int GetHashCode()
        {
            return BaseNumber << 16 + Multiplier;
        }
    }
}
