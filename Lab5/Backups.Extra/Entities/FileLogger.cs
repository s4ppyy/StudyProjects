namespace Backups.Extra.Entities;

public class FileLogger : ILogger
{
    private StreamWriter streamWriter;
    public FileLogger(string path, bool isPrefixNeeded)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentNullException();
        Path = path;
        streamWriter = File.AppendText(Path);
        TimePrefix = isPrefixNeeded;
    }

    public string Path { get; }

    public bool TimePrefix { get; }

    public void SendMessge(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException();
        streamWriter.WriteLine(message + "\n");
    }
}