using Banks.Exception;

namespace Banks.Entities;

public class ClientConsoleInterface
{
    private string[] uInterface =
     {
         "Press button to:\n",
         "1 - get bank account\n",
         "2 - get information about account\n",
         "3 - remove account\n",
         "4 - print notifications\n",
         "q to quit\n",
     };

    private string[] accounts =
     {
         "Press for:\n",
         "1 - debit card\n",
         "2 - deposit card\n",
         "3 - credit card\n",
         "q to quit\n",
     };
    public ClientConsoleInterface(Bank bank)
    {
        Bank = bank;
        Console.WriteLine("Creating client account^\n");
        Client = CreateClient();
    }

    public Bank Bank { get; private set; }

    public Client Client { get; private set; }

    public void Run()
    {
        PrintUI();
        char option = 's';
        while (option != 'q')
        {
            option = (char)Console.Read();
            switch (option)
            {
                case '1':
                    char account = 's';
                    while (account != 'q')
                    {
                        PrintAccounts();
                        account = (char)Console.Read();
                        if (account == 'q')
                            break;
                        MakeAccount(account);
                    }

                    break;
                case '2':
                    GetInfoAboutAccounts();
                    break;
                case '3':
                    Bank.RemoveClient(Client.BankAccounts.Last());
                    break;
                case '4':
                    PrintNotifications();
                    break;
                case 'q':
                    break;
            }
        }
    }

    private void PrintUI()
    {
        foreach (string s in uInterface)
        {
            Console.WriteLine(s);
        }
    }

    private void PrintAccounts()
    {
        foreach (string s in accounts)
        {
            Console.WriteLine(s);
        }
    }

    private void MakeAccount(char account)
    {
        switch (account)
        {
            case '1':
                Bank.AddClientToDebit(Client);
                break;
            case '2':
                Console.WriteLine("Enter start sum: ");
                decimal startSum = Console.Read();
                Bank.AddClientToDeposit(Client, startSum);
                break;
            case '3':
                Bank.AddClientToCredit(Client);
                break;
            case 'q':
                break;
        }
    }

    private void GetInfoAboutAccounts()
    {
        foreach (IBankAccount clientBankAccount in Client.BankAccounts)
        {
            Console.WriteLine($"Balance: {clientBankAccount.Balance}\n");
            Console.WriteLine($"Is Approved: {clientBankAccount.Approved}\n");
        }
    }

    private Client CreateClient()
    {
        ClientBuilder clientBuilder = new ClientBuilder();
        Console.WriteLine("\nEnter name: ");
        string name = Console.ReadLine() ?? throw new ArgumentNullException();
        clientBuilder.AddName(name);
        Console.WriteLine("\nEnter surname: ");
        string surname = Console.ReadLine() ?? throw new ArgumentNullException();
        clientBuilder.AddSurname(surname);
        Console.WriteLine("\nEnter passport ID: ");
        int id = int.Parse(Console.ReadLine() ?? string.Empty);
        if (id != 0)
            clientBuilder.AddPassportID(id);
        Console.WriteLine("\nEnterAddress ");
        string? address = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(address))
            clientBuilder.AddAddress(address);
        Client client1 = clientBuilder.Build();
        Console.WriteLine("Sign to notifications? y/any other button to quit ");
        char n = 'n';
        n = (char)Console.Read();
        switch (n)
        {
            case 'y':
                   Bank.AddObserver(client1);
                   break;
            default:
                   break;
        }

        return client1;
    }

    private void PrintNotifications()
    {
        foreach (string notification in Client.Notifications)
        {
            Console.WriteLine(notification);
            Console.WriteLine("\n");
        }
    }
}