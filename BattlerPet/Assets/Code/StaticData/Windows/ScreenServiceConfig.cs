using UnityEngine;

namespace Code.StaticData.UI
{
    [CreateAssetMenu(fileName = "ScreenServiceConfig", menuName = "ScriptableObject/ScreenServiceConfig")]
    public class ScreenServiceConfig : ScriptableObject
    {
        [field: SerializeField] public ScreenPrefabMap ScreenPrefabMap { get; private set; }

        private void OnValidate() => 
            CreateScreenPrefabMap();
        
        private void CreateScreenPrefabMap()
        {
#if UNITY_EDITOR
            ScreenPrefabMap.CreateMap();
#endif
        }
    }
}