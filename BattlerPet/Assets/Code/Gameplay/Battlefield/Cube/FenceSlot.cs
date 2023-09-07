using UnityEngine;
using Code.Data.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class FenceSlot : MonoBehaviour
    {
        [field: SerializeField] public SideType SideType { get; private set; }

        public Vector3 GetPosition => 
            transform.position;
    }
}