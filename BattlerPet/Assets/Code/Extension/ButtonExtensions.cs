using UnityEngine.UI;
using UnityEngine.Events;

namespace CodeBase.Extensions
{
    public static class ButtonExtensions
    {
        public static void Add(this Button button, UnityAction action) => 
            button.onClick.AddListener(action);

        public static void Remove(this Button button, UnityAction action) => 
            button.onClick.RemoveListener(action);
    }
}