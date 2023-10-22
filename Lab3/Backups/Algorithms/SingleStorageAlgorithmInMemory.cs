using Backups.Entities;
using Backups.Repositories;

namespace Backups.Algorithms;

public class SingleStorageAlgorithmInMemory : IAlgorithm
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
        ZipArchiveInMemory zipArchiveInMemory =
                        new ZipArchiveInMemory(newRestorePoint.Path + "_" + newRestorePoint.Version + ".zip");
        foreach (BackupObject obj in backupObjects)
        {
            Storage newStorage = new Storage(obj.FileName, zipArchiveInMemory.PathToArchive + "/" + obj.FileName);
            zipArchiveInMemory.AddToArchive(newStorage);
        }

        newRestorePoint.AddArchive(zipArchiveInMemory);
        return newRestorePoint;
    }
}