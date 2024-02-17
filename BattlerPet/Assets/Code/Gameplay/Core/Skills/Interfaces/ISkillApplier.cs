using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public interface ISkillApplier
    {
        SkillType SkillType { get; }
        
        void WarmUp();
        void ApplySkill(SkillExecution skillExecution); 
        // float CalculateSkillValue(string casterId, SkillTypeId skillTypeId, string targetId);
    }
}