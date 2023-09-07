using UnityEngine;
using Code.Gameplay.Battlefield;

namespace Code.Infrastructure
{
    public interface IBattlefieldFactory
    {
        BattlefieldBehaviour CreateBattlefieldBehaviour(string path);
        Cube CreateBattlefieldItem(string path, Vector3 at, Transform under);
    }
}