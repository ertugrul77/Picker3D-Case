using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.UI
{
    public class Progress : MonoBehaviour
    {
        private Image _image;

        public bool IsActive;

        private readonly Color _deActiveColor = Color.white;
        private readonly Color _activeColor = Color.green;
        
        public void Initialize()
        {
            _image = transform.GetComponent<Image>();
            SetActivity(false);
        }

        public void SetActivity(bool activity)
        {
            IsActive = activity;
            _image.color = activity ? _activeColor : _deActiveColor;
        }
        
    }
}
