using Code.StaticData;
using Cysharp.Threading.Tasks;
using Code.StaticData.Gameplay;
using Code.Gameplay.Battlefield;

namespace Code.Services
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        EntityConfig GetEntityData(EntityType entityType);
        BattlefieldConfig GetBattlefieldConfig();
    }
}