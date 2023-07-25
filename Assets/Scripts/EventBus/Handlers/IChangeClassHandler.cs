namespace EventBus.Handlers
{
    public interface IChangeClassHandler : IGlobalSubscriber
    {
        void HandleClassChanged();
    }
}