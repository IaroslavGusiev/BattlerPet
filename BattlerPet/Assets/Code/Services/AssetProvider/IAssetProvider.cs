using UnityEngine;
using System.Collections.Generic;

namespace Code.Services
{
    public interface IAssetProvider 
    {
        T Get<T>(string path) where T : Object;
        List<T> GetAll<T>(string path) where T : Object;
    }
}