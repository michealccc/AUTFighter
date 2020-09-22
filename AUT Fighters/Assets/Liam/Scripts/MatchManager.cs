using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public CharacterController[] characters;
    public CharacterController p1;
    public CharacterController p2;
    public Transform p1Spawn;
    public Transform p2Spawn;
    public Animator temp;
    public CameraController camera;

    public float roundEndTimer;
    private float rEndTimeCurrent;

    [SerializeField]
    private bool roundEnded;
    [SerializeField]
    private int p1Score;
    [SerializeField]
    private int p2Score;

    // Start is called before the first frame update
    void Start()
    {
        rEndTimeCurrent = roundEndTimer;
        p1Score = 0;
        p2Score = 0;
        ResetRound();
        RoundStart();           //Temporary here
    }

    // Update is called once per frame
    void Update()
    {
        CheckRoundEnd();
        HandleRoundEnd();   //Or run this in the Check round End
    }

    private void IntializeCharacters()
    {
        //Instantiate the characters based on what the players chose

    }

    public void ResetRound()
    {
        Time.timeScale = 1.0f;
        //Probably make a method that takes in a character controller and runs these so there doesn't need to be dupe code
        p1.ChangeState(new RoundStartState());
        p1.stats.ResetHp();
        p1.stats.ResetSuperMeter();
        p1.transform.position = p1Spawn.position;
        p1.anim.SetBool("IsKO", false);

        p2.ChangeState(new RoundStartState());
        p2.stats.ResetHp();
        p2.stats.ResetSuperMeter();
        p2.transform.position = p2Spawn.position;
        p2.anim.SetBool("IsKO", false);

        camera.ResetCamera();

        RoundStart();
    }

    public void RoundStart()
    {
        p1.ChangeState(new IdleState());
        p2.ChangeState(new IdleState());
    }

    private void CheckRoundEnd()
    {
        //if (p1.stats.currentHp <= 0)     //Player 1 KO
        //{
        //    p1.OnKO();
        //    //p2.OnVictory();
        //    Time.timeScale = 0.5f;
        //}
        //else if (p2.stats.currentHp <= 0)    //Player 2 KO
        //{
        //    p2.OnKO();
        //    //p1.OnVictory();
        //    Time.timeScale = 0.5f;
        //}

        //Don't actually want this to loop
        if(!roundEnded)
        {
            if (p1.stats.currentHp <= 0 || p2.stats.currentHp <= 0)
            {
                if (p1.stats.currentHp < p2.stats.currentHp)    //Player 1 loses round
                {
                    p1.OnKO();
                    p2.OnVictory();
                    p2Score++;
                    //Also update round counter
                }
                else if (p1.stats.currentHp > p2.stats.currentHp)    //Player 2 loses round
                {
                    p2.OnKO();
                    p1.OnVictory();
                    p1Score++;
                    //Also update round counter
                }
                else                                               //Double KO
                {
                    p1.OnKO();
                    p2.OnKO();
                    //Also update round counter
                }

                Time.timeScale = 0.5f;
                roundEnded = true;
            }
        }
    }

    private void HandleRoundEnd()
    {
        if(roundEnded && rEndTimeCurrent > 0)
        {
            //Play round end screen - wait for end screen to finish before reseting or time it
            rEndTimeCurrent -= Time.deltaTime;
            Debug.Log(rEndTimeCurrent);
        }
        else if(roundEnded && rEndTimeCurrent <= 0)
        {
            rEndTimeCurrent = roundEndTimer;
            roundEnded = false;
            temp.Play("BlackFade");
        }
    }
}
