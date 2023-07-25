namespace EventBus.Handlers
{
    public interface IGamemodeChangeHandler : IGlobalSubscriber
    {
        void HandleGamemodeChange();
    }
}