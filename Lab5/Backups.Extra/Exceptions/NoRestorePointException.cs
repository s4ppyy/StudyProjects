using Backups.Extra.Entities;

namespace Backups.Extra.Exceptions;

public class NoRestorePointException : Exception
{
    public NoRestorePointException(BackupTaskExtra backupTaskExtra)
    {
        BackupTaskExtra = backupTaskExtra;
    }

    public BackupTaskExtra BackupTaskExtra { get; }
}