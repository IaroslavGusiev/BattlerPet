﻿using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.LifetimeScopes
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Debug.Log("<color=green>GameLifetimeScope</color>");
        }
    }
}