using UnityEngine;
using Code.Data.Gameplay;
using Code.Gameplay.Hero;
using System.Collections.Generic;

namespace Code.StaticData.Hero
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/HeroData")]
    public class HeroData : ScriptableObject
    {
        [field: Header("HeroType")]
        [field: SerializeField] public HeroType HeroType { get; private set; }
        [field: Space(10)]
        
        [field: Header("Prefab")]
        [field: SerializeField] public HeroBehaviour Prefab { get; private set; }
        [field: SerializeField] public string PrefabPath { get; set; }
        [field: Space(1)]
        
        [field: Header("Skills")]
        [field: SerializeField] public List<SkillData> SkillData { get; private set; }
        [field: Space(10)]
        
        [field: Header("Stats")]
        [field: SerializeField] public float MaxHp { get; private set; }
        [field: SerializeField] public float MaxHaste { get; private set; }
    }
}