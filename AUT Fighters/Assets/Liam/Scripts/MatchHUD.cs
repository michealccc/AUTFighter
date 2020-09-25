using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchHUD : MonoBehaviour
{
    public PlayerHUD p1HUD;
    public PlayerHUD p2HUD;
    // Start is called before the first frame update
    void Start()
    {
        ResetPlayerHUDs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPlayerHUDs()
    {
        p1HUD.ResetHealthBar();
        p1HUD.ResetSuperBar();
        p1HUD.ResetRoundCounter();

        p2HUD.ResetHealthBar();
        p2HUD.ResetSuperBar();
        p2HUD.ResetRoundCounter();
    }

    public void SetupPlayerProfiles(CharacterController p1Con, CharacterController p2Con)
    {
        p1HUD.SetName(p1Con);
        p1HUD.SetPortrait(p1Con);

        p2HUD.SetName(p2Con);
        p2HUD.SetPortrait(p2Con);
    }
}
