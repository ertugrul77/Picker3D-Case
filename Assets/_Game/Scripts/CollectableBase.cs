using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts
{
    public class CollectableBase : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void SetActivity(bool activity)
        {
            gameObject.SetActive(activity);
        }

        
        private const float Force = 70f;
        private const float TargetOffset = 10f;
        public void Push()
        {
            _rigidbody.DOMoveZ(transform.position.z + TargetOffset, 1f);
            //_rigidbody.AddForce(Vector3.forward * Force);
        }
    }
}