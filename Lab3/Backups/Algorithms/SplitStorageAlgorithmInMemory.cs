using Backups.Entities;
using Backups.Repositories;

namespace Backups.Algorithms;
using System.IO.Compression;
using System.IO;

public class SplitStorageAlgorithmInMemory : IAlgorithm
{
    private int counter = 0;
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
        foreach (BackupObject obj in backupObjects)
        {
            ZipArchiveInMemory zipArchiveInMemory =
                new ZipArchiveInMemory(newRestorePoint.Path + "RestorePoint_" + counter);
            Storage newStorage = new Storage(obj.FileName, zipArchiveInMemory.PathToArchive + "/" + obj.FileName);
            zipArchiveInMemory.AddToArchive(newStorage);
            newRestorePoint.AddArchive(zipArchiveInMemory);
            counter++;
        }

        return newRestorePoint;
    }
}