using UnityEngine;
using Code.Gameplay.Entity;
using Cysharp.Threading.Tasks;
using Code.StaticData.Gameplay;

namespace Code.Infrastructure
{
    public interface IEntityFactory
    {
        UniTask<EntityBehaviour> CreateEntity(EntityType entityType, Vector3 at, Quaternion rotation, Transform parent);
    }
}