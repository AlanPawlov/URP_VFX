using System;
using EventBus.Handlers;
using UnityEngine.UIElements;

public class ToolBarEditorWidget : IDisposable
{
    private VisualTreeAsset _toolBarAsset;
    private TemplateContainer _toolBar;

    public ToolBarEditorWidget(VisualElement root, VisualTreeAsset toolBarAsset)
    {
        _toolBarAsset = toolBarAsset;
        CreateToolBar(root);
    }

    private void CreateToolBar(VisualElement root)
    {
        _toolBar = _toolBarAsset.CloneTree();
        var height = 60;
        _toolBar.style.height = height;
        _toolBar.Q<Button>("Save").clicked += Save;
        _toolBar.Q<Button>("Load").clicked += Load;
        _toolBar.Q<Button>("AddElement").clicked += AddElement;
        _toolBar.Q<Button>("RemoveElement").clicked += RemoveElemnet;
        _toolBar.Q<Button>("HideCategory").clicked += CategoryVisiblityChange;

        var buttons = _toolBar.Query<Button>().ToList();
        var size = height / 2;
        foreach (var button in buttons)
        {
            button.style.height = size;
            button.style.width = size;
        }

        root.Add(_toolBar);
    }

    private void CategoryVisiblityChange()
    {
        EventBus.EventBus.RaiseEvent<IEditorCategoriesToolbarHandler>(h=>h.HandleCategoryVisiblityChange());
    }

    private void RemoveElemnet()
    {
        EventBus.EventBus.RaiseEvent<IEditorToolbarHandler>(h=>h.HandleRemoveElement());
    }

    private void Load()
    {
        EventBus.EventBus.RaiseEvent<IEditorToolbarHandler>(h=>h.HandleLoad());
    }

    private void Save()
    {
        EventBus.EventBus.RaiseEvent<IEditorToolbarHandler>(h=>h.HandleSave());
    }

    private void AddElement()
    {
        EventBus.EventBus.RaiseEvent<IEditorToolbarHandler>(h=>h.HandleAddElement());
    }

    public void Dispose()
    {
        _toolBar.Q<Button>("Save").clicked -= Save;
        _toolBar.Q<Button>("Load").clicked -= Load;
        _toolBar.Q<Button>("AddElement").clicked -= AddElement;
        _toolBar.Q<Button>("RemoveElement").clicked -= RemoveElemnet;
        _toolBar.Q<Button>("HideCategory").clicked -= CategoryVisiblityChange;
        _toolBar = null;
        _toolBarAsset = null;
    }
}