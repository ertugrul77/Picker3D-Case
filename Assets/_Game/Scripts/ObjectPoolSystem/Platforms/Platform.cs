using UnityEngine;

namespace _Game.Scripts.ObjectPoolSystem.Platforms
{
    public abstract class Platform : MonoBehaviour
    {
        public abstract PlatformType PlatformType { get; }
        public bool IsActive;
    
        public virtual void Initialize(){ IsActive = false; }

        public void SetActivity(bool activity)
        {
            IsActive = activity;
            gameObject.SetActive(activity);
        }
    }

    public enum PlatformType
    {
        Simple,
        Collector,
        Finish
    }
}