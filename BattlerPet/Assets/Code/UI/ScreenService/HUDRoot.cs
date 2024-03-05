using UnityEngine;

namespace Code.UI.ScreenServiceSpace
{
    public class HUDRoot : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        public void SetMainCameraAsWorldCamera() => 
            _canvas.worldCamera = Camera.main;
    }
}