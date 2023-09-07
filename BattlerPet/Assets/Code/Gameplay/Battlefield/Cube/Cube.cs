using UnityEngine;
using Code.Data.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class Cube : MonoBehaviour
    {
        [field: SerializeField] public BattlefieldPart BattlefieldPart { get; private set; }
        [SerializeField] private MeshRenderer _meshRenderer;

        public void ChangeMaterial(Material material) => 
            _meshRenderer.material = material;
    }
}