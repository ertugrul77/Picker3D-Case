using System;
using _Game.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Game.Scripts.ObjectPoolSystem.Platforms
{
    public class CollectableCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshPro countText;
        [SerializeField] private Transform simplePlatform;

        private Vector3 _simplePlatformStartPosition;
        private int _targetCountNumber;
        private int _currentCount;

        public void Initialize(int targetNumber)
        {
            
            _currentCount = 0;
            _targetCountNumber = targetNumber;
            countText.text = _currentCount + "/" + _targetCountNumber;
            simplePlatform.gameObject.SetActive(false);

            _simplePlatformStartPosition = simplePlatform.transform.position;
            
            EventManager.Instance.OnGameStateChanged += EventManagerOnGameStateChanged;
        }
        

        private void EventManagerOnGameStateChanged(GameState obj)
        {
            if (obj == GameState.GameSuccess || obj == GameState.GameLose)
            {
                Reset();
            }
        }

        private const float MoveDuration = 1f;
        public void Success()
        {
            simplePlatform.gameObject.SetActive(true);
            simplePlatform.transform.DOMoveY(0, MoveDuration);
            countText.enabled = false;
        }
        
        public int GetCounter()
        {
            var temp = _currentCount;
            _currentCount = 0;
            return temp;
        }

        private void Reset()
        {
            simplePlatform.transform.position = _simplePlatformStartPosition;
            countText.enabled = true;
            countText.text = _currentCount +"/" + _targetCountNumber;
        }

        private const float CountDuration = 1f;
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.TryGetComponent(out CollectableBase collectableObject)) return;
            
            collectableObject.SetActivity(false);
            DOVirtual.Float(_currentCount,++_currentCount, CountDuration, value =>
            {
                countText.text = Mathf.RoundToInt(value) + "/" + _targetCountNumber;
            });
        }
    }
}
