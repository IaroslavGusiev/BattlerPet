using UnityEngine;

namespace Code.Data.Gameplay.Battlefield
{
    [CreateAssetMenu(fileName = "BattlefieldConfig", menuName = "ScriptableObject/Battlefield/BattlefieldConfig")]
    public class BattlefieldConfig : ScriptableObject
    {
        [field: Header("Battlefield Prefab Path")] [field: Space(10)]
        [field: SerializeField] public string BattlefieldBehaviourPath { get; private set; }
        
        [field: Space(10)] [field: Header("Battlefield Data Container")] 
        [field: SerializeField] public BattlefieldDataContainer BattlefieldDataContainer { get; private set; }
        
        [field: Header("Grid Size")]
        [field: SerializeField] public Vector3Int GridSize { get; private set; } 
        [field: SerializeField] public Vector3Int StartPositionOffset { get; private set; }
        
        [field: Space(10)] [field: Header("SkyboxData")]
        [field: SerializeField] public SkyboxData SkyboxData { get; private set; }
    }
}