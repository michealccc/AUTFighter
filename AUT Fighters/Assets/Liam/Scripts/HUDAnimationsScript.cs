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
    [HideInInspector]public string winner;

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

    public void PlayWinnerAudio()
    {
        if(winner.CompareTo("p1") == 0) //P1 wins
        {
            FindObjectOfType<AudioManager>().Play("WinnerPlayer1");
        }
        else if(winner.CompareTo("p2") == 0)    //P2 wins
        {
            FindObjectOfType<AudioManager>().Play("WinnerPlayer2");
        }
    }
}
