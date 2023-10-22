using Backups.Entities;
using Backups.Repositories;

namespace Backups.Services;

public class BackupTask
{
    private int versionCounter = 0;
    public BackupTask(BackupConfigurtion configurtion)
    {
        if (configurtion == null)
            throw new ArgumentNullException();
        BackupConfigurtion = configurtion;
        CurrentBackup = new Backup();
    }

    public BackupConfigurtion BackupConfigurtion { get; }

    public Backup CurrentBackup { get; private set; }

    public void AddBackupObject(BackupObject backupObject)
    {
        if (backupObject == null)
            throw new ArgumentNullException();
        BackupConfigurtion.RepositoryBackupObjects.AddToRepository(backupObject);
    }

    public void DeleteBackupObject(BackupObject backupObject)
    {
        BackupConfigurtion.RepositoryBackupObjects.DeleteFromRepository(backupObject);
    }

    public void Run()
    {
        var backupObjList = BackupConfigurtion.RepositoryBackupObjects.GetAll();
        var repStorage = BackupConfigurtion.RepositoryStorage;
        var newRestorePoint = BackupConfigurtion.Algorithm.CreateZipArchive(
            backupObjList, repStorage, DateTime.Now, versionCounter);
        CurrentBackup.AddRestorePoint(newRestorePoint);
        versionCounter++;
    }

    public IReadOnlyList<RestorePoint> FinishTask()
    {
        versionCounter = 0;
        return CurrentBackup.GetAllRestorePoints();
    }
}