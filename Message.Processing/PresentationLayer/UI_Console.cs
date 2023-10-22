using ClassLibrary1.BusinessLayer;

namespace ClassLibrary1.PresentationLayer;

public class UI_Console
{
    private MessageProcessingSystem _messageProcessingSystem;

    private string[] uInterface =
     {
         "Press button to:\n",
         "1 - Send message\n",
         "2 - Log In\n",
         "3 - Answer message\n",
         "4 - Make Report\n",
         "5 - Hire new workers\n",
         "6 - Serialize system\n",
         "7 - Deserialize system\n",
         "8 - Log Out\n",
         "q to quit\n",
     };

    public UI_Console(string login, string password, string name, string surname)
    {
        _messageProcessingSystem = new MessageProcessingSystem(new Identification(login, password, 0), name, surname);
        Run();
    }

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
                    Console.WriteLine("\nWrite your message\n");
                    string message = Console.ReadLine()!;
                    _messageProcessingSystem.SendMessage(MessageSource.Messenger, message);
                    break;

                case '2':
                    Console.WriteLine("\nWrite your login and password\n");
                    string login = Console.ReadLine()!;
                    string password = Console.ReadLine()!;
                    if (_messageProcessingSystem.Authentication(login, password))
                    {
                        Console.WriteLine("\nYou successfully logged in\n");
                        break;
                    }

                    Console.WriteLine("\nWrong login or password\n");
                    break;

                case '3':
                    Console.WriteLine("\nWrite answer to message\n");
                    string answer = Console.ReadLine()!;
                    if (_messageProcessingSystem.AnswerMessage(answer))
                    {
                        Console.WriteLine("\nAnswer is successfully delivered\n");
                        break;
                    }

                    Console.WriteLine("\nYou cant answer on message with your role\n");
                    break;
                case '4':
                    if (_messageProcessingSystem.MakeReport())
                    {
                        Console.WriteLine("\nReport successfully made by path C:/Users/Danee/OneDrive/Рабочий стол/C#/Reports/\n");
                        break;
                    }

                    Console.WriteLine("\nYou cant make report with your role\n");
                    break;

                case '5':
                    Console.WriteLine("\nWrite login, password, name and surname of new employee\n");
                    string loginE = Console.ReadLine()!;
                    string passwordE = Console.ReadLine()!;
                    string nameE = Console.ReadLine()!;
                    string surnameE = Console.ReadLine()!;
                    if (_messageProcessingSystem.HireEmployee(loginE, passwordE, nameE, surnameE))
                    {
                        Console.WriteLine("\nWorker successfully hired\n");
                        break;
                    }

                    Console.WriteLine("\nYou cant hire new workers with your role\n");
                    break;

                case '6':
                    if (_messageProcessingSystem.SerializeSystem())
                    {
                        Console.WriteLine("\nSystem is successfully serialized\n");
                        break;
                    }

                    Console.WriteLine("\nYou cant serialize system with your role\n");
                    break;

                case '7':
                    if (_messageProcessingSystem.DeserializeSystem())
                    {
                        Console.WriteLine("\nSystem is successfully deserialized\n");
                        break;
                    }

                    Console.WriteLine("\nYou cant deserialize system with your role\n");
                    break;

                case '8':
                    _messageProcessingSystem.LogOut();
                    Console.WriteLine("\nYou successfully logged out\n");
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
}