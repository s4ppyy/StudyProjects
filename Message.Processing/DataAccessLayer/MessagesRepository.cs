using ClassLibrary1.BusinessLayer;
using ClassLibrary1.Exceptions;
using Newtonsoft.Json;

namespace ClassLibrary1.DataAccessLayer;

public class MessagesRepository : IRepository<Message>
{
    private List<Message> _messages;

    public MessagesRepository()
    {
        _messages = new List<Message>();
    }

    public void Add(Message toAdd)
    {
        if (toAdd == null)
            throw new ArgumentNullException();
        _messages.Add(toAdd);
    }

    public void Delete(int id)
    {
        if (id < 0)
            throw new ArgumentNullException();
        _messages.Remove(
            _messages.Find(toFind => toFind.MessageId == id) ?? throw new NoSuchMessageException(id));
    }

    public Message GetById(int id)
    {
        if (id < 0)
            throw new ArgumentNullException();
        return _messages.Find(toFind => toFind.MessageId == id) ?? throw new NoSuchMessageException(id);
    }

    public List<Message> GetAll()
    {
        return _messages;
    }

    public void Update(Message toUpdate)
    {
        if (toUpdate == null)
            throw new ArgumentNullException();
        var updatingMessage = _messages.Find(toFind => toFind.MessageId == toUpdate.MessageId) ??
            throw new NoSuchMessageException(toUpdate.MessageId);
        updatingMessage = toUpdate;
    }

    public void Serialize(string path)
    {
        string jsonString = JsonConvert.SerializeObject(_messages, new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        });
        File.WriteAllText(path, jsonString);
    }

    public void Deserialize(string path)
    {
        string jsonStringMessagesInWork = File.ReadAllText(path);
        _messages = JsonConvert.DeserializeObject<List<Message>>(jsonStringMessagesInWork, new
            JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            })!;
    }

    public Message GetFirstMessage()
    {
        if (!_messages.Any())
            throw new NoMessageInWorkException(this);
        Message toReturn = _messages[0];
        foreach (Message message in _messages)
        {
            if (message.TimeOfCreation > toReturn.TimeOfCreation)
                toReturn = message;
        }

        return toReturn;
    }
}