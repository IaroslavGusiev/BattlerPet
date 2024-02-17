using UnityEngine;

namespace Code.Gameplay
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake() => 
            _mainCamera = Camera.main;

        private void Update()
        {
            Quaternion rotation = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
        }
    }
}