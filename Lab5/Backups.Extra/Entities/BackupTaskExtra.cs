using Backups.Entities;
using Backups.Extra.CleaningAlgorithms;
using Backups.Extra.Exceptions;
using Backups.Extra.Test;
using Backups.Services;

namespace Backups.Extra.Entities;

public class BackupTaskExtra : BackupTask
{
    public BackupTaskExtra(BackupConfigurtion configurtion, SavedConfig savedConfig, ICleaningAlgorithm cleaningAlgorithm)
        : base(configurtion)
    {
        SavedConfig = savedConfig ?? throw new ArgumentNullException();
        CleaningAlgorithm = cleaningAlgorithm ?? throw new ArgumentNullException();
    }

    public SavedConfig SavedConfig { get; }

    public ICleaningAlgorithm CleaningAlgorithm { get; }

    public RestorePoint RestoreLastVersion()
    {
        var allPoints = CurrentBackup.GetAllRestorePoints();
        if (!allPoints.Any())
            throw new NoRestorePointException(this);
        RestorePoint lastRestorePoint = allPoints[0];
        foreach (var restorePoint in CurrentBackup.GetAllRestorePoints())
        {
            if (restorePoint.TimeOFCreating > lastRestorePoint.TimeOFCreating)
                lastRestorePoint = restorePoint;
        }

        return lastRestorePoint;
    }

    public RestorePoint RestoreByVersion(int version)
    {
        return CurrentBackup.GetRestorePointByVersion(version);
    }

    public void MergeRestorePoints(int versionToMerge)
    {
        if (versionToMerge < 1)
            throw new ArgumentNullException();
        RestorePoint lastPoint = RestoreLastVersion();
        RestorePoint toMerge = RestoreByVersion(versionToMerge);
        foreach (Storage storage in toMerge.RepositoryStorage.GetAll())
        {
            if (!lastPoint.RepositoryStorage.GetAll().Contains(storage))
                lastPoint.AddArchive(new ZipArchiveInMemory(storage.FilePath));
            if (lastPoint.RepositoryStorage.GetAll().Contains(storage))
                toMerge.RepositoryStorage.GetAll().Remove(storage);
        }
    }

    public void RunExtra()
    {
        CleaningAlgorithm.Clean(CurrentBackup);
        Run();
        SavedConfig.RenewBackup(CurrentBackup);
    }
}