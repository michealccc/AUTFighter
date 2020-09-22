using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UIEvent : UnityEvent { };

public class UIEvents : MonoBehaviour
{
    public UIEvent theEvent;

    public void DoEvent()
    {
        theEvent.Invoke();
        Debug.Log(theEvent.ToString());
    }

}
