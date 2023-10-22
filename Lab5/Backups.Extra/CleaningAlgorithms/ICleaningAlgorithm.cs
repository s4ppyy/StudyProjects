using Backups.Entities;

namespace Backups.Extra.CleaningAlgorithms;

public interface ICleaningAlgorithm
{
    void Clean(Backup backup);
}