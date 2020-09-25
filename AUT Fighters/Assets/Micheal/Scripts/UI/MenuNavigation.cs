using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    public Selectable defaultSelection;

    public bool forceSelection = false;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void LateUpdate()
    {
        if (EventSystem.current.currentSelectedGameObject == null || forceSelection)
        {
            EventSystem.current.SetSelectedGameObject(defaultSelection.gameObject);
        }
    }

    private void OnDisable()
    {
        if (forceSelection && EventSystem.current.currentSelectedGameObject == defaultSelection.gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}