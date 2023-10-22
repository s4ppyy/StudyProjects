using ClassLibrary1.BusinessLayer;
using ClassLibrary1.PresentationLayer;

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
messageProcessingSystem.MakeReport();
messageProcessingSystem.SerializeSystem();
messageProcessingSystem.DeserializeSystem();

UI_Console console = new UI_Console("manager", "qwerty", "Igor", "Muha");
