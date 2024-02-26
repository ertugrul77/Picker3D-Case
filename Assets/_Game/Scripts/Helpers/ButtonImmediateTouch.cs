using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _Game.Scripts.Helpers
{
    public class ButtonImmediateTouch : MonoBehaviour,IPointerDownHandler {
        public UnityEvent pointerDown; 

        public void OnPointerDown(PointerEventData eventData) {
            pointerDown.Invoke();
        }
    }
}