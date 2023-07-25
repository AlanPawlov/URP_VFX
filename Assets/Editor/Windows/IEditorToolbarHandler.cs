namespace EventBus.Handlers
{
    public interface IEditorToolbarHandler : IGlobalSubscriber
    {
        void HandleSave();
        void HandleLoad();
        void HandleAddElement();
        void HandleRemoveElement();
    }
}