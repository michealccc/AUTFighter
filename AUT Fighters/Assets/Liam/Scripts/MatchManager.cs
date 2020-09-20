using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public CharacterController p1;
    public CharacterController p2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckRoundEnd();
    }

    private void CheckRoundEnd()
    {
        if (p1.stats.currentHp <= 0)     //Player 1 KO
        {
            p1.OnKO();
            p2.OnVictory();
            Time.timeScale = 0.5f;
        }
        else if (p2.stats.currentHp <= 0)    //Player 2 KO
        {
            p2.OnKO();
            p1.OnVictory();
            Time.timeScale = 0.5f;
        }
    }
}
