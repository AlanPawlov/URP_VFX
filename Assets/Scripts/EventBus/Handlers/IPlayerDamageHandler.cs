namespace EventBus.Handlers
{
    public interface IPlayerDamageHandler : IGlobalSubscriber
    {
        void HandleDamage();
    }
}