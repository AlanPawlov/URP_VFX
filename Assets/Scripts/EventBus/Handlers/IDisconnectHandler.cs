namespace EventBus.Handlers
{
    public interface IDisconnectHandler : IGlobalSubscriber
    {
        void HandleDisconnect();
        //void HandleConnect();
    }
}