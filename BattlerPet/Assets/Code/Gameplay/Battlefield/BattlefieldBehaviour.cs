using UnityEngine;
using System.Linq;
using Code.Data.Battlefield;
using System.Collections.Generic;

namespace Code.Gameplay.Battlefield
{
    public class BattlefieldBehaviour : MonoBehaviour
    {
        [field: SerializeField] public List<SlotBehaviour> SlotBehaviours { get; private set; }
        [SerializeField] private List<Cube> _cubes;
        [SerializeField] private List<FenceSlot> _fenceSlots;
        [SerializeField] private List<DecorSpot> _decorSpots;

        private readonly Dictionary<SideType, List<FenceSlot>> _fenceSlotsMapping = new();
        private readonly Dictionary<BattlefieldPart, List<Cube>> _cubesMapping = new();

        public void Initialize()
        {
            _cubes.ForEach(ProcessCube);
            _fenceSlots.ForEach(ProcessFenceSlot);
        }

        public void SetTopCubesMaterials(List<Material> materials)
        {
            List<Cube> topCubes = _cubesMapping[BattlefieldPart.TopCube];
            foreach (Cube cube in topCubes)
                cube.ChangeMaterial(materials.PickRandom());
        }

        public void SetBotCubesMaterials(Material material)
        {
            List<Cube> botCubes = _cubesMapping[BattlefieldPart.BotCube];
            foreach (Cube cube in botCubes)
                cube.ChangeMaterial(material);
        }

        public IEnumerable<Vector3> GetDecorSpawnPositions() => 
            _decorSpots.Select(x => x.GetPosition).ToList();

        public List<Vector3> GetFenceSpawnPositions(SideType sideType) => 
            _fenceSlotsMapping[sideType].Select(x => x.GetPosition).ToList();

        private void ProcessCube(Cube cube)
        {
            if (_cubesMapping.TryGetValue(cube.BattlefieldPart, out List<Cube> cubeList))
                cubeList.Add(cube);
            else
                _cubesMapping[cube.BattlefieldPart] = new List<Cube> { cube };
        }

        private void ProcessFenceSlot(FenceSlot slot)
        {
            if (_fenceSlotsMapping.TryGetValue(slot.SideType, out List<FenceSlot> slots))
                slots.Add(slot);
            else
                _fenceSlotsMapping[slot.SideType] = new List<FenceSlot> { slot };
        }
    }
}