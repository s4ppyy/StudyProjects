namespace ClassLibrary1.Exceptions;

public class NoSuchEmployeeException : Exception
{
    public NoSuchEmployeeException(int id)
    {
        Id = id;
    }

    public NoSuchEmployeeException(string login)
    {
        Login = login;
    }

    public int? Id { get; }

    public string? Login { get; }
}