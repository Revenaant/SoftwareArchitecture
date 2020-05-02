namespace Model
{
    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void UnregisterObserver(IObserver observer);
        void NotifyObservers();
    }

    public interface IObserver
    {
        void OnUpdate();
    }
}
