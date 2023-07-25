namespace EventBus.Handlers
{
    public interface IChangeLodoutHandler : IGlobalSubscriber
    {
        void HandleLodoutChanged();
    }
}