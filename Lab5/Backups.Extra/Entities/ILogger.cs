namespace Backups.Extra.Entities;

public interface ILogger
{
    bool TimePrefix { get; }
    void SendMessge(string message);
}