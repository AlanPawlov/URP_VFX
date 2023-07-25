using System.Collections.Generic;
using EventBus.Handlers;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class GameModeEditorPage : BasePage,IEditorToolbarHandler
{
    public ListView ListView;
    public List<GameModeModel> GameModes;
    public VisualTreeAsset _mapAsset;
    public override string GetCategory => "Game modes";

    public GameModeEditorPage()
    {
    }

    public GameModeEditorPage(VisualTreeAsset mapAsset, GameData gameData)
    {
        _mapAsset = mapAsset;
        GameModes = gameData.GameModes;
        Init();
        EventBus.EventBus.Subscribe(this);
    }

    public override void Init()
    {
        ListView = new ListView(GameModes, 105, MakeItem, BindItem);
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
        e.Q<TextField>("Name").value = GameModes[i].Name;
        e.Q<IntegerField>("Id").value = GameModes[i].Id;
        e.Q<IntegerField>("MaxPlayers").value = GameModes[i].MaxPlayers;
        e.Q<TextField>("Description").value = GameModes[i].Description;

        var mapIcon = AssetDatabase.LoadAssetAtPath<Sprite>(GameModes[i].GameModeIcon);
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
        GameModes[(int)visualElement.userData].Name = changeEvent.newValue;
    }

    private void OnMaxPlayersChanged(VisualElement visualElement, ChangeEvent<int> changeEvent)
    {
        GameModes[(int)visualElement.userData].MaxPlayers = changeEvent.newValue;
    }

    private void OnIdChanged(VisualElement visualElement, ChangeEvent<int> changeEvent)
    {
        GameModes[(int)visualElement.userData].Id = changeEvent.newValue;
    }

    private void OnSpriteChanged(VisualElement visualElement, ChangeEvent<Object> changeEvent)
    {
        var newValue = (Sprite)changeEvent.newValue;
        GameModes[(int)visualElement.userData].GameModeIcon = AssetDatabase.GetAssetPath(newValue);
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
        return new GameModeEditorPage(mapAsset, gameData);
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
        GameModes.Add(new GameModeModel() { Disable = false, Id = id, MaxPlayers = 2, Name = "New GameMode" });
        ListView.RefreshItems();
    }

    public virtual void HandleRemoveElement()
    {
        var targetIndex = ListView.selectedIndex == -1 ? GameModes.Count - 1 : ListView.selectedIndex;
        GameModes.RemoveAt(targetIndex);
        ListView.RefreshItems();
    }
}