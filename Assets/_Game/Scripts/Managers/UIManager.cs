using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform uiRoot;

    private void Awake()
    {
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        var childCount = uiRoot.childCount;

        for (var i = 0; i < childCount; i++)
        {
            uiRoot.GetChild(i).GetComponent<RootUIBase>().Initialize();
        }
    }
}
