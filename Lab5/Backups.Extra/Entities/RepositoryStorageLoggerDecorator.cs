using Backups.Entities;
using Backups.Repositories;

namespace Backups.Extra.Entities;

public class RepositoryStorageLoggerDecorator : IRepositoryStorage, ILogger
{
    private IRepositoryStorage _repositoryStorage;
    private ILogger _logger;

    public RepositoryStorageLoggerDecorator(IRepositoryStorage repositoryStorage, ILogger logger)
    {
        _repositoryStorage = repositoryStorage ?? throw new ArgumentNullException();
        _logger = logger ?? throw new ArgumentNullException();
        PathToRepository = repositoryStorage.PathToRepository;
    }

    public string PathToRepository { get; }

    public bool TimePrefix => _logger.TimePrefix;

    public List<Storage> GetAll()
    {
        return _repositoryStorage.GetAll();
    }

    public List<Storage> GetByName(List<string> names)
    {
        if (!names.Any())
            throw new ArgumentNullException();
        return _repositoryStorage.GetByName(names);
    }

    public void AddZipArchive(ZipArchiveInMemory zipArchiveInMemory)
    {
        if (zipArchiveInMemory == null)
            throw new ArgumentNullException();
        _repositoryStorage.AddZipArchive(zipArchiveInMemory);
        var tmpStorages = zipArchiveInMemory.Storages;
        if (TimePrefix)
        {
            _logger.SendMessge($"New restore point added at {DateTime.Now}. Files in this point:");
        }
        else
        {
            _logger.SendMessge("New restore point added. Files in this point:");
        }

        foreach (Storage tmpStorage in tmpStorages)
        {
            _logger.SendMessge(tmpStorage.FileName);
        }
    }

    public void SendMessge(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException();
        _logger.SendMessge(message);
    }
}