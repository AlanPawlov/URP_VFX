namespace EventBus.Handlers
{
    public interface IEditorCategoriesToolbarHandler : IGlobalSubscriber
    {
        void HandleCategoryVisiblityChange();
    }
}