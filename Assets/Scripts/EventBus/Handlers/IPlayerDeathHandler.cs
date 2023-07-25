namespace EventBus.Handlers
{
    public interface IPlayerDeathHandler : IGlobalSubscriber
    {
        void HandleDeath();
    }
}