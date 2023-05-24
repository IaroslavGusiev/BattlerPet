using CodeBase.Infrastructure.Observables;
using UnityEngine;

namespace Code
{
    public class User
    {
        public User(Observable<int> observable)
        {
            observable.OnChanged += Test;
        }

        private void Test(Observable<int> arg1, int arg2, int arg3)
        {
            Debug.Log("<color=yellow>arg1</color>" + arg1);
            Debug.Log("<color=green>arg2</color>" + arg2);
            Debug.Log("<color=yellow>arg3</color>" + arg3);
        }
    }
}