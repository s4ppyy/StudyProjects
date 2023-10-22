namespace Banks.Entities;

public interface IObserver
{
    void Update(string message);
}