using System.Linq;
using Code.Data.Battlefield;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    public class SlotSetup
    {
        private readonly Dictionary<SideType, List<SlotBehaviour>> _slotsMapping = new();

        public SlotSetup(IEnumerable<SlotBehaviour> slots)
        {
            IEnumerable<IGrouping<SideType, SlotBehaviour>> groupedCubes =  slots.GroupBy(x => x.Side);

            foreach (IGrouping<SideType, SlotBehaviour> group in groupedCubes)
                EnumerableExtension.AddListToDictionary(group.Key, _slotsMapping, group.ToList());
        }

        public List<SlotBehaviour> GetSlotForSide(SideType side) => 
            _slotsMapping[side];
    }
}