using Backups.Entities;
using Backups.Repositories;

namespace Backups.Algorithms;
using System.IO.Compression;

public interface IAlgorithm
{
    RestorePoint CreateZipArchive(
        IReadOnlyList<BackupObject> backupObjects,
        IRepositoryStorage repositoryStorage,
        DateTime dateTime,
        int version);
}