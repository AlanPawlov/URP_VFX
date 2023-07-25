using System.Collections.Generic;
using UnityEngine.UIElements;

public abstract class BasePage : IPageInstanceGetter<BasePage>
{
    public abstract void Init();
    public abstract void Uninit();
    public abstract VisualElement GetView();
    public virtual string GetCategory { get; set; }
    public virtual BasePage GetInstance(VisualTreeAsset mapAsset, GameData gameData)
    {
        throw new System.NotImplementedException();
    }
}