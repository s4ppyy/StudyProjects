namespace Backups.Entities;

public class Storage
{
    public Storage(string fileName, string filePath)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentNullException();
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentNullException();
        FileName = fileName;
        FilePath = filePath;
    }

    public string FileName { get; }
    public string FilePath { get; }
}