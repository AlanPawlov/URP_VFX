using System.Collections.Generic;
using UnityEngine.UIElements;

public interface IPageInstanceGetter<T> where T : BasePage
{
    T GetInstance(VisualTreeAsset mapAsset, GameData gameData);
}