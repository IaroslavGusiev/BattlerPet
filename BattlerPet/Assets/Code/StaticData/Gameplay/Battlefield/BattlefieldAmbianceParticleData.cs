using System;
using UnityEngine;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    [Serializable]
    public class BattlefieldAmbianceParticleData
    {
        [field: SerializeField] public string ParticlePrefabPath { get; private set; }
        [field: SerializeField] public List<Vector3Int> SpawnPositions { get; private set; }
    }
}