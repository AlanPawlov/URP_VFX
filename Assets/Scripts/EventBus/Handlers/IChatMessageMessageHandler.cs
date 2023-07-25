namespace EventBus.Handlers
{
    public interface IChatMessageMessageHandler : IGlobalSubscriber
    {
        void HandleMessage(string message);
    }
}