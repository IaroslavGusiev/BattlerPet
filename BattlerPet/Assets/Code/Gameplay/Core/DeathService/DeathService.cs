using UniRx;
using System;
using UnityEngine;
using Code.Gameplay.Entity;

namespace Code.Gameplay.Core
{
    public class DeathService : IDeathService, IDisposable
    {
        private readonly IEntityRegister _entityRegister;
        private readonly CompositeDisposable _disposable = new();

        public DeathService(IEntityRegister entityRegister)
        {
            _entityRegister = entityRegister;
        }

        public void RegisterEntity(IReactiveModel reactiveModel)
        {
            reactiveModel.UponDeath
                .SkipLatestValueOnSubscribe()
                .Subscribe(HandleDeathOfEntity)
                .AddTo(_disposable);
        }

        public void Dispose() => 
            _disposable?.Dispose();

        private void HandleDeathOfEntity(string entityId)
        {
            Debug.Log("<color=yellow>1111</color>");
            IEntity entity = _entityRegister.GetEntity(entityId);
            entity.AnimateDeath(); //  TODO: wait for anim time and disable in scene
        }
    }
}