namespace ClassLibrary1.BusinessLayer;

public class Identification
{
    private string _password;
    public Identification(string login, string password, int id)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentNullException();
        Login = login;
        _password = password;
        if (id < 0)
            throw new ArgumentNullException();
        Id = id;
    }

    public string Login { get; }

    public int Id { get; }

    public bool Authentication(string password)
    {
        if (password == _password)
            return true;
        return false;
    }

    public void ChangePassword(string oldPassword, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(oldPassword))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentNullException();
        if (_password == oldPassword)
            _password = newPassword;
    }
}
