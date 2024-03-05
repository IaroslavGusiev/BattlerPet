using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.ScreenServiceSpace
{
    public class TestScreenWithArgs : BaseScreen<int>
    {
        [SerializeField] private Text _text;
        
        public override void SetupOnInstantiate(int arg)
        {
            _text.text = arg.ToString();
            Debug.Log($"<color=yellow> {arg} </color>");
        }
    }
}