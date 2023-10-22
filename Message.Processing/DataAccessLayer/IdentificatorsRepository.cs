using ClassLibrary1.BusinessLayer;
using ClassLibrary1.Exceptions;
using Newtonsoft.Json;

namespace ClassLibrary1.DataAccessLayer;

public class IdentificatorsRepository : IRepository<Identification>
{
    private List<Identification> _identifications;

    public IdentificatorsRepository()
    {
        _identifications = new List<Identification>();
    }

    public void Add(Identification toAdd)
    {
        if (toAdd == null)
            throw new ArgumentNullException();
        _identifications.Add(toAdd);
    }

    public void Delete(int id)
    {
        if (id < 0)
            throw new ArgumentNullException();
        _identifications.Remove(
            _identifications.Find(toFind => toFind.Id == id) ?? throw new NoSuchIdentificatorException(id));
    }

    public Identification GetById(int id)
    {
        if (id < 0)
            throw new ArgumentNullException();
        return _identifications.Find(toFind => toFind.Id == id) ?? throw new NoSuchIdentificatorException(id);
    }

    public List<Identification> GetAll()
    {
        return _identifications;
    }

    public void Update(Identification toUpdate)
    {
        if (toUpdate == null)
            throw new ArgumentNullException();
        var updatingIdentification = _identifications.Find(toFind => toFind.Id == toUpdate.Id) ??
                               throw new NoSuchIdentificatorException(toUpdate.Id);
        updatingIdentification = toUpdate;
    }

    public Identification GetByLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentNullException();
        return _identifications.Find(toFind => toFind.Login == login) ??
               throw new NoSuchEmployeeException(login);
    }

    public void Serialize(string path)
    {
        string jsonString = JsonConvert.SerializeObject(_identifications, new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        });
        File.WriteAllText(path, jsonString);
    }

    public void Deserialize(string path)
    {
        string jsonStringIdentificators = File.ReadAllText(path);
        _identifications = JsonConvert.DeserializeObject<List<Identification>>(jsonStringIdentificators, new
            JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            })!;
    }
}