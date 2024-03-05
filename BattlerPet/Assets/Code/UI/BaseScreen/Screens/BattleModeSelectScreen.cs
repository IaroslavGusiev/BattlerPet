using UniRx;
using System;
using UnityEngine;
using UnityEngine.UI;
using Code.StaticData;

namespace Code.UI.ScreenServiceSpace
{
    public class BattleModeSelectScreen : BaseScreen
    {
        public IObservable<BattleMode> ModeObservable => _modeSubject;
        
        [SerializeField] private Button _autoModeButton;
        [SerializeField] private Button _manualModeButton;
        private readonly Subject<BattleMode> _modeSubject = new();
        
        public override void SetupOnInstantiate()
        {
            _autoModeButton.OnClickAsObservable()
                .Subscribe(_ => OnModeButtonClick(BattleMode.Auto))
                .AddTo(this);

            _manualModeButton.OnClickAsObservable()
                .Subscribe(_ => OnModeButtonClick(BattleMode.Manual))
                .AddTo(this);
        }

        private void OnModeButtonClick(BattleMode selectedMode) => 
            _modeSubject.OnNext(selectedMode);
    }
}