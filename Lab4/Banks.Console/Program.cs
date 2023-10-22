using Banks;
using Banks.Entities;

Centrobank centrobank = Centrobank.GetCentrobank();
var bank1 = centrobank.AddBank("Sberbank", "Lenina 1");

ClientConsoleInterface ui = new ClientConsoleInterface(bank1);
bank1.SetDepositPercent(3);
bank1.SetUnapproveTransferLimit(500);
bank1.SetCreditLimit(500);
bank1.SetDepositValidity(31);
bank1.SetCreditComissionPercent(2);
bank1.SetDebitPercent(5);
ui.Run();
