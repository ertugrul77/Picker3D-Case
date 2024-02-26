using System;
using _Game.Scripts.Managers;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerBase : MonoBehaviour
    {
        
        private Camera _mainCamera;
        private Vector3 _cameraOffset;
        
        private PlayerPhysicManager _playerPhysicManager;

        private PlayerPhysicController _playerPhysicController;
        private PlayerMovementController _playerMovementController;
        
        public void Initialize()
        {
            _mainCamera = _mainCamera == null ? Camera.main : _mainCamera;
            if (_mainCamera == null) return;
            
            _cameraOffset = _mainCamera.transform.position - transform.position;

            _playerPhysicManager = new PlayerPhysicManager();

            _playerMovementController = GetComponent<PlayerMovementController>();
            _playerPhysicController = GetComponent<PlayerPhysicController>();

            _playerMovementController.Initialize(_mainCamera);

            _playerPhysicController.Initialize(_playerPhysicManager,_playerMovementController);
            
        }



        private void LateUpdate()
        {
            _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x,_mainCamera.transform.position.y,
                transform.position.z + _cameraOffset.z);
        }
    }
}