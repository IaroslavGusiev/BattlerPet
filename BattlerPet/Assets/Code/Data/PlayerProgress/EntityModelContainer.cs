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

        //     private EntityModelDTO ParseModel(EntityModel entityModel)
        //     {
        //         var dto = new EntityModelDTO()
        //         {
        //             EntityId = entityModel._modelDto.EntityId, // Assuming EntityId is directly stored
        //             EntityType = entityModel._modelDto.EntityType, // Assuming EntityType is directly stored
        //         };
        //
        //         // Iterate through entity attributes and extract current values
        //         foreach (var attribute in entityModel._entityAttributes)
        //         {
        //             switch (attribute.AttributeType)
        //             {
        //                 case AttributeType.Health:
        //                     dto.CurrentHealth = attribute.CurrentValue.Value;
        //                     break;
        //                 case AttributeType.Haste:
        //                     dto.CurrentHaste = attribute.CurrentValue.Value;
        //                     break;
        //                 // Add cases for other attribute types
        //             }
        //         }
        //
        //         return dto;
        //     }
        // }
    }
    
    public interface IEntityModelParser
    {
        EntityModelDTO ParseModel(EntityModel entityModel);
    }
}

