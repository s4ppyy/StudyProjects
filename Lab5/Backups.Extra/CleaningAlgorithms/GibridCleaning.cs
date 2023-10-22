using Backups.Entities;

namespace Backups.Extra.CleaningAlgorithms;

public class GibridCleaning : ICleaningAlgorithm
{
    public GibridCleaning(bool bothToDelete, int maximumAmount, double timeUntilDelete)
    {
        BothToDeleteFlag = bothToDelete;
        if (maximumAmount < 1)
            throw new ArgumentNullException();
        MaximalAmountOfRestorePoints = maximumAmount;
        if (timeUntilDelete <= 0)
            throw new ArgumentNullException();
        TimeUntilToDelete = timeUntilDelete;
    }

    public int MaximalAmountOfRestorePoints { get; }

    public double TimeUntilToDelete { get; }

    public bool BothToDeleteFlag { get; }

    public void Clean(Backup backup)
    {
        var candidatesFromAmount = new List<RestorePoint>();
        var candidatesFromTime = new List<RestorePoint>();
        if (backup.GetAmountOfRestorePoints() == MaximalAmountOfRestorePoints)
        {
            candidatesFromAmount.Add(backup.RestorePoints[0]);
        }

        foreach (RestorePoint restorePoint in backup.RestorePoints)
        {
            if (restorePoint.TimeOFCreating.AddSeconds(TimeUntilToDelete) < DateTime.Now && backup.GetAmountOfRestorePoints() > 1)
                candidatesFromTime.Add(restorePoint);
        }

        if (BothToDeleteFlag)
        {
            var toDelete = candidatesFromAmount.Intersect(candidatesFromTime);
            foreach (RestorePoint restorePoint in toDelete)
            {
                backup.RestorePoints.Remove(restorePoint);
            }
        }
        else
        {
            foreach (RestorePoint restorePoint in candidatesFromAmount)
            {
                backup.RestorePoints.Remove(restorePoint);
            }

            foreach (RestorePoint restorePoint in candidatesFromTime)
            {
                backup.RestorePoints.Remove(restorePoint);
            }
        }
    }
}