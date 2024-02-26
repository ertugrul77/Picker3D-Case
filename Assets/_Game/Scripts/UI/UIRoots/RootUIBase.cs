using System.Collections.Generic;
using _Game.Scripts.Managers;
using _Game.Scripts.UI.UISubs;
using UnityEngine;

public class RootUIBase : MonoBehaviour
{
    public GameState gameState;

    private Canvas _canvas;
    private Dictionary<string, GenericUIBase> _genericUIs = new Dictionary<string, GenericUIBase>();

    public void Initialize()
    {
        EventManager.Instance.OnGameStateChanged += OnGameStateChangedHandler;
        _canvas = GetComponent<Canvas>();

        var childCount = transform.childCount;

        for (var i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out GenericUIBase genericUIBase))
            {
                _genericUIs.Add(genericUIBase.name, genericUIBase);
            }
        }
    }

    private void OnGameStateChangedHandler(GameState state)
    {
        SetActive(gameState == state);
    }

    private void SetActive(bool isActive)
    {
        _canvas.enabled = isActive;
    }

    public void SetSubValue<T, C>(string name, T val) where C : Component
    {
        if (_genericUIs.TryGetValue(name, out GenericUIBase genericUIBase))
        {
            SubUIBase<T, C> subUIBase = (SubUIBase<T, C>)genericUIBase;
            if (subUIBase != default) subUIBase.SetValue(val);
        }
        else
        {
            Debug.LogError("No any " + typeof(T).Name + " found with name \"" + name + "\"");
        }
    }
}

