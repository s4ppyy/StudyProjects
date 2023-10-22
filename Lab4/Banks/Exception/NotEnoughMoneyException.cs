namespace Banks.Exception;

public class NotEnoughMoneyException : System.Exception
{
    public NotEnoughMoneyException(decimal money)
    {
        Money = money;
    }

    public decimal Money { get; }
}