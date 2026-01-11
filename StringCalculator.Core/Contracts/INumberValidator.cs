namespace StringCalculator.Core.Contracts
{
    public interface INumberValidator
    {
        void Validate(int number, List<int> negativesCollector);
    }
}
