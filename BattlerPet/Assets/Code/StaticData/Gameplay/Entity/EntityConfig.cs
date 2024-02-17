using UnityEngine;
using System.Collections.Generic;

namespace Code.StaticData.Gameplay
{
    [CreateAssetMenu(fileName = "EntityConfig", menuName = "ScriptableObject/Gameplay/EntityConfig")]
    public class EntityConfig : ScriptableObject
    {
        [field: Header("EntityType")]
        [field: SerializeField] public EntityType EntityType { get; private set; }
        [field: Space(10)]
        
        [field: Header("Prefab")]
        [field: SerializeField] public string PrefabAddress { get; private set; }
        [field: Space(1)]
        
        [field: Header("Skills")]
        [field: SerializeField] public List<SkillConfig> SkillConfigs { get; private set; } 
        [field: Space(10)]
        
        [field: Header("Stats")]
        [field: SerializeField] public float MaxHp { get; private set; }
        [field: SerializeField] public float MaxHaste { get; private set; }
    }
}