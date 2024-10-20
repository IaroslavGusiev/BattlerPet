using VContainer;
using System.Linq;
using UnityEngine;
using Code.Gameplay.Core;
using CodeBase.Extensions;

namespace Code.UI.DebugSpace
{
    public class ReportDebugScreen : MonoBehaviour
    {
        [SerializeField] private SkillExecutionEntry _skillExecutionEntryPrefab;
        [SerializeField] private Transform _entriesRoot;

        private IBattleTurnService _battleTurnService;
        private IEntityRegister _entityRegister;

        [Inject]
        public void Construct(IBattleTurnService battleTurnService, IEntityRegister entityRegister)
        {
            _entityRegister = entityRegister;
            _battleTurnService = battleTurnService;
            _battleTurnService.OnEntitySkillExecution += ShowSkillEntryForEntityAction;
        }

        private void OnDestroy() =>
            _battleTurnService.OnEntitySkillExecution -= ShowSkillEntryForEntityAction;

        public void ToggleDebugScreen() =>
            gameObject.SetActive(!gameObject.activeSelf);

        private void ShowSkillEntryForEntityAction(EntityAction entityAction)
        {
            Instantiate(_skillExecutionEntryPrefab, _entriesRoot)
                .With(x => x.EntityName.text = _entityRegister.GetEntity(entityAction.Caster).EntityType.ToString())
                .With(x => x.SkillName.text = entityAction.AttackType.ToString())
                .With(x => x.TargetsLine.text = entityAction.TargetIds.Aggregate("", (current, id) => $"{current} {_entityRegister.GetEntity(id).EntityType.ToString()}"));
        }
    }
}