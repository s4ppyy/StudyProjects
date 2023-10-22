using ClassLibrary1.BusinessLayer;
using ClassLibrary1.Exceptions;
using Newtonsoft.Json;

namespace ClassLibrary1.DataAccessLayer;

public class EmployeeRepository : IRepository<IEmployee>
{
    private List<IEmployee> _employees;

    public EmployeeRepository()
    {
        _employees = new List<IEmployee>();
    }

    public void Add(IEmployee toAdd)
    {
        if (toAdd == null)
            throw new ArgumentNullException();
        _employees.Add(toAdd);
    }

    public void Delete(int id)
    {
        if (id < 0)
            throw new ArgumentNullException();
        _employees.Remove(
            _employees.Find(toFind => toFind.Id == id) ?? throw new NoSuchEmployeeException(id));
    }

    public IEmployee GetById(int id)
    {
        if (id < 0)
            throw new ArgumentNullException();
        return _employees.Find(toFind => toFind.Id == id) ?? throw new NoSuchEmployeeException(id);
    }

    public List<IEmployee> GetAll()
    {
        return _employees;
    }

    public void Update(IEmployee toUpdate)
    {
        if (toUpdate == null)
            throw new ArgumentNullException();
        var updatingEmployee = _employees.Find(toFind => toFind.Id == toUpdate.Id) ??
                                throw new NoSuchEmployeeException(toUpdate.Id);
        updatingEmployee = toUpdate;
    }

    public IEmployee GetByLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
            throw new ArgumentNullException();
        return _employees.Find(toFind => toFind.Identification.Login == login) ??
               throw new NoSuchEmployeeException(login);
    }

    public void Serialize(string path)
    {
        string jsonString = JsonConvert.SerializeObject(_employees, new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
        });
        File.WriteAllText(path, jsonString);
    }

    public void Deserialize(string path)
    {
        string jsonStringEmployees = File.ReadAllText(path);
        _employees = JsonConvert.DeserializeObject<List<IEmployee>>(jsonStringEmployees, new
            JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            })!;
    }
}