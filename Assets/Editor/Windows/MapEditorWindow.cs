using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.Handlers;
using Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MapEditorWindow : EditorWindow, IEditorCategoriesToolbarHandler
{
    [SerializeField] private Sprite _categorySprites;
    [SerializeField] private string[] _categoryNames;
    [SerializeField] private VisualTreeAsset _mapAsset;
    [SerializeField] private VisualTreeAsset _editorCategoryAsset;
    [SerializeField] private VisualTreeAsset _toolBarAsset;

    [SerializeField] private GameDataSo _gameDataSo;

    private GameData _gameData;
    private ToolBarEditorWidget _toolbar;
    private ListView _leftPlane;
    private string _selectedCategory;
    private Dictionary<string, BasePage> _pages;
    private TwoPaneSplitView _splitView;
    private VisualElement _root;
    private bool _hideCategory;

    [MenuItem("Tools/MapEditor _%#T")]
    public static void ShowExample()
    {
        var window = GetWindow<MapEditorWindow>();
        window.titleContent = new GUIContent("MapEditorWindow");
        window.minSize = new Vector2(50, 50);
    }

    public void CreateGUI()
    {
        EventBus.EventBus.Subscribe(this);
        _pages = new Dictionary<string, BasePage>();
        _gameData = new GameData();
        _gameData.Maps = _gameDataSo.Maps;
        _gameData.GameModes = _gameDataSo.GameModes;

        var types = TypeCache.GetTypesDerivedFrom<BasePage>();
        foreach (var t in types)
        {
            var instance = (BasePage)Activator.CreateInstance(t);
            var page = instance.GetInstance(_mapAsset, _gameData);
            if (!_pages.ContainsKey(page.GetCategory))
            {
                _pages.Add(page.GetCategory, page);
            }
        }

        _categoryNames = _pages.Keys.ToArray();

        _root = rootVisualElement;
        _toolbar = new ToolBarEditorWidget(_root, _toolBarAsset);

        _splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        _root.Add(_splitView);
        _leftPlane = new ListView(_categoryNames, 100, MakeCategoryItem, BindCategoryItem);
        _leftPlane.selectionType = SelectionType.Single;
        _leftPlane.selectionChanged += OnCategoryChanged;
        _splitView.Add(_leftPlane);
        _selectedCategory = _categoryNames[0];
        var newPage = _pages[_selectedCategory].GetView();
        _splitView.Add(newPage);
    }

    public void HandleCategoryVisiblityChange()
    {
        _hideCategory = !_hideCategory;
        if (_hideCategory)
        {
            _splitView.CollapseChild(0);
            return;
        }

        _splitView.UnCollapse();
    }

    private void OnCategoryChanged(IEnumerable<object> obj)
    {
        var newCategory = (string)obj.First();

        if (_pages.ContainsKey(_selectedCategory))
        {
            var oldPage = _pages[_selectedCategory].GetView();
            _splitView.Remove(oldPage);
        }

        _selectedCategory = newCategory;
        var newPage = _pages[_selectedCategory].GetView();
        _splitView.Add(newPage);
        newPage.style.flexGrow = 1;
    }

    private VisualElement MakeCategoryItem()
    {
        return _editorCategoryAsset.CloneTree();
    }

    private void BindCategoryItem(VisualElement e, int i)
    {
        e.Q<Label>("Name").text = _categoryNames[i];
        e.Q<VisualElement>("Image").style.backgroundImage = _categorySprites.texture;
    }

    private void OnDestroy()
    {
        foreach (var page in _pages)
        {
            page.Value.Uninit();
        }

        EventBus.EventBus.Unsubscribe(this);
    }
}

public class GameData
{
    public List<MapModel> Maps;
    public List<GameModeModel> GameModes;
}