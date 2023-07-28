using Code.Data;
using Code.Gameplay.Hero;
using UnityEngine;

namespace Code.StaticData.Hero
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "ScriptableObject/HeroData")]
    public class HeroData : ScriptableObject
    {
        [field: SerializeField] public HeroBehaviour Prefab { get; private set; }
        [field: SerializeField] public HeroType HeroType { get; private set; }
        
        [field: SerializeField] public float MaxHp { get; private set; }
        [field: SerializeField] public float MaxHaste { get; private set; }
    }
}