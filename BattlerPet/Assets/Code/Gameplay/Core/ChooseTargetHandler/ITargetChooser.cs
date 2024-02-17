using System.Collections.Generic;
using Code.StaticData.Gameplay;

namespace Code.Gameplay.Core
{
    public interface ITargetChooser
    {
        IEnumerable<string> AvailableTargetsFor(string casterId, TargetType targetType);
    }
}