namespace Banks.Entities;

public class ClientBuilder
{
    private Client newClient = null!;
    private int idCounter = 0;

    public string? Name { get; private set; }

    public string? Surname { get; private set; }

    public string? Address { get; private set; }

    public int PassportID { get; private set; }
    public void AddName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException();
        Name = name;
    }

    public void AddSurname(string surname)
    {
        if (string.IsNullOrWhiteSpace(surname))
            throw new ArgumentNullException();
        Surname = surname;
    }

    public void AddAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException();
        Address = address;
    }

    public void AddPassportID(int id)
    {
        if (id <= 0)
            throw new ArgumentNullException();
        PassportID = id;
    }

    public Client Build()
    {
        newClient = new Client(idCounter);
        if (!string.IsNullOrWhiteSpace(Name))
            newClient.SetName(Name);
        if (!string.IsNullOrWhiteSpace(Surname))
            newClient.SetSurname(Surname);
        if (PassportID != 0)
            newClient.SetPassportID(PassportID);
        if (!string.IsNullOrWhiteSpace(Address))
            newClient.SetAddress(Address);
        return newClient;
    }

    public void Reset()
    {
        idCounter++;
        newClient = new Client(idCounter);
    }
}