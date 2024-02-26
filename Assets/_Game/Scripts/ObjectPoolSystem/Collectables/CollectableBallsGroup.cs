using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.ObjectPoolSystem.Collectables
{
    public class CollectableBallsGroup : MonoBehaviour
    {
        
        public bool IsActive;
        public BallShapeType ballShapeType;

        private List<CollectableBase> _collectableBases;
        private Vector3 _spawnPosition;
        private List<Vector3> _positions;
        
        public void Initialize()
        {
            _collectableBases = GetComponentsInChildren<CollectableBase>(true).ToList();
            
            _spawnPosition = transform.position;
            _positions = new List<Vector3>(_collectableBases.Count);
            foreach (var t in _collectableBases)
            {
                _positions.Add(t.transform.localPosition);
            }
            
            SetActivity(false);
        }

        public void SetActivity(bool activity)
        {
            IsActive = activity;
            gameObject.SetActive(activity);
            IsKinematic(activity);
            if (activity)
            {
                transform.position = _spawnPosition;
                for (int i = 0; i < _collectableBases.Count; i++)
                {
                    _collectableBases[i].transform.localPosition = _positions[i];
                    _collectableBases[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
                    _collectableBases[i].SetActivity(true);
                }
            }
        }

        private void IsKinematic(bool check)
        {
            foreach (var collectableBase in _collectableBases)
            {
                collectableBase.GetComponent<Rigidbody>().isKinematic = !check;
            }
        }
    }
    
    public enum BallShapeType
    {
        Horizontal,
        Vertical,
        VShape,
        ReverseVShape,
        Zigzag
    }
}
