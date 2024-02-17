using System;
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
            AddGroupedElementsToDictionary(_cubes, _cubesMapping, cube => cube.BattlefieldPart);
            AddGroupedElementsToDictionary(_fenceSlots, _fenceSlotsMapping, slot => slot.SideType);
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
        
        private void AddGroupedElementsToDictionary<TElement, TKey>(IEnumerable<TElement> elements, Dictionary<TKey, List<TElement>> dictionary, Func<TElement, TKey> keySelector)
        {
            IEnumerable<IGrouping<TKey, TElement>> groupedElements = elements.GroupBy(keySelector);
            
            foreach (IGrouping<TKey, TElement> group in groupedElements)
                EnumerableExtension.AddListToDictionary(group.Key, dictionary, group.ToList());
        }
    }
}