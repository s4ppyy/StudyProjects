using Backups.Entities;

namespace Backups.Repositories;

public class RepositoryBackupObjectsOnLocal : IRepositoryBackupObjects
{
    private string _path;

    public RepositoryBackupObjectsOnLocal(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException();
        _path = path;
    }

    public string GetPath()
    {
        return _path;
    }

    public IReadOnlyList<BackupObject> GetAll()
    {
        var listFiles = Directory.GetFiles(_path).ToList();
        List<BackupObject> backupObjects = new List<BackupObject>();
        foreach (var filePath in listFiles)
        {
            var fileName = Path.GetFileName(filePath);
            BackupObject obj = new BackupObject(fileName, filePath);
            backupObjects.Add(obj);
        }

        return backupObjects;
    }

    public IReadOnlyList<BackupObject> GetByName(List<string> names)
    {
        if (!names.Any())
            throw new ArgumentNullException();
        var objToReturn = new List<BackupObject>();
        foreach (string name in names)
        {
            if (File.Exists(_path + name))
            {
                BackupObject obj;
                obj = new BackupObject(name, _path + name);
                objToReturn.Add(obj);
            }
        }

        return objToReturn;
    }

    public void AddToRepository(BackupObject backupObject)
    {
        if (backupObject == null)
            throw new ArgumentNullException();
        var pathStirng = Path.Combine(_path, backupObject.FileName);
        File.Create(pathStirng);
    }

    public void DeleteFromRepository(BackupObject backupObject)
    {
        if (backupObject == null)
            throw new ArgumentNullException();
        File.Delete(backupObject.FilePath);
    }
}