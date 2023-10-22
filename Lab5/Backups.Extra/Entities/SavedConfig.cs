using Backups.Algorithms;
using Backups.Entities;

namespace Backups.Extra.Test;

public class SavedConfig
{
    public SavedConfig(BackupConfigurtion backupConfigurtion, string pathToSaverTxt)
    {
        BackupConfigurtion = backupConfigurtion ?? throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(pathToSaverTxt))
            throw new ArgumentNullException();
        PathToTxt = pathToSaverTxt;
    }

    public string PathToTxt { get; }
    public BackupConfigurtion BackupConfigurtion { get; }

    public Backup? Backup { get; private set; }

    public void RenewBackup(Backup backup)
    {
        if (backup == null)
            throw new ArgumentNullException();
        Backup = backup;
    }
}