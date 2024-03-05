using Code.StaticData.UI;
using Cysharp.Threading.Tasks;
using Code.StaticData.Gameplay;
using Code.Gameplay.Battlefield;

namespace Code.Services
{
    public interface IStaticDataService
    {
        UniTask Initialize();
        BattlefieldConfig GetBattlefieldConfig();
        ScreenServiceConfig GetScreenServiceConfig();
        EntityConfig EntityConfigFor(EntityType entityType);
        SkillConfig SkillConfigFor(EntityType entityType, AttackType attackType);
    }
}