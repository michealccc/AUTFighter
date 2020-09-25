using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HUDAnimEvent : UnityEvent { };

public class HUDAnimationsScript : MonoBehaviour
{
    public HUDAnimEvent blackFadeEvent;
    public HUDAnimEvent startEvent;
    public HUDAnimEvent endFadeEvent;

    public void RunBladeFadeEvent()
    {
        blackFadeEvent.Invoke();
    }

    public void RunStartEvent()
    {
        startEvent.Invoke();
    }

    public void RunEndFadeEvent()
    {
        endFadeEvent.Invoke();
    }
}
