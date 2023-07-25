using System.Collections.Generic;
using EventBus.Handlers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class MapEditorPage : BasePage, IEditorToolbarHandler
{
    public ListView ListView;
    public List<MapModel> _maps;
    public VisualTreeAsset _mapAsset;
    public override string GetCategory => "Map";

    public MapEditorPage()
    {
    }

    public MapEditorPage(VisualTreeAsset mapAsset, GameData maps)
    {
        _mapAsset = mapAsset;
        _maps = maps.Maps;
        Init();
        EventBus.EventBus.Subscribe(this);
    }

    public override void Init()
    {
        ListView = new ListView(_maps, 105, MakeItem, BindItem);
        ListView.unbindItem = Unbind;
        ListView.selectionType = SelectionType.Single;
    }

    private void Unbind(VisualElement e, int i)
    {
        e.Q<TextField>("Name").value = default;
        e.Q<IntegerField>("Id").value = default;
        e.Q<IntegerField>("MaxPlayers").value = default;
        e.Q<TextField>("Description").value = default;
        e.Q<ObjectField>("MapIcon").value = default;
        e.Q<VisualElement>("MapIconView").style.backgroundImage = default;
    }

    private VisualElement MakeItem()
    {
        var result = _mapAsset.CloneTree();
        result.Q<TextField>("Name").RegisterValueChangedCallback((e) => OnNameChanged(result, e));
        result.Q<IntegerField>("Id").RegisterValueChangedCallback((e) => OnIdChanged(result, e));
        result.Q<IntegerField>("MaxPlayers").RegisterValueChangedCallback(e => OnMaxPlayersChanged(result, e));
        result.Q<ObjectField>("MapIcon").RegisterValueChangedCallback(e => OnSpriteChanged(result, e));
        return result;
    }

    private void BindItem(VisualElement e, int i)
    {
        e.userData = i;
        e.Q<TextField>("Name").value = _maps[i].Name;
        e.Q<IntegerField>("Id").value = _maps[i].Id;
        e.Q<IntegerField>("MaxPlayers").value = _maps[i].MaxPlayers;
        e.Q<TextField>("Description").value = _maps[i].Description;

        var mapIcon = AssetDatabase.LoadAssetAtPath<Sprite>(_maps[i].SceneIcon);
        var iconPreview = e.Q<VisualElement>("MapIconView");
        var size = e.style.height;
        if (mapIcon != null)
        {
            e.Q<ObjectField>("MapIcon").value = mapIcon;
            iconPreview.style.backgroundImage = mapIcon.texture;
        }

        iconPreview.style.height = size;
        iconPreview.style.width = size;
    }

    private void OnNameChanged(VisualElement visualElement, ChangeEvent<string> changeEvent)
    {
        _maps[(int)visualElement.userData].Name = changeEvent.newValue;
    }

    private void OnMaxPlayersChanged(VisualElement visualElement, ChangeEvent<int> changeEvent)
    {
        _maps[(int)visualElement.userData].MaxPlayers = changeEvent.newValue;
    }

    private void OnIdChanged(VisualElement visualElement, ChangeEvent<int> changeEvent)
    {
        _maps[(int)visualElement.userData].Id = changeEvent.newValue;
    }

    private void OnSpriteChanged(VisualElement visualElement, ChangeEvent<Object> changeEvent)
    {
        var newValue = (Sprite)changeEvent.newValue;
        _maps[(int)visualElement.userData].SceneIcon = AssetDatabase.GetAssetPath(newValue);
        visualElement.Q<ObjectField>("MapIcon").value = newValue;
        visualElement.Q<VisualElement>("MapIconView").style.backgroundImage =
            newValue == null ? default : newValue.texture;
    }

    public override void Uninit()
    {
        EventBus.EventBus.Unsubscribe(this);
    }

    public override BasePage GetInstance(VisualTreeAsset mapAsset, GameData gameData)
    {
        return new MapEditorPage(mapAsset, gameData);
    }

    public override VisualElement GetView()
    {
        return ListView;
    }

    public void HandleSave()
    {
        Debug.Log("Typo Save");
    }

    public void HandleLoad()
    {
        Debug.Log("Typo Load");
    }

    public virtual void HandleAddElement()
    {
        var id = Random.Range(0, 99999);
        _maps.Add(new MapModel() { Disable = false, Id = id, MaxPlayers = 2, Name = "New MapData" });
        ListView.RefreshItems();
    }

    public virtual void HandleRemoveElement()
    {
        var targetIndex = ListView.selectedIndex == -1 ? _maps.Count - 1 : ListView.selectedIndex;
        _maps.RemoveAt(targetIndex);
        ListView.RefreshItems();
    }
}
