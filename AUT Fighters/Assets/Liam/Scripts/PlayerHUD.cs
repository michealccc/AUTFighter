using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public Slider healthBar;
    public Image roundCounter;
    public Image portrait;
    public TextMeshProUGUI name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPortrait(CharacterController character)
    {
        portrait.sprite = character.charactePortrait;
    }

    public void SetName(CharacterController character)
    {
        name.text = character.characterName;
    }

    public void ResetHealthBar()
    {
        healthBar.value = 100;
    }

    public void UpdateHealthBar(float playerHealth)
    {
        healthBar.normalizedValue = playerHealth;
    }

    public void ResetRoundCounter()
    {

    }

    public void UpdateRoundCounter()
    {

    }
}
