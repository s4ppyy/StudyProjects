namespace Backups.Exceptions;

public class CantFindRestorePointException : Exception
{
    public CantFindRestorePointException(int version)
    {
        Version = version;
    }

    public int Version { get; }
}