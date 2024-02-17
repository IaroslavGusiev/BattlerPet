using Code.Data.Battlefield;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    public interface IBattlefield
    {
        UniTask Initialize();
        IEnumerable<SlotBehaviour> GetSlotForSide(SideType side);
    }
}