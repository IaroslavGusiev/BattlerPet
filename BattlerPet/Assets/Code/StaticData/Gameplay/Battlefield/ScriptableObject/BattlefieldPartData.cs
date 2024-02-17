using UnityEngine;
using Code.Data.Battlefield;

namespace Code.Gameplay.Battlefield
{
    [CreateAssetMenu(fileName = "CubeData", menuName = "ScriptableObject/Battlefield/CubeData")]
    public class BattlefieldPartData : ScriptableObject
    {
        [field: SerializeField] public string PrefabAddress { get; set; }
        [field: SerializeField] public string MaterialPath { get; set; }
        [field: SerializeField] public Cube Prefab { get; private set; }
        [field: SerializeField] public BattlefieldPart BattlefieldPart { get; private set; }
        [field: SerializeField] public TopLayerCubeType TopLayerCubeType { get; private set; } 
    }
}