namespace EventBus.Handlers
{
    public interface ISceneChangeHandler : IGlobalSubscriber
    {
        void HandleLoadScene();
    }
}