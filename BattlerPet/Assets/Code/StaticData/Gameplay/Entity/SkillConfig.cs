using UnityEngine;

namespace Code.StaticData.Gameplay
{
    [CreateAssetMenu(fileName = "SkillConfig", menuName = "ScriptableObject/Gameplay/SkillConfig")]
    public class SkillConfig : ScriptableObject
    {
        [field: Header("Base")]
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public EntityType EntityType { get; private set; }
        [field: SerializeField] public AttackType AttackType { get; private set; }
        [field: Space(15)]
        
        [field: Header("Skill Parameters")]
        [field: SerializeField] public SkillType SkillType { get; private set; }
        [field: SerializeField] public TargetType TargetType { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: Space(15)]
        
        [field: Header("Skill Modifier Parameters")]
        [field: SerializeField] public SkillModifier SkillModifier { get; private set; }
        
        public void Rename(string newName) => 
            name = newName;
    }
}