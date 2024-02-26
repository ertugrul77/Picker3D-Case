using _Game.Scripts.UI.UISubs;
using UnityEngine;

namespace _Game.Scripts.UI.UISubs
{

    public class GenericUIBase : MonoBehaviour
    {
    }
    
    public abstract class SubUIBase<T, C> : GenericUIBase where C : Component
    {
        [HideInInspector] [SerializeField] protected C component;

        private void OnValidate()
        {
            component = GetComponent<C>();
        }

        public abstract void SetValue(T value);
    }
}

