using System;

namespace Code.Data
{
    public class PlayerProgress
    {
        public EntityModelContainer ModelContainer = new();
    }

    [Serializable]
    public class EntityModelDTO
    {
        public byte EntityType;
        public string EntityId;
        public float CurrentHealth;
        public float CurrentHaste;
        public float MaxHealth;
        public float MaxHaste;
    }
}