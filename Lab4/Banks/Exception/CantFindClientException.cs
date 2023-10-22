using Banks.Entities;

namespace Banks.Exception;

public class CantFindClientException : System.Exception
{
    public CantFindClientException(Client client)
    {
        Client = client;
    }

    public Client Client { get; }
}