using Code.Data;
using UnityEngine;
using Code.Data.Gameplay.Skill;

namespace Code.StaticData.Hero
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObject/SkillData")]
    public class SkillData : ScriptableObject
    {
        [field: SerializeField] public SkillType SkillType { get; private set; }
        [field: SerializeField] public TargetType TargetType { get; private set; }
        [field: SerializeField] public AnimAttackType AnimAttackType { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public float Value { get; private set; }
        [field: SerializeField] public float Cooldown { get; private set; }
    }
}