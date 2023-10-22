using Backups.Entities;
using Backups.Extra.Entities;

namespace Backups.Extra.CleaningAlgorithms;

public class CleanByAmount : ICleaningAlgorithm
{
    public CleanByAmount(int maximumAmount)
    {
        if (maximumAmount < 1)
            throw new ArgumentNullException();
        MaximalAmountOfRestorePoints = maximumAmount;
    }

    public int MaximalAmountOfRestorePoints { get; }

    public void Clean(Backup backup)
    {
        if (backup.GetAmountOfRestorePoints() == MaximalAmountOfRestorePoints)
        {
            int howManyDelete = backup.GetAmountOfRestorePoints() - MaximalAmountOfRestorePoints + 1;
            backup.RestorePoints.RemoveRange(0, howManyDelete);
        }
    }
}