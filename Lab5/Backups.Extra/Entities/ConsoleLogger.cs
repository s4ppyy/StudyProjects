using Backups.Entities;

namespace Backups.Extra.Entities;

public class ConsoleLogger : ILogger
{
    public ConsoleLogger(bool isPrefixNeeded)
    {
        TimePrefix = isPrefixNeeded;
    }

    public bool TimePrefix { get; }

    public void SendMessge(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException();
        Console.WriteLine(message + "\n");
    }
}