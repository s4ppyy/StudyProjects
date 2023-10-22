namespace Banks.Entities;

public class DepositConfiguration
{
    public DepositConfiguration(decimal upperLimit, int percent)
    {
        if (upperLimit == 0)
            throw new ArgumentNullException();
        if (percent == 0)
            throw new ArgumentNullException();
        UpperLimit = upperLimit;
        Percent = percent;
    }

    public decimal UpperLimit { get; }

    public int Percent { get; }
}