using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [CreateAssetMenu(fileName = "MapData", menuName = "Map")]
    public class GameDataSo : ScriptableObject
    {
        [SerializeField] public List<MapModel> Maps;
        [SerializeField] public List<GameModeModel> GameModes = new List<GameModeModel>();
    }
}