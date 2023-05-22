using UnityEngine;

namespace Code.Services
{
    public class StaticDataService : IStaticDataService
    {
        public void Initialize()
        {
            Debug.Log("Static data loaded");
        }
    }
}