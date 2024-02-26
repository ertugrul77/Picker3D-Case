using System;
using _Game.Scripts.Managers;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float swerveSpeed;
        
        private Vector3 _mousePos;
        private Camera _mainCamera;
        private float _distanceToScreen;
        private bool _activity;

        public void Initialize(Camera mainCamera)
        {
            _mainCamera = mainCamera;
            EventManager.Instance.OnCollector += EventManagerOnCollector;
            EventManager.Instance.OnGameStateChanged += EventManager_OnGameStateChanged;
        }

        private void EventManager_OnGameStateChanged(GameState obj)
        {
            switch (obj)
            {
                case GameState.InGame:
                    SetActivity(true);
                    break;
                case GameState.GameLose:
                case GameState.GameSuccess:
                case GameState.PreGame:
                    SetActivity(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
            }
        }
        
        private void EventManagerOnCollector() { SetActivity(true); }

        public void SetActivity(bool activity)
        {
            _activity = activity;
        }
        
        private void FixedUpdate()
        {
            if(!_activity) return;
            
            if (Input.GetMouseButton(0))
            {
                var position = Input.mousePosition;
                
                _distanceToScreen = _mainCamera.WorldToScreenPoint(gameObject.transform.position).z;
                _mousePos = _mainCamera.ScreenToWorldPoint(new Vector3(position.x, position.y, _distanceToScreen ));
                
                var direction = swerveSpeed;
                direction = _mousePos.x > transform.position.x ? direction : -direction;
                
                if(Math.Abs(_mousePos.x - transform.position.x) > 0.5f)
                    transform.Translate(Time.deltaTime * direction,0,0);
            }
            transform.Translate(0,0,Time.deltaTime * forwardSpeed);
        }
    }
}