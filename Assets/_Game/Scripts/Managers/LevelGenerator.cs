using _Game.Scripts.Helpers;
using _Game.Scripts.ObjectPoolSystem;
using _Game.Scripts.ObjectPoolSystem.Platforms;
using _Game.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Managers
{
    public class LevelGenerator : MonoBehaviour
    {
        private ObjectPoolManager _objectPoolManager;
        private PlayerBase _playerBase;
        private int _levelIndex;
        
        [Inject]
        private void OnInstaller(PlayerBase playerBase, ObjectPoolManager objectPoolManager)
        {
            _playerBase = playerBase;
            _objectPoolManager = objectPoolManager;

            var currentLevel = PlayerPrefs.GetInt("LevelIndex");
            _levelIndex = currentLevel == 0 ? 1 : currentLevel;
            
            EventManager.Instance.OnGameStateChanged += EventManager_OnGameStateChanged;
            
        }
        
        private void EventManager_OnGameStateChanged(GameState obj)
        {
            if (obj == GameState.GameSuccess)
            {
                GameSuccess();
            }
        }

        private void GameSuccess()
        {
            Timer.Instance.TimerWait(1f, () =>
            {
                GenerateNextLevel();
                _levelIndex++;
                PlayerPrefs.SetInt("LevelIndex", _levelIndex);
            });
        }
        
        private float _pointerZ = -5f;
        private float _ballPointerZ;
        public void GenerateLevels()
        {
            if (_levelIndex <= 0) { _levelIndex = 1; }
            
            _playerBase.transform.position = Vector3.forward * -_pointerZ;

            for (int i = _levelIndex; i < _levelIndex + 3; i++)
            {
               var levelData = AssetManager.Instance.LoadLevel(i);
               
               var platformList = levelData.platformDataList;
               foreach (var platformData in platformList)
               {
                   var platform = _objectPoolManager.GetPlatform(platformData.platformType);
                   platform.transform.position = Vector3.forward * _pointerZ;
                   _pointerZ += platform.transform.localScale.z;

                   if (platform.PlatformType == PlatformType.Collector)
                   {
                       platform.GetComponent<Collector>()?.SetTarget(platformData.collectCount);
                   }
                   
                   var ballList = platformData.ballCollectableDataList;
                   if (platform.PlatformType == PlatformType.Simple)
                   {
                       if (ballList == null) continue;
                       foreach (var ball in ballList)
                       {
                           var ballObj = _objectPoolManager.GetBall(ball.ballShapeType);
                           ballObj.transform.position = ball.position + platform.transform.position;
                       }
                   }    
               }
            }
        }
        private void GenerateNextLevel()
        {
                var levelData = AssetManager.Instance.LoadLevel(_levelIndex+3);
               
                var platformList = levelData.platformDataList;
                foreach (var platformData in platformList)
                {
                    var platform = _objectPoolManager.GetPlatform(platformData.platformType);
                    platform.transform.position = Vector3.forward * _pointerZ;
                    _pointerZ += platform.transform.localScale.z;

                    if (platform.PlatformType == PlatformType.Collector)
                    {
                        platform.GetComponent<Collector>()?.SetTarget(platformData.collectCount);
                    }
                   
                    var ballList = platformData.ballCollectableDataList;
                    if (platform.PlatformType == PlatformType.Simple)
                    {
                        if (ballList == null) continue;
                        foreach (var ball in ballList)
                        {
                            var ballObj = _objectPoolManager.GetBall(ball.ballShapeType);
                            ballObj.transform.position = ball.position + platform.transform.position;
                        }
                    }    
                }
        }
    }
}
