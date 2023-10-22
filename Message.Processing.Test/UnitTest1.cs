using ClassLibrary1.BusinessLayer;
using ClassLibrary1.Exceptions;

namespace Message.Processing.Test;
using Xunit;

public class UnitTest1
{
    [Fact]
    public void AddNewEmployee_EmployeeAdded()
    {
        MessageProcessingSystem messageProcessingSystem = new
            MessageProcessingSystem(new Identification("manager", "qwerty", 0), "Igor", "Muha");
        messageProcessingSystem.SendMessage(MessageSource.Email, "testing...");
        messageProcessingSystem.Authentication("manager", "qwerty");
        messageProcessingSystem.HireEmployee("handler", "yoyo", "Misha", "Jackson");
        Assert.Single(messageProcessingSystem.GetAllEmployeesOfCurrentWorker());
    }

    [Fact]
    public void AddNewMessageAndHandleIt_MessageAddedAndHandled()
    {
        MessageProcessingSystem messageProcessingSystem = new 
            MessageProcessingSystem(new Identification("manager", "qwerty", 0), "Igor", "Muha");
        messageProcessingSystem.SendMessage(MessageSource.Email, "testing...");
        messageProcessingSystem.Authentication("manager", "qwerty");
        messageProcessingSystem.HireEmployee("handler", "yoyo", "Misha", "Jackson");
        messageProcessingSystem.LogOut();
        messageProcessingSystem.Authentication("handler", "yoyo");
        messageProcessingSystem.AnswerMessage("Success!");
        messageProcessingSystem.LogOut();
        messageProcessingSystem.Authentication("manager", "qwerty");
        Assert.Single(messageProcessingSystem.GetHandledMessages());
    }

    [Fact] public void TryingToUseMethodWithWrongAccessLevel_ExceptionWasThrown()
    {
        MessageProcessingSystem messageProcessingSystem = new 
            MessageProcessingSystem(new Identification("manager", "qwerty", 0), "Igor", "Muha");
        messageProcessingSystem.SendMessage(MessageSource.Email, "testing...");
        messageProcessingSystem.Authentication("manager", "qwerty");
        messageProcessingSystem.HireEmployee("handler", "yoyo", "Misha", "Jackson");
        messageProcessingSystem.LogOut();
        messageProcessingSystem.Authentication("handler", "yoyo");
        messageProcessingSystem.AnswerMessage("Success!");
        messageProcessingSystem.LogOut();
        Assert.Throws<AccessDeniedException>(() => messageProcessingSystem.MakeReport());
    }
}