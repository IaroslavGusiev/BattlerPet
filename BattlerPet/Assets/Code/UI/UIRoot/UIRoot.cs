using UnityEngine;
using VContainer.Unity;

namespace Code.UI
{
    public class UIRoot : MonoBehaviour, IUIRoot, IInitializable
    {
        public void Initialize()
        {
            Debug.Log("<color=yellow>UIRoot</color>");
        }
    }
}