using UnityEngine;
using System.Collections.Generic;

namespace Code.Data.Gameplay.Battlefield
{
    [CreateAssetMenu(fileName = "SkyboxData", menuName = "ScriptableObject/Battlefield/SkyboxData")]
    public class SkyboxData : ScriptableObject
    {
        [field: SerializeField] public List<Material> SkyBoxMaterials { get; private set; } 
    }
}