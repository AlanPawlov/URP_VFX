namespace EventBus.Handlers
{
    public interface IGameplayMessageHandler : IGlobalSubscriber
    {
        void HandleMessage(string message, float liveTime);
    }
}