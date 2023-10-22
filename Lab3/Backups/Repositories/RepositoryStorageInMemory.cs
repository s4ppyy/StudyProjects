using System.Text;
using Backups.Entities;
using Backups.Exceptions;

namespace Backups.Repositories;

public class RepositoryStorageInMemory : IRepositoryStorage
{
    private List<ZipArchiveInMemory> _repository;

    public RepositoryStorageInMemory(string pathToRepository)
    {
        if (string.IsNullOrWhiteSpace(pathToRepository))
            throw new ArgumentNullException();
        _repository = new List<ZipArchiveInMemory>();
        PathToRepository = pathToRepository;
    }

    public string PathToRepository { get; }

    public List<Storage> GetAll()
    {
        List<Storage> storagesToReturn = new List<Storage>();
        foreach (ZipArchiveInMemory archiveInMemory in _repository)
        {
            foreach (Storage storage in archiveInMemory.Storages)
            {
                storagesToReturn.Add(storage);
            }
        }

        return storagesToReturn;
    }

    public List<Storage> GetByName(List<string> names)
    {
        if (!names.Any())
            throw new ArgumentNullException();
        List<Storage> storagesToReturn = new List<Storage>();
        foreach (string name in names)
        {
            foreach (ZipArchiveInMemory archiveInMemory in _repository)
            {
                var storToAdd = archiveInMemory.Storages.Find(storage => storage.FileName == name);
                if (storToAdd != null)
                    storagesToReturn.Add(storToAdd);
            }
        }

        return storagesToReturn;
    }

    public void AddZipArchive(ZipArchiveInMemory zipArchiveInMemory)
    {
        if (zipArchiveInMemory == null)
            throw new ArgumentNullException();
        _repository.Add(zipArchiveInMemory);
    }

    public int GetSize()
    {
        return _repository.Count;
    }
}