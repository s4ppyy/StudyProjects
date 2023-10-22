namespace Banks.Entities;

public interface IObservable
{
    void AddObserver(IObserver observer);

    void RemoveObserver(IObserver observer);

    void NotifyObservers(string message);
}
