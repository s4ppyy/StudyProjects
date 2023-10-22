using System.IO.Compression;
using Backups.Entities;
using Backups.Repositories;

namespace Backups.Algorithms;

public class SingleStorageAlgorithmOnLocal : IAlgorithm
{
    public RestorePoint CreateZipArchive(
        IReadOnlyList<BackupObject> backupObjects,
        IRepositoryStorage repositoryStorage,
        DateTime dateTime,
        int version)
    {
        if (!backupObjects.Any())
            throw new ArgumentNullException();
        if (repositoryStorage == null)
            throw new ArgumentNullException();
        RestorePoint newRestorePoint = new RestorePoint(repositoryStorage, dateTime, version, repositoryStorage.PathToRepository);
        ZipFile.CreateFromDirectory(backupObjects[0].FilePath + "/" + backupObjects[0].FileName, repositoryStorage.PathToRepository + ".zip");
        return newRestorePoint;
    }
}
