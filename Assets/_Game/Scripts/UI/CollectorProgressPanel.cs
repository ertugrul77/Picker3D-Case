using System.Collections.Generic;
using System.Linq;
using _Game.Scripts.Managers;
using _Game.Scripts.ObjectPoolSystem.Platforms;
using _Game.Scripts.Player;
using UnityEngine;
using Zenject;
using TMPro;

namespace _Game.Scripts.UI
{
    public class CollectorProgressPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private TextMeshProUGUI nextLevelText;
        [SerializeField] private Progress progressPrefab;
        
        private readonly List<Progress> _progressList = new List<Progress>();
        private PlayerBase _playerBase;
        private CanvasGroup _canvasGroup;
        private int _point;
        private int _currentLevel;
        private int _currentProgress;
        private int _collectorCount;

        [Inject]
        private void OnInstaller(PlayerBase playerBase)
        {
            _playerBase = playerBase;
            Initialize();
        }
        
        private void Initialize()
        {
            var platformDataList = AssetManager.Instance.LoadLevel(1).platformDataList;

            foreach (var progress in from platformData in platformDataList 
                     where platformData.platformType == PlatformType.Collector 
                     select Instantiate(progressPrefab.gameObject, transform) 
                     into progressObject 
                     select progressObject.GetComponent<Progress>())
            {
                _progressList.Add(progress);
                progress.Initialize();
            }
            
            _currentProgress = 0;
            _canvasGroup = GetComponent<CanvasGroup>();
            
            var levelIndex = PlayerPrefs.GetInt("LevelIndex");
            _currentLevel = levelIndex == 0 ? 1 : levelIndex;
            SetLevelTexts();
            
            //GameEvents.SubscribeEvent(GameEventType.Collector, ProgressSuccess);
            EventManager.Instance.OnCollector += EventManager_OnCollector;
            EventManager.Instance.OnGameStateChanged += EventManager_OnGameStateChanged;
            
        }

        private void EventManager_OnCollector()
        {
            _progressList[_currentProgress].SetActivity(true);
            _currentProgress++;
        }
        
        private void EventManager_OnGameStateChanged(GameState obj)
        {
            switch (obj)
            {
                case GameState.GameSuccess:
                    SetCanvasGroup(1);
                    _currentLevel++;
                    SetLevelTexts();
                    break;
                case GameState.GameLose:
                    SetCanvasGroup(0);
                    break;
            }
        }

        

        private void SetCanvasGroup(float alpha)
        {
            _canvasGroup.alpha = alpha;
        }

        private void SetLevelTexts()
        {
            currentLevelText.text = _currentLevel.ToString();
            nextLevelText.text = (_currentLevel + 1).ToString();
        }
    }
}
