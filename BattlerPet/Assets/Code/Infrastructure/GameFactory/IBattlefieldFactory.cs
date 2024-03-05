using UnityEngine;
using Cysharp.Threading.Tasks;
using Code.Gameplay.Battlefield;

namespace Code.Infrastructure
{
    public interface IBattlefieldFactory
    {
        UniTask<BattlefieldBehaviour> CreateBattlefieldBehaviour(string prefabAddress);
        UniTask<Cube> CreateBattlefieldItem(string prefabAddress, Vector3 at, Transform parent);
    }
}