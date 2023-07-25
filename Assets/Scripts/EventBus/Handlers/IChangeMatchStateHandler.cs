namespace EventBus.Handlers
{
    public interface IChangeMatchStateHandler : IGlobalSubscriber
    {
        void HandleMatchStateChanged();
    }
}