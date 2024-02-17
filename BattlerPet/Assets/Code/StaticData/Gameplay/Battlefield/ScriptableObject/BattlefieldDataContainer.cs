using UnityEngine;
using Code.Data.Battlefield;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    [CreateAssetMenu(fileName = "BattlefieldDataContainer", menuName = "ScriptableObject/Battlefield/BattlefieldDataContainer")]
    public class BattlefieldDataContainer : ScriptableObject
    {
        [field: SerializeField] public List<BattlefieldPartBundle> BattlefieldPartBundles { get; private set; }
        [field: SerializeField] public List<TopLayerCubeType> TopLayerCubeTypes { get; private set; }
    }
}