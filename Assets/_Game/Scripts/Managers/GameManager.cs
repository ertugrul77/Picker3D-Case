using System;
using _Game.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Game.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameStateVariable gateState;
        private LevelGenerator _levelGenerator;
        private PlayerBase _playerBase;
        private EventManager _eventManager;
        
        [Inject]
        private void OnInstaller(LevelGenerator levelGenerator, PlayerBase playerBase)
        {
            _levelGenerator = levelGenerator;
            _playerBase = playerBase;
            
            AssetManager.Instance.LoadPlatforms();
            _playerBase.Initialize();
        }
        

        private void Start()
        {
            InitializeGame();
            Application.targetFrameRate = 60;
        }

        private void InitializeGame()
        {
            _levelGenerator.GenerateLevels();

            _eventManager = EventManager.Instance;
            
            _eventManager.OnGameStarted += EventManager_OnGameStarted;
            _eventManager.OnGameSuccess += EventManager_OnGameSuccess;
            _eventManager.OnGameLose += EventManager_OnGameLose;
            _eventManager.OnRetriedLevel += EventManager_OnRetriedLevel;
        }

        private void EventManager_OnRetriedLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        private void EventManager_OnGameStarted() { BroadcastChange(GameState.InGame); }
        private void EventManager_OnGameSuccess() { BroadcastChange(GameState.GameSuccess); }
        private void EventManager_OnGameLose() { BroadcastChange(GameState.GameLose); }
        private void BroadcastChange(GameState state)
        {
            gateState.ChangeStateTo(state);
            EventManager.Instance.ChangeGameState(state);
        }
    }
}
