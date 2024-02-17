using UnityEngine;
using System.Collections;

namespace Code.UI.LoadingCurtain
{
    public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        [SerializeField] private CanvasGroup _curtain;
        private readonly YieldInstruction _fadeAlphaDelay = new WaitForSeconds(0.03f);
        
        public void Show()
        {
            gameObject.SetActive(true);
            _curtain.alpha = 1;
        }

        public void Hide() => 
            StartCoroutine(DoFadeIn());

        private IEnumerator DoFadeIn()
        {
            while (_curtain.alpha > 0)
            {
                _curtain.alpha -= 0.03f;
                yield return _fadeAlphaDelay;
            }
            gameObject.SetActive(false);
        }
    }
}