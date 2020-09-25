using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchManager : MonoBehaviour
{
    public MatchHUD matchHUD;
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
    void Awake()
    {
        rEndTimeCurrent = roundEndTimer;
        p1Score = 0;
        p2Score = 0;
        IntializeCharacters();
        ResetRound();
        RoundStart();           //Temporary here
        matchHUD.ResetPlayerHUDs();
        matchHUD.SetupPlayerProfiles(p1, p2);
        camera.p1Pos = p1.transform;    //Set up character reference for camera
        camera.p2Pos = p2.transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRoundEnd();
        HandleRoundEnd();   //Or run this in the Check round End
        UpdateHUD();
    }

    private void IntializeCharacters()
    {
        //Instantiate the characters based on what the players chose
        foreach(CharacterController character in characters)
        {
            if(character.characterID == CharacterChoice.p1Character)
            {
                p1 = Instantiate(character, transform.position, transform.rotation);
                //p1.GetComponent<PlayerInput>().defaultActionMap = "Player1";
                p1.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player1");
                //p1.inputs.AssignInputs();
                Debug.Log("Player one default map: " + p1.GetComponent<PlayerInput>().defaultActionMap);
            }

            if(character.characterID == CharacterChoice.p2Character)
            {
                p2 = Instantiate(character, transform.position, transform.rotation);
               // p2.GetComponent<PlayerInput>().defaultActionMap = "Player2";
                p2.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player2");
                //p2.inputs.AssignInputs();
                Debug.Log("Player two default map: " + p2.GetComponent<PlayerInput>().defaultActionMap);
            }
        }

        p1.opponent = p2;
        p2.opponent = p1;
    }

    public void ResetRound()
    {
        Time.timeScale = 1.0f;

        ResetCharacter(p1);
        ResetCharacter(p2);
        p1.transform.position = p1Spawn.position;
        p2.transform.position = p2Spawn.position;
        camera.ResetCamera();

        RoundStart();
    }

    private void ResetCharacter(CharacterController player)
    {
        player.ChangeState(new RoundStartState());
        player.stats.ResetHp();
        player.stats.ResetSuperMeter();
        //player.transform.position = p1Spawn.position;
        player.anim.SetBool("IsKO", false);
    }

    public void RoundStart()
    {
        p1.ChangeState(new IdleState());
        p2.ChangeState(new IdleState());
    }

    private void CheckRoundEnd()
    {

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

    private void UpdateHUD()
    {
        matchHUD.p1HUD.UpdateHealthBar(p1.stats.currentHp / p1.stats.maxHp);
        matchHUD.p2HUD.UpdateHealthBar(p2.stats.currentHp / p2.stats.maxHp);
    }
}
