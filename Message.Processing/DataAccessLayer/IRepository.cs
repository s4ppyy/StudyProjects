namespace ClassLibrary1.DataAccessLayer;

public interface IRepository<T>
{
    void Add(T toAdd);

    void Delete(int id);

    T GetById(int id);

    List<T> GetAll();

    void Update(T toUpdate);

    void Serialize(string path);

    void Deserialize(string path);
}