using Backups.Algorithms;
using Backups.Entities;
using Backups.Repositories;
using Backups.Services;
using Xunit;

namespace Backups.Test;

public class BackupTests
{
    [Fact]
    public void TestOneFromNoution()
    {
        SplitStorageAlgorithmInMemory splitAlgorithm = new SplitStorageAlgorithmInMemory();
        RepositoryBackupObjectsInMemory repositoryBackupObjectsInMemory = new RepositoryBackupObjectsInMemory();
        RepositoryStorageInMemory repositoryStorageInMemory = new RepositoryStorageInMemory("~/Danee");
        BackupConfigurtion config =
            new BackupConfigurtion(repositoryBackupObjectsInMemory, repositoryStorageInMemory, splitAlgorithm);
        var backupTask = new BackupTask(config);
        BackupObject backupObject1 = new BackupObject("text", "~/Danee/Books/");
        BackupObject backupObject2 = new BackupObject("poem", "~/Danee/Books/");
        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);
        backupTask.Run();
        backupTask.DeleteBackupObject(backupObject1);
        backupTask.Run();
        var backup = backupTask.FinishTask();
        Assert.Equal(2, backup.Count);
        Assert.Equal(3, repositoryStorageInMemory.GetSize());
    }
}