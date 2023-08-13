using UnityEngine;
using Code.Data.Battlefield;

namespace Code.Data.Gameplay.Battlefield
{
    [CreateAssetMenu(fileName = "CubeData", menuName = "ScriptableObject/Battlefield/CubeData")]
    public class CubeData : ScriptableObject
    {
        [field: SerializeField] public string PrefabPath { get; set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public CubeType CubeType { get; private set; }
        [field: SerializeField] public TopLayerCubeType TopLayerCubeType { get; private set; }
    }
}