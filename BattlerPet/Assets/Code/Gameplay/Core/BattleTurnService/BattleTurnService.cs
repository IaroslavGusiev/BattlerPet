using System;
using UniRx;
using Code.Services;
using System.Threading;
using Code.StaticData;
using VContainer.Unity;
using Code.Gameplay.Entity;
using Code.Gameplay.Core.AI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Code.UI.ScreenServiceSpace;
using Code.Infrastructure.UpdateRunner;

namespace Code.Gameplay.Core
{
    public class BattleTurnService : IBattleTurnService, ITickListener, IAsyncStartable, IDisposable
    {
        private const float TurnTickDuration = 0.3f;

        private readonly IArtificialIntelligence _artificialIntelligence;
        private readonly SkillCooldownService _skillCooldownService;
        private readonly IEntityRegister _entityRegister;
        private readonly HasteService _hasteService;
        private readonly ISkillSolver _skillSolver;
        private ITickSource _tickSource;
        
        private readonly IScreenService _screenService; 
        private readonly CompositeDisposable _disposables = new();
        
        private float _timeUntilNextTurnTick;
        private BattleState _currentBattleState = BattleState.NotStarted;
        private BattleMode _currentBattleMode = BattleMode.Manual;
        
        public BattleTurnService(
            IEntityRegister entityRegister, 
            HasteService hasteService, 
            SkillCooldownService skillCooldownService, 
            ISkillSolver skillSolver, 
            ITickSource tickSource, 
            IArtificialIntelligence artificialIntelligence, 
            IScreenService screenService)
        {
            _tickSource = tickSource;
            _artificialIntelligence = artificialIntelligence;
            _screenService = screenService;
            _skillSolver = skillSolver;
            _hasteService = hasteService;
            _skillCooldownService = skillCooldownService;
            _entityRegister = entityRegister;
            _tickSource.AddListener(this);
        }
        
        public async UniTask StartAsync(CancellationToken cancellation) => 
            await InitializeScreen();

        public void Tick(float deltaTime)
        {
            if (_currentBattleState != BattleState.InProgress)
                return;
            
            UpdateTurnTimer(deltaTime);
            _skillSolver.SkillDelaysTick(deltaTime); // TODO: rename this method 
            // TODO: tick SkillExecution in skill solver 
        }

        public void Dispose()
        {
            _tickSource.RemoveListener(this);
            _tickSource = null;
            _disposables.Dispose();
        }

        public void StartBattle() =>
            _currentBattleState = BattleState.InProgress;

        public void EndBattle() =>
            _currentBattleState = BattleState.Finished;

        private async Task InitializeScreen()
        {
            var screen = await _screenService.ShowScreen<BattleModeSelectScreen>(); // TODO: think about logic how must show and when. BattleModeSelector or other instance 
            screen.ModeObservable
                .Subscribe(OnModeSelected)
                .AddTo(_disposables);
        }

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
            _hasteService.IncreaseHasteTick();
            ProcessReadyEntities();

            if (_hasteService.EntityIsReadyOnNextTick())
               SetPauseIfManualMode();
        }

        private void ProcessReadyEntities()
        {
            foreach (IEntity entity in _entityRegister.AllActiveEntities())       
            {
                if (entity.IsReadyForTurn())
                {
                    PerformEntityAction(entity);
                    entity.ResetHasteToZero();
                }
            }
        }

        private void PerformEntityAction(IEntity entity)
        {
            EntityAction bestEntityAction = _artificialIntelligence.MakeBestDecision(entity);
            _skillSolver.ProcessEntityAction(bestEntityAction);
        }

        private void SetPauseIfManualMode()
        {
            if (_currentBattleMode == BattleMode.Manual)
                _currentBattleState = BattleState.Paused;
        }
        
        private void OnModeSelected(BattleMode selectedMode)
        {
            if (IsSwitchFromManualToAuto(selectedMode))
                _currentBattleState = BattleState.InProgress;
            
            _currentBattleMode = selectedMode;
        }
        
        private bool IsSwitchFromManualToAuto(BattleMode newMode) => 
            _currentBattleMode == BattleMode.Manual && newMode == BattleMode.Auto;
    }
}