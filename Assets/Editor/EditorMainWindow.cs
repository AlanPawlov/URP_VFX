using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EditorMainWindow : EditorWindow
{
    private ScrollView _rightPane;
    [SerializeField] private int _selectedIndex = -1;

    [MenuItem("Tools/EditorMainWindow _%#L")]
    public static void ShowExample()
    {
        var window = GetWindow<EditorMainWindow>();
        window.titleContent = new GUIContent("EditorMainWindow");
        window.minSize = new Vector2(450, 200);
        window.maxSize = new Vector2(1920, 720);
    }

    public void CreateGUI()
    {
        var root = rootVisualElement;
        var allObjectGuids = AssetDatabase.FindAssets("t:Sprite");
        var allObjects = new List<Sprite>();
        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);
        root.Add(splitView);

        var leftPane = new ListView();
        splitView.Add(leftPane);
        _rightPane = new ScrollView();
        splitView.Add(_rightPane);
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
        leftPane.itemsSource = allObjects;
        leftPane.selectedIndex = _selectedIndex;
        leftPane.onSelectionChange += (items) => { _selectedIndex = leftPane.selectedIndex; };
        leftPane.onSelectionChange += OnTreeSelectionChange;
    }

    private void OnTreeSelectionChange(IEnumerable<object> obj)
    {
        _rightPane.Clear();
        var selectedSprite = obj.First() as Sprite;
        if (selectedSprite == null)
            return;

        var spriteImage = new Image();
        spriteImage.scaleMode = ScaleMode.ScaleToFit;
        spriteImage.sprite = selectedSprite;

        _rightPane.Add(spriteImage);
    }
}