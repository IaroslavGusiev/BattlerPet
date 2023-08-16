using UnityEngine;

namespace Code.Infrastructure
{
    public interface IBattlefieldFactory
    {
        GameObject CreateBattlefieldItem(string path, Vector3 at, Transform under);
    }
}