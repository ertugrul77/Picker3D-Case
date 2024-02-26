using _Game.Scripts.Helpers;
using _Game.Scripts.Managers;
using _Game.Scripts.Player;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.ObjectPoolSystem.Platforms
{
    public class Collector : Platform
    {
        public override PlatformType PlatformType => PlatformType.Collector;
        
        [SerializeField] private Gate gateLeft;
        [SerializeField] private Gate gateRight;
        [SerializeField] private CollectableCounter collectableCounter;
        
        private int _target;


        public override void Initialize()
        {
            base.Initialize();
            
            EventManager.Instance.OnGameStateChanged += EventManagerOnGameStateChanged;
        }

        private void EventManagerOnGameStateChanged(GameState obj)
        {
            if (obj == GameState.GameSuccess || obj == GameState.GameLose)
            {
                Reset();
            }
        }

        public void SetTarget(int value)
        {
            _target = value;
            collectableCounter.Initialize(_target);
        }

        private void CheckContinue(PlayerBase player)
        {
            var counter = collectableCounter.GetCounter();
            if (counter >= _target)
            {
                collectableCounter.Success();
                gateLeft.transform.DORotate(new Vector3(0,0,90), 1f);
                gateRight.transform.DORotate(new Vector3(0,0,90), 1f).OnComplete(()=>
                {
                    EventManager.Instance.Collector();
                });
            }
            else
            {
                DebugLog.Log("Game Failed!");
                EventManager.Instance.GameLose();
            }
        }

        private void Reset()
        {
            gateLeft.transform.eulerAngles = new Vector3(0,0,0);
            gateRight.transform.eulerAngles = new Vector3(0,0,180);
        }


        private const float WaitTime = 2f;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerPhysicController player)) return;
            
            player.PushCollectables();
            Timer.Instance.TimerWait(WaitTime, ()=> CheckContinue(player.GetComponent<PlayerBase>()));
        }
    }
}
