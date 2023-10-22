using ClassLibrary1.DataAccessLayer;

namespace ClassLibrary1.BusinessLayer;

public class Message
{
    public Message(MessageSource messageSource, MessageStatus messageStatus, string message, int messageId)
    {
        MessageSource = messageSource;
        MessageStatus = messageStatus;
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException();
        MessageText = message;
        TimeOfCreation = DateTime.Today;
        if (messageId < 0)
            throw new ArgumentNullException();
        MessageId = messageId;
    }

    public MessageSource MessageSource { get; }

    public MessageStatus MessageStatus { get; private set; }

    public string MessageText { get; }

    public string? Answer { get; private set; }

    public DateTime TimeOfCreation { get; }

    public DateTime? TimeOfClosing { get; private set; }

    public int MessageId { get; }

    public void AddToAnswer(string textToAdd)
    {
        if (string.IsNullOrWhiteSpace(textToAdd))
            throw new ArgumentNullException();
        MessageStatus = MessageStatus.UnhandInSystem;
        Answer = Answer + " " + textToAdd;
    }

    public void DeleteAnswer()
    {
        Answer = string.Empty;
        MessageStatus = MessageStatus.NewMessage;
    }

    public void CloseMessage()
    {
        MessageStatus = MessageStatus.Handled;
        TimeOfClosing = DateTime.Today;
    }
}