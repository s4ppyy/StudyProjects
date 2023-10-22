using Backups.Entities;

namespace Backups.Repositories;

public interface IRepositoryBackupObjects
{
    IReadOnlyList<BackupObject> GetAll();
    IReadOnlyList<BackupObject> GetByName(List<string> names);

    void AddToRepository(BackupObject backupObject);

    void DeleteFromRepository(BackupObject backupObject);
}