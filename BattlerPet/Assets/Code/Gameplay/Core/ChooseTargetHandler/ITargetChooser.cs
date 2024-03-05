using Code.StaticData.Gameplay;
using System.Collections.Generic;

namespace Code.Gameplay.Core
{
    public interface ITargetChooser
    {
        IEnumerable<string> AvailableTargetsFor(string casterId, TargetType targetType);
    }
}