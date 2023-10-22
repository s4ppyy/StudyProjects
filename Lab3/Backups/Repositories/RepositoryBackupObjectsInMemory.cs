using Backups.Entities;
using Backups.Exceptions;

namespace Backups.Repositories;

public class RepositoryBackupObjectsInMemory : IRepositoryBackupObjects
{
    private List<BackupObject> _backupObjects;

    public RepositoryBackupObjectsInMemory()
    {
        _backupObjects = new List<BackupObject>();
    }

    public IReadOnlyList<BackupObject> GetAll()
    {
        return _backupObjects;
    }

    public IReadOnlyList<BackupObject> GetByName(List<string> names)
    {
        if (!names.Any())
            throw new ArgumentNullException();
        List<BackupObject> objects = new List<BackupObject>();
        foreach (string name in names)
        {
            var objToAdd = _backupObjects.Find(obj => obj.FileName == name);
            if (objToAdd != null)
                objects.Add(objToAdd);
        }

        return objects;
    }

    public void AddToRepository(BackupObject backupObject)
    {
        if (backupObject == null)
            throw new ArgumentNullException();
        _backupObjects.Add(backupObject);
    }

    public void DeleteFromRepository(BackupObject backupObject)
    {
        if (backupObject == null)
            throw new ArgumentNullException();
        _backupObjects.Remove(_backupObjects.Find(obj => obj.FilePath == backupObject.FilePath) ??
                              throw new CantFindBackupObjectException(backupObject));
    }
}