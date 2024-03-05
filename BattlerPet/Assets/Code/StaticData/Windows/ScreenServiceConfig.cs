using UnityEngine;

namespace Code.StaticData.UI
{
    [CreateAssetMenu(fileName = "ScreenServiceConfig", menuName = "ScriptableObject/ScreenServiceConfig")]
    public class ScreenServiceConfig : ScriptableObject
    {
        [field: SerializeField] public ScreenPrefabMap ScreenPrefabMap { get; private set; }

        private void OnValidate() => 
            CreateScreenPrefabMap();

#if UNITY_EDITOR
        private void CreateScreenPrefabMap() => 
            ScreenPrefabMap.CreateMap();
#endif
    }
}