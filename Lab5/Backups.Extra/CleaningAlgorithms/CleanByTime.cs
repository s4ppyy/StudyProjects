using Backups.Entities;

namespace Backups.Extra.CleaningAlgorithms;

public class CleanByTime : ICleaningAlgorithm
{
    public CleanByTime(double timeUntilDelete)
    {
        if (timeUntilDelete <= 0)
            throw new ArgumentNullException();
        TimeUntilToDelete = timeUntilDelete;
    }

    public double TimeUntilToDelete { get; }
    public void Clean(Backup backup)
    {
        foreach (RestorePoint restorePoint in backup.RestorePoints)
        {
            if (restorePoint.TimeOFCreating.AddSeconds(TimeUntilToDelete) < DateTime.Now && backup.GetAmountOfRestorePoints() > 1)
                backup.RestorePoints.Remove(restorePoint);
        }
    }
}