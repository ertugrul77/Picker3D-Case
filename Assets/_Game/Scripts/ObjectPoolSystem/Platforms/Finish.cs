using _Game.Scripts.Helpers;
using _Game.Scripts.Managers;
using _Game.Scripts.Player;
using UnityEngine;

namespace _Game.Scripts.ObjectPoolSystem.Platforms
{
    public class Finish : Platform
    {
        public override PlatformType PlatformType => PlatformType.Finish;

        private const float WaitTime = 1f;
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out PlayerPhysicController playerPhysicController)) return;
        
            DebugLog.Log("Level Success!!");
            Timer.Instance.TimerWait(WaitTime, () =>
            {
                EventManager.Instance.GameSuccess();
            });
        }
    }
}