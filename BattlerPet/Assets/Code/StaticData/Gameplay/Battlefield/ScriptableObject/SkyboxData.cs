using UnityEngine;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    [CreateAssetMenu(fileName = "SkyboxData", menuName = "ScriptableObject/Battlefield/SkyboxData")]
    public class SkyboxData : ScriptableObject
    {
        [SerializeField] private List<Material> _skyBoxMaterials;

        public Material GetRandomSkyboxMaterial() => 
            _skyBoxMaterials.PickRandom();
    }
}