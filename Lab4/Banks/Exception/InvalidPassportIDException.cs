namespace Banks.Exception;

public class InvalidPassportIDException : System.Exception
{
    public InvalidPassportIDException(int id)
    {
        ID = id;
    }

    public int ID { get; }
}