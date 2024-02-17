using Code.Gameplay.Core;
using Cysharp.Threading.Tasks;
using Code.Gameplay.Battlefield;
using Code.Infrastructure.StateMachineBase;

namespace Code.Gameplay.States
{
    public class CreateBattlefieldState : IState
    {
        private readonly IBattlefield _battlefield;
        private readonly BattleStarter _battleStarter;

        public CreateBattlefieldState(IBattlefield battlefield, BattleStarter battleStarter)
        {
            _battlefield = battlefield;
            _battleStarter = battleStarter;
        }

        public async UniTask Enter()
        {
            await _battlefield.Initialize();
            await _battleStarter.Initialize(_battlefield);
        }

        public async UniTask Exit()
        {
            
        }
    }
}