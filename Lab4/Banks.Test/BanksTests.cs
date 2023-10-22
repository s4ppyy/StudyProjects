using Banks.Entities;
using Banks.Exception;
using Xunit;
namespace Banks.Test;

public class BanksTests
{
    [Fact]
    public void SignClientToDebitAndSubscribeToNotifications_ClientAddedAndSubscribed()
    {
        Centrobank centrobank = Centrobank.GetCentrobank();
        var bank1 = centrobank.AddBank("Sber", "Lenina 1");
        ClientBuilder clientBuilder = new ClientBuilder();
        clientBuilder.AddName("Ivan");
        clientBuilder.AddSurname("Sergeev");
        clientBuilder.AddPassportID(338857);
        clientBuilder.AddAddress("Pushkina 7");
        var client1 = clientBuilder.Build();
        bank1.AddObserver(client1);
        bank1.SetDepositPercent(2);
        bank1.SetUnapproveTransferLimit(100);
        var debit1 = bank1.AddClientToDebit(client1);
        Assert.True(client1.BankAccounts.Count == 1);
        Assert.True(client1.Notifications.Count == 2);
    }

    [Fact]
    public void TimeShifted_DebitIncomed()
    {
        Centrobank centrobank = Centrobank.GetCentrobank();
        var bank1 = centrobank.AddBank("Sber", "Lenina 1");
        ClientBuilder clientBuilder = new ClientBuilder();
        clientBuilder.AddName("Ivan");
        clientBuilder.AddSurname("Sergeev");
        clientBuilder.AddPassportID(338857);
        clientBuilder.AddAddress("Pushkina 7");
        var client1 = clientBuilder.Build();
        bank1.AddObserver(client1);
        bank1.SetDepositPercent(2);
        bank1.SetUnapproveTransferLimit(100);
        var debit1 = bank1.AddClientToDebit(client1);
        debit1.UpBalance(100);
        centrobank.TimeShift(31);
        Assert.True(debit1.Balance == 162);
    }

    [Fact]
    public void TimeShifted_DepositIncomed()
    {
        Centrobank centrobank = Centrobank.GetCentrobank();
        var bank1 = centrobank.AddBank("Sber", "Lenina 1");
        ClientBuilder clientBuilder = new ClientBuilder();
        clientBuilder.AddName("Ivan");
        clientBuilder.AddSurname("Sergeev");
        clientBuilder.AddPassportID(338857);
        clientBuilder.AddAddress("Pushkina 7");
        var client1 = clientBuilder.Build();
        bank1.AddDepositConfiguration(500, 1);
        bank1.AddDepositConfiguration(1000, 2);
        bank1.AddDepositConfiguration(1500, 3);
        bank1.SetUnapproveTransferLimit(1000);
        bank1.SetDepositPercent(5);
        bank1.SetUnapproveTransferLimit(1000);
        bank1.SetDepositValidity(30);
        var deposit1 = bank1.AddClientToDeposit(client1, 1000);
        centrobank.TimeShift(30);
        Assert.True(deposit1.Balance == 1030);
        Assert.True(deposit1.OperationsUnlocked);
    }

    [Fact]
    public void CreditAdded_CreditOverflowed()
    {
        Centrobank centrobank = Centrobank.GetCentrobank();
        var bank1 = centrobank.AddBank("Sber", "Lenina 1");
        ClientBuilder clientBuilder = new ClientBuilder();
        clientBuilder.AddName("Ivan");
        clientBuilder.AddSurname("Sergeev");
        clientBuilder.AddPassportID(338857);
        clientBuilder.AddAddress("Pushkina 7");
        var client1 = clientBuilder.Build();
        bank1.SetUnapproveTransferLimit(1000);
        bank1.SetCreditComissionPercent(3);
        bank1.SetCreditLimit(50);
        var credit1 = bank1.AddClientToCredit(client1);
        Assert.Throws<CreditLimitOverflowed>(() => credit1.Withdraw(52));
    }
}