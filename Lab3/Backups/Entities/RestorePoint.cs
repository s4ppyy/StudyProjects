using Backups.Repositories;

namespace Backups.Entities;

public class RestorePoint
{
    public RestorePoint(IRepositoryStorage repositoryStorage, DateTime dateTime, int version, string path)
    {
        if (repositoryStorage == null)
            throw new ArgumentNullException();
        RepositoryStorage = repositoryStorage;
        if (dateTime == default)
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException();
        TimeOFCreating = dateTime;
        Version = version;
        Path = path;
    }

    public string Path { get; }
    public IRepositoryStorage RepositoryStorage { get; }
    public DateTime TimeOFCreating { get; }

    public int Version { get; }

    public void AddArchive(ZipArchiveInMemory zipArchive)
    {
        if (zipArchive == null)
            throw new ArgumentNullException();
        RepositoryStorage.AddZipArchive(zipArchive);
    }
}