namespace Backups.Entities;

public class ZipArchiveInMemory
{
    private List<Storage> _storagesInArchive;
    public ZipArchiveInMemory(string pathToArchive)
    {
        if (string.IsNullOrWhiteSpace(pathToArchive))
            throw new ArgumentNullException();
        PathToArchive = pathToArchive + ".zip";
        _storagesInArchive = new List<Storage>();
    }

    public string PathToArchive { get; }

    public List<Storage> Storages { get { return _storagesInArchive; } }
    public void AddToArchive(Storage storage)
    {
        if (storage == null)
            throw new ArgumentNullException();
        _storagesInArchive.Add(storage);
    }
}