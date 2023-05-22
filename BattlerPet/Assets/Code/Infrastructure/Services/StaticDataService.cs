using UnityEngine;

namespace Code.Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        public void Initialize()
        {
            Debug.Log("Static data loaded");
        }
    }
}