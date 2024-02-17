using UnityEngine;
using System.Collections.Generic;

namespace Code.Gameplay.Entity
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private List<AttributeSlider> _attributeSliders;
        
        public void Construct(IReactiveModel reactiveModel)
        {
            foreach (AttributeSlider slider in _attributeSliders)
                slider.Initialize(reactiveModel.GetAttribute(slider.AttributeType));
        }
    }
}