using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.CleaningAlgorithms;
using Backups.Extra.Entities;
using Backups.Repositories;
using Backups.Services;
using Xunit;

namespace Backups.Extra.Test;

public class Backups_Extra_Tests
{
    [Fact]
    public void BackupTaskIsWorkingRight()
    {
        SplitStorageAlgorithmInMemory splitAlgorithm = new SplitStorageAlgorithmInMemory();
        RepositoryBackupObjectsInMemory repositoryBackupObjectsInMemory = new RepositoryBackupObjectsInMemory();
        RepositoryStorageInMemory repositoryStorageInMemory = new RepositoryStorageInMemory("~/Danee");
        BackupConfigurtion config =
            new BackupConfigurtion(repositoryBackupObjectsInMemory, repositoryStorageInMemory, splitAlgorithm);
        SavedConfig savedConfig = new SavedConfig(
            config,
            "C:/Users/Danee/OneDrive/Рабочий стол/C#/s4ppyy/Lab5/Backups.Extra.Test/SaverTxt.txt");
        CleanByAmount cleanByAmount = new CleanByAmount(5);
        var backupTask = new BackupTaskExtra(config, savedConfig, cleanByAmount);
        BackupObject backupObject1 = new BackupObject("text", "~/Danee/Books/");
        BackupObject backupObject2 = new BackupObject("poem", "~/Danee/Books/");
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        backupTask.Run();
        backupTask.DeleteBackupObject(backupObject1);
        backupTask.RunExtra();
        var backup = backupTask.FinishTask();
        Assert.Equal(2, backup.Count);
        Assert.Equal(3, repositoryStorageInMemory.GetSize());
    }

    [Fact]
    public void RestorePointsCleanerAlgorithmWorking()
    {
        SplitStorageAlgorithmInMemory splitAlgorithm = new SplitStorageAlgorithmInMemory();
        RepositoryBackupObjectsInMemory repositoryBackupObjectsInMemory = new RepositoryBackupObjectsInMemory();
        RepositoryStorageInMemory repositoryStorageInMemory = new RepositoryStorageInMemory("~/Danee");
        BackupConfigurtion config =
            new BackupConfigurtion(repositoryBackupObjectsInMemory, repositoryStorageInMemory, splitAlgorithm);
        SavedConfig savedConfig = new SavedConfig(
            config,
            "C:/Users/Danee/OneDrive/Рабочий стол/C#/s4ppyy/Lab5/Backups.Extra.Test/SaverTxt.txt");
        CleanByAmount cleanByAmount = new CleanByAmount(4);
        var backupTask = new BackupTaskExtra(config, savedConfig, cleanByAmount);
        BackupObject backupObject1 = new BackupObject("text", "~/Danee/Books/");
        BackupObject backupObject2 = new BackupObject("poem", "~/Danee/Books/");
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        backupTask.DeleteBackupObject(backupObject1);
        backupTask.RunExtra();
        backupTask.RunExtra();
        backupTask.RunExtra();
        backupTask.RunExtra();
        backupTask.RunExtra();
        var backup = backupTask.FinishTask();
        Assert.True(backup[0].Version == 1);
    }

    [Fact]
    public void MergeRestorePoints_PointsMerged()
    {
        SplitStorageAlgorithmInMemory splitAlgorithm = new SplitStorageAlgorithmInMemory();
        RepositoryBackupObjectsInMemory repositoryBackupObjectsInMemory = new RepositoryBackupObjectsInMemory();
        RepositoryStorageInMemory repositoryStorageInMemory = new RepositoryStorageInMemory("~/Danee");
        BackupConfigurtion config =
            new BackupConfigurtion(repositoryBackupObjectsInMemory, repositoryStorageInMemory, splitAlgorithm);
        SavedConfig savedConfig = new SavedConfig(
            config,
            "C:/Users/Danee/OneDrive/Рабочий стол/C#/s4ppyy/Lab5/Backups.Extra.Test/SaverTxt.txt");
        CleanByAmount cleanByAmount = new CleanByAmount(4);
        var backupTask = new BackupTaskExtra(config, savedConfig, cleanByAmount);
        BackupObject backupObject1 = new BackupObject("text", "~/Danee/Books/");
        BackupObject backupObject2 = new BackupObject("poem", "~/Danee/Books/");
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        backupTask.RunExtra();
        backupTask.DeleteBackupObject(backupObject1);
        backupTask.RunExtra();
        backupTask.MergeRestorePoints(1);
        var backup = backupTask.FinishTask();
        Assert.Equal(4, backup[1].RepositoryStorage.GetAll().Capacity);
    }
}