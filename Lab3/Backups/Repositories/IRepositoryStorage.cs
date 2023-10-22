using Backups.Entities;

namespace Backups.Repositories;

public interface IRepositoryStorage
{
    string PathToRepository { get; }
    List<Storage> GetAll();

    List<Storage> GetByName(List<string> names);

    void AddZipArchive(ZipArchiveInMemory zipArchiveInMemory);
}