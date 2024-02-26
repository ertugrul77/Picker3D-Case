using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Player
{
    public class PlayerPhysicController : MonoBehaviour
    {
        [SerializeField] private Transform pusher;
        
        private PlayerPhysicManager _playerPhysicManager;
        private PlayerMovementController _playerMovementController;

        private Vector3 _pusherStartPosition;
        private const float PusherTargetMovePosition = 1.75f;
        
        
        public void Initialize(PlayerPhysicManager playerPhysicManager, PlayerMovementController playerMovementController)
        {
            _playerPhysicManager = playerPhysicManager;
            _playerMovementController = playerMovementController;

            _pusherStartPosition = pusher.transform.localPosition;
            pusher.gameObject.SetActive(false);
        }

        public void PushCollectables()
        {
            _playerMovementController.SetActivity(false);

            
            //Push with an object
            /*pusher.gameObject.SetActive(true);
            var pusherRb = pusher.GetComponent<Rigidbody>();
            var targetPos = transform.position.z + PusherTargetMovePosition;
            pusherRb.DOMoveZ(targetPos, 0.5f).OnComplete(ResetPusher);*/
            
            
            //Push every cube itself
            var collectables = _playerPhysicManager.GetCollectables();
            foreach (var collectable in collectables)
            {
                collectable.Push();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CollectableBase collectableBase))
            {
                _playerPhysicManager.AddCollectable(collectableBase);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CollectableBase collectableBase))
            {
                _playerPhysicManager.RemoveCollectable(collectableBase);
            }
        }

        private void ResetPusher()
        {
            pusher.transform.localPosition = _pusherStartPosition;
            pusher.gameObject.SetActive(false);
        }
    }
}
