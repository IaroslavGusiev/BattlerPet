using System;
using UnityEngine;
using System.Collections.Generic;

namespace Code.Data.Windows
{
    [CreateAssetMenu(fileName = "WindowsStaticData", menuName = "ScriptableObject/WindowsStaticData")]
    public class WindowsStaticData : ScriptableObject
    {
        [field: SerializeField] public List<WindowConfig> WindowConfigs { get; private set; }
    }

    [Serializable]
    public class WindowConfig
    {
        public WindowType WindowType;
        public GameObject Prefab;
    }
}