using UnityEngine;
using CodeBase.Infrastructure.Observables;

namespace Code
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            var obs = new Observable<int>(123);

            User user = new User(obs);
            
            obs.OnChanged += (o, oldVal, newVal) => Debug.Log("changed from " + oldVal + " to " + newVal);
            obs.Value = 456; // dispatches OnChanged
        }
    }
}