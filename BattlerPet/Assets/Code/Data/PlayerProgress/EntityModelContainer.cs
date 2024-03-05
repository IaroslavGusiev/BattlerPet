using System;
using Code.Gameplay.Entity;

namespace Code.Data
{
    [Serializable]
    public class EntityModelContainer : SerializableDictionary<byte, EntityModelDTO>
    {
        public void AddModelForSave(byte index, EntityModel entityModel)
        {
            if (Dictionary.TryGetValue(index, out EntityModelDTO modelDto))
            {
                //modelDto.Haste = entityModel.GetAttribute(AttributeType.Haste).CurrentValue;
            }
            
            if (Dictionary.ContainsKey(index))
                return;
        }

        private EntityModelDTO ParseModel(EntityModel entityModel)
        {
            return new EntityModelDTO()
            {

            };
        }
    }
}