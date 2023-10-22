using Backups.Entities;

namespace Backups.Exceptions;

public class CantFindBackupObjectException : Exception
{
    public CantFindBackupObjectException(BackupObject backupObject)
    {
        BackupObject = backupObject;
    }

    public BackupObject BackupObject { get; }
}