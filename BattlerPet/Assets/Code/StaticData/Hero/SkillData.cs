using UnityEngine;
using Code.Data.Gameplay;

namespace Code.StaticData.Hero
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObject/SkillData")]
    public class SkillData : ScriptableObject
    {
        [field: Header("Base")]
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public AnimAttackType AnimAttackType { get; private set; }
        [field: Space(15)]
        
        [field: Header("Skill Parameters")]
        [field: SerializeField] public SkillType SkillType { get; private set; }
        [field: SerializeField] public TargetType TargetType { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
        [field: Space(15)]
        
        [field: Header("Skill Modifier Parameters")]
        [field: SerializeField] public SkillModifier SkillModifier { get; private set; }
        [field: SerializeField] public float SkillModifierChance { get; private set; }
        [field: SerializeField] public float SkillModifierValue { get; private set; }
        [field: SerializeField] public float SkillModifierAmount { get; private set; }
        
        public void Rename(string newName) => 
            name = newName;
    }
}