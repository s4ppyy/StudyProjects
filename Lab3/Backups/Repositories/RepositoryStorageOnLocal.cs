using Backups.Entities;

namespace Backups.Repositories;

public class RepositoryStorageOnLocal : IRepositoryStorage
{
    public RepositoryStorageOnLocal(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException();
        PathToRepository = path;
    }

    public string PathToRepository { get; }

    public List<Storage> GetAll()
    {
        var listStorages = Directory.GetFiles(PathToRepository).ToList();
        List<Storage> storages = new List<Storage>();
        foreach (var filePath in listStorages)
        {
            var fileName = Path.GetFileName(filePath);
            Storage obj = new Storage(fileName, filePath);
            storages.Add(obj);
        }

        return storages;
    }

    public List<Storage> GetByName(List<string> names)
    {
        if (!names.Any())
            throw new ArgumentNullException();
        var objToReturn = new List<Storage>();
        foreach (string name in names)
        {
            if (File.Exists(PathToRepository + name))
            {
                Storage obj;
                obj = new Storage(name, PathToRepository + name);
                objToReturn.Add(obj);
            }
        }

        return objToReturn;
    }

    public void AddZipArchive(ZipArchiveInMemory zipArchiveInMemory)
    {
        Directory.CreateDirectory(zipArchiveInMemory.PathToArchive);
    }
}