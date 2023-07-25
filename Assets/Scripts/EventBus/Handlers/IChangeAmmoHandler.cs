namespace EventBus.Handlers
{
    public interface IChangeAmmoHandler : IGlobalSubscriber
    {
        void HandleChangeAmmo(string ammo, string supply);
    }
}