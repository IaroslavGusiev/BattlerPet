using Sirenix.Utilities;
using Code.Data.Battlefield;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    public class SlotSetup
    {
        private readonly Dictionary<SideType, List<SlotBehaviour>> _slotsMapping = new();
        
        public SlotSetup(IEnumerable<SlotBehaviour> slots) => 
            slots.ForEach(ProcessSlot);

        private void ProcessSlot(SlotBehaviour slot)
        {
            if (_slotsMapping.TryGetValue(slot.Side, out List<SlotBehaviour> slots))
                slots.Add(slot);
            else
                _slotsMapping[slot.Side] = new List<SlotBehaviour> { slot };
        }
    }
}