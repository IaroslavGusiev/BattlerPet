using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CodeBase.Extensions
{
    public static class MonoBehaviourExtensions
    {
        private static readonly WaitForFixedUpdate WaitForEndOfFixedUpdate = new();
        private static readonly WaitForEndOfFrame WaitForEndOfFrame = new();
        
        public static WaitForEndOfFrame GetWaitForEndOfFrame()
            => WaitForEndOfFrame;

        public static WaitForFixedUpdate GetWaitForFixedUpdate()
            => WaitForEndOfFixedUpdate;
        
        public static void SetSortingOrderAndLayer(this SpriteRenderer spriteRenderer, int sortingOrder, string sortingLayerName)
        {
            spriteRenderer.sortingOrder = sortingOrder;
            spriteRenderer.sortingLayerName = sortingLayerName;
        }
        
        public static void Add(this Button button, UnityAction action) =>
            button.onClick.AddListener(action);

        public static void Remove(this Button button, UnityAction action) =>
            button.onClick.RemoveListener(action);
        
        public static bool IsNull(this Object obj) => 
            (object)obj == null;

        public static bool IsNotNull(this Object obj) => 
            !IsNull(obj);
    }
}