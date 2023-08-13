using UnityEngine;

namespace Code.Data.Gameplay.Battlefield
{
    [CreateAssetMenu(fileName = "BattlefieldConfig", menuName = "ScriptableObject/Battlefield/BattlefieldConfig")]
    public class BattlefieldConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3Int GridSize { get; private set; } 
        [field: SerializeField] public Vector3Int StartPositionOffset { get; private set;}
    }
}