using UnityEngine;
using System.Threading.Tasks;
using Code.Gameplay.Battlefield;
using Cysharp.Threading.Tasks;

namespace Code.Infrastructure
{
    public interface IBattlefieldFactory
    {
        UniTask<BattlefieldBehaviour> CreateBattlefieldBehaviour(string prefabAddress);
        UniTask<Cube> CreateBattlefieldItem(string prefabAddress, Vector3 at, Transform parent);
    }
}