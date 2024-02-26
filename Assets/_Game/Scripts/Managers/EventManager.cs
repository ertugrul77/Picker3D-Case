using System;
using _Game.Scripts.Helpers;

namespace _Game.Scripts.Managers
{

    public class EventManager : GenericSingleton<EventManager>
    {
        public event Action<GameState> OnGameStateChanged;
        public void ChangeGameState(GameState state) => OnGameStateChanged?.Invoke(state);
        
        public event Action OnGameStarted;
        public void StartGame() => OnGameStarted?.Invoke();
        
        public event Action OnGameSuccess;
        public void GameSuccess() => OnGameSuccess?.Invoke();
        
        public event Action OnGameLose;
        public void GameLose() => OnGameLose?.Invoke();
        
        public event Action OnCollector;
        public void Collector() => OnCollector?.Invoke();
        
        public event Action OnRetriedLevel;
        public void RetriedLevel() => OnRetriedLevel?.Invoke();

    }
}