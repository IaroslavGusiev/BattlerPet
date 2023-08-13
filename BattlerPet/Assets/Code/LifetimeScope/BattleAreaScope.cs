using VContainer;
using UnityEngine;
using VContainer.Unity;
using Code.Gameplay.Battlefield;

namespace Code.LifetimeScopes
{
    public class BattleAreaScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("<color=yellow>BattleAreaScope</color>");
            builder.RegisterEntryPoint<Battlefield>().AsSelf();
        }
    }
}