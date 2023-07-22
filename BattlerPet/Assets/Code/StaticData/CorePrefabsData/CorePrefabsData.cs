using Code.UI;
using UnityEngine;
using Code.Infrastructure;
using Code.UI.LoadingCurtain;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "CorePrefabsData", menuName = "ScriptableObject/CorePrefabsData")]
    public class CorePrefabsData : ScriptableObject
    {
        [field: SerializeField] public UIRoot UIRootPrefab { get; private set; }
        [field: SerializeField] public Bootstrapper BootstrapperPrefab { get; private set; }
        [field: SerializeField] public LoadingCurtain LoadingCurtainPrefab { get; private set; }
        [field: SerializeField] public CoroutineRunner CoroutineRunnerPrefab { get; private set; }
    }
}