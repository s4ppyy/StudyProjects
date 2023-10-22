using Backups.Algorithms;
using Backups.Repositories;

namespace Backups.Entities;

public class BackupConfigurtion
{
    public BackupConfigurtion(
        IRepositoryBackupObjects repositoryBackupObjects,
        IRepositoryStorage repositoryStorage,
        IAlgorithm algorithm)
    {
        if (repositoryBackupObjects == null)
            throw new ArgumentNullException();
        if (repositoryStorage == null)
            throw new ArgumentNullException();
        if (algorithm == null)
            throw new ArgumentNullException();
        RepositoryBackupObjects = repositoryBackupObjects;
        RepositoryStorage = repositoryStorage;
        Algorithm = algorithm;
    }

    public IRepositoryBackupObjects RepositoryBackupObjects { get; }
    public IRepositoryStorage RepositoryStorage { get; }
    public IAlgorithm Algorithm { get; }
}