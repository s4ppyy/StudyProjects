using Backups.Exceptions;

namespace Backups.Entities;

public class Backup
{
    public Backup()
    {
        RestorePoints = new List<RestorePoint>();
    }

    public List<RestorePoint> RestorePoints { get; set; }
    public IReadOnlyList<RestorePoint> GetAllRestorePoints()
    {
        return RestorePoints;
    }

    public RestorePoint GetRestorePointByVersion(int version)
    {
        return RestorePoints.Find(point => point.Version == version) ??
               throw new CantFindRestorePointException(version);
    }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (restorePoint == null)
            throw new ArgumentNullException();
        RestorePoints.Add(restorePoint);
    }

    public int GetAmountOfRestorePoints()
    {
        return RestorePoints.Count;
    }

    public void ClearRestorePointsList()
    {
        RestorePoints.Clear();
    }
}