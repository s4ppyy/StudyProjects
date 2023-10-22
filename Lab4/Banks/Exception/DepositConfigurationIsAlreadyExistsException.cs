using Banks.Entities;

namespace Banks.Exception;

public class DepositConfigurationIsAlreadyExistsException : System.Exception
{
    public DepositConfigurationIsAlreadyExistsException(decimal upperLimit)
    {
        UpperLimit = upperLimit;
    }

    public decimal UpperLimit { get; }
}