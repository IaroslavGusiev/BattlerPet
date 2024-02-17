﻿using UnityEngine;
using Code.Data.Battlefield;

namespace Code.Gameplay.Battlefield
{
    public class SlotBehaviour : MonoBehaviour
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public SideType Side { get; private set; }

        public Vector3 GetPosition() => 
            transform.position;

        public Quaternion GetRotation() => 
            transform.rotation;
    }
}