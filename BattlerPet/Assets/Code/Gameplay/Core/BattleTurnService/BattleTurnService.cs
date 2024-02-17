using System;
using Code.StaticData;
using Code.Gameplay.Entity;
using Code.Infrastructure.UpdateRunner;

namespace Code.Gameplay.Core
{
    public class BattleTurnService : IBattleTurnService, ITickListener, IDisposable
    {
        private const float TurnTickDuration = 0.3f;

        private readonly SkillCooldownService _skillCooldownService;
        private readonly IEntityRegister _entityRegister;
        private readonly HasteService _hasteService;
        private readonly ISkillSolver _skillSolver;
        private ITickSource _tickSource;

        private float _timeUntilNextTurnTick;
        private BattleState _currentBattleState = BattleState.NotStarted;
        
        public BattleTurnService(
            IEntityRegister entityRegister, 
            HasteService hasteService, 
            SkillCooldownService skillCooldownService, 
            ISkillSolver skillSolver, 
            ITickSource tickSource)
        {
            _tickSource = tickSource;
            _skillSolver = skillSolver;
            _hasteService = hasteService;
            _skillCooldownService = skillCooldownService;
            _entityRegister = entityRegister;
            _tickSource.AddListener(this);
        }

        public void Tick(float deltaTime)
        {
            if (_currentBattleState != BattleState.InProgress)
                return;
            
            UpdateTurnTimer(deltaTime);
        }
        
        public void Dispose()
        {
            _tickSource.RemoveListener(this);
            _tickSource = null;
        }

        public void StartBattle() =>
            _currentBattleState = BattleState.InProgress;

        public void EndBattle() =>
            _currentBattleState = BattleState.Finished;

        private void UpdateTurnTimer(float deltaTime)
        {
            if (_currentBattleState == BattleState.Paused)
                return;
            
            _timeUntilNextTurnTick -= deltaTime;
            if (_timeUntilNextTurnTick <= 0)
            {
                ProcessTurnActions(deltaTime);
                _timeUntilNextTurnTick = TurnTickDuration;
            }
        }

        private void ProcessTurnActions(float deltaTime)
        {
            _skillCooldownService.CooldownTick(deltaTime);
            _hasteService.ReplenishHasteTick();
            ProcessReadyEntities();

            if (_hasteService.EntityIsReadyOnNextTick())
                _currentBattleState = BattleState.Paused;
        }

        private void ProcessReadyEntities()
        {
            foreach (IEntity entity in _entityRegister.AllActiveEntities())       
            {
                if (entity.IsReadyForTurn())
                {
                    PerformEntityAction(entity);
                }
            }
        }

        private void PerformEntityAction(IEntity entity)
        {
            // TODO: skill solver and decision maker
            // TODO: entity must reset his current Haste to zero

            var entityAction = new EntityAction()
            {
                Caster = entity
            };
            
            _skillSolver.ProcessEntityAction(entityAction);
        }
    }
}