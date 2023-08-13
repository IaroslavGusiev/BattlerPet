using UnityEngine;

namespace Code.Infrastructure
{
    public interface IBattlefieldFactory
    {
        GameObject CreateBattlefieldCube(string path, Vector3 at, Transform under);
    }
}