using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Code.UI.ScreenServiceSpace;
using UnityEngine.AddressableAssets;

namespace Code.StaticData.UI
{
    [Serializable]
    public class ScreenPrefabMap
    {
#if UNITY_EDITOR
        [SerializeField] private List<CommonScreen> _screens = new();
#endif
        private Dictionary<Type, AssetReference> _screenReferenceMap;
        
        public AssetReference GetPrefabReference<TScreen>() where TScreen : CommonScreen
        {
            Debug.Log($"<color=yellow> {_screenReferenceMap.Count} </color>");
            
            if (_screenReferenceMap.TryGetValue(typeof(TScreen), out AssetReference screenReference))
                return screenReference;
            
            throw new IndexOutOfRangeException($"Can't find asset reference for view of type { typeof(TScreen) }");
        }
        
#if UNITY_EDITOR
        public void CreateMap()
        {
            _screenReferenceMap = new Dictionary<Type, AssetReference>();
            foreach (CommonScreen screen in _screens
                         .Where(screen => !_screenReferenceMap.TryAdd(screen.GetType(), screen.AssetReference)))
                Debug.LogError($" Duplicate type found: { screen.GetType() }");
        }
#endif
    }
}