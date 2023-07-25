namespace EventBus.Handlers
{
    public interface IQuickSaveLoadHandler : IGlobalSubscriber
    {
        void HandleQuickSave();
        void HandleQuickLoad();
    }
}