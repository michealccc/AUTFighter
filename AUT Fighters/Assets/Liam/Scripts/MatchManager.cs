using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class MatchManager : MonoBehaviour
{
    public MatchHUD matchHUD;
    public CharacterController[] characters;
    public CharacterController p1;
    public CharacterController p2;
    public Transform p1Spawn;
    public Transform p2Spawn;
    public Animator hudAnimator;
    public CameraController camera;
    public TextMeshProUGUI gameEndText;

    private bool gameEnded = false;

    [SerializeField]
    private bool roundEnded;
    [SerializeField]
    private int p1Score;
    [SerializeField]
    private int p2Score;

    // Start is called before the first frame update
    void Awake()
    {
        
        CharacterChoice.p1Character = Characters.CHARLIE;
        CharacterChoice.p2Character = Characters.SAN;
        p1Score = 0;
        p2Score = 0;
        IntializeCharacters();
        ResetRound();
        //RoundStart();           //Temporary here
        matchHUD.ResetPlayerHUDs();
        matchHUD.SetupPlayerProfiles(p1, p2);
        camera.p1Pos = p1.transform;    //Set up character reference for camera
        camera.p2Pos = p2.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Probably could just run these within the characters?
        CheckRoundEnd();
        //HandleRoundEnd();   //Or run this in the Check round End
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

            if (character.characterID == CharacterChoice.p2Character)
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
        hudAnimator.Play("ReadyText");
        roundEnded = false;

        ResetCharacter(p1);
        ResetCharacter(p2);
        p1.transform.position = p1Spawn.position;
        p2.transform.position = p2Spawn.position;
        camera.ResetCamera();
    }

    private void ResetCharacter(CharacterController player)
    {
        player.ChangeState(new RoundStartState());
        player.stats.ResetHp();
        player.stats.ResetSuperMeter();
        player.anim.SetBool("IsKO", false);
    }

    public void RoundStart()
    {
        roundEnded = false;
        p1.ChangeState(new IdleState());
        p2.ChangeState(new IdleState());
    }

    public void GameEnd()
    {
        Debug.Log("Game ended");
        if(p1Score > p2Score)
        {
            gameEndText.SetText("Player 1 WINS!");
        }
        else
        {
            gameEndText.SetText("Player 2 WINS!");
        }
        hudAnimator.SetBool("IsGameEnded", true);
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
                    matchHUD.p2HUD.UpdateRoundCounter(p2Score);
                    //Also update round counter
                }
                else if (p1.stats.currentHp > p2.stats.currentHp)    //Player 2 loses round
                {
                    p2.OnKO();
                    p1.OnVictory();
                    p1Score++;
                    matchHUD.p1HUD.UpdateRoundCounter(p1Score);
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
                if(p1Score == 2 || p2Score == 2)
                {
                    gameEnded = true;
                }
                HandleRoundEnd();
            }
        }
    }

    private void HandleRoundEnd()
    {
        Debug.Log("ended round");
        hudAnimator.Play("KOText");
        if(gameEnded)
        {
            hudAnimator.SetBool("IsGameEnded", true);
            GameEnd();
        }
    }

    private void UpdateHUD()
    {
        //Update player 1 stats UI
        matchHUD.p1HUD.UpdateHealthBar(p1.stats.currentHp / p1.stats.maxHp);
        matchHUD.p1HUD.UpdateSuperBar(p1.stats.currentSuperMeter);
        //Update player 2 stats UI
        matchHUD.p2HUD.UpdateHealthBar(p2.stats.currentHp / p2.stats.maxHp);
        matchHUD.p2HUD.UpdateSuperBar(p2.stats.currentSuperMeter);
    }
}
