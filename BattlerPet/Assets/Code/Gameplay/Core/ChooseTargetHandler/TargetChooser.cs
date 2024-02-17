using System;
using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public class TargetChooser : ITargetChooser
    {
        private readonly IEntityRegister _entityRegister;

        public TargetChooser(IEntityRegister entityRegister)
        {
            _entityRegister = entityRegister;
        }

        public IEnumerable<string> AvailableTargetsFor(string casterId, TargetType targetType)
        {
            switch (targetType)
            {
                case TargetType.Enemy:
                case TargetType.AllEnemies:
                    return _entityRegister.EnemiesOf(casterId);

                case TargetType.Ally:
                case TargetType.AllAllies:
                    return _entityRegister.AlliesOf(casterId);

                case TargetType.Self:
                    return new[] { casterId };
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetType), targetType, $"Invalid value for {nameof(targetType)}: {targetType}");
            }
        }
    }
}