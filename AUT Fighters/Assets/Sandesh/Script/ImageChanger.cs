using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Button;


public class ImageChanger : MonoBehaviour
{
    //This script changes the character sprite image according to the character selected
    public static int CharlieCharacter = 0;
    public static int CharlieCharacterOtherway = 1;

    public static int LiamCharacter = 2;
    public static int MichealCharacter = 3;

    public static int SanCharacter = 4;
    public static int SanCharacterOtherway = 5;


    public string resourceName = "PlayerImages";
    public Sprite[] backgrounds;


    void Awake()
    {
        if (resourceName != "")
            backgrounds = Resources.LoadAll<Sprite>(resourceName);
    }

    //Player 1 San image
    public void SanCharacterChanger()
    {
        GameObject.Find("SelectedFirstCharacter").GetComponent<Image>().sprite = backgrounds[SanCharacter];


    }

    //Player 1 Micheal Image

    public void MichealCharacterChanger()
    {

        GameObject.Find("SelectedFirstCharacter").GetComponent<Image>().sprite = backgrounds[MichealCharacter];

    }

    //Player 1 Charlie image
    public void CharlieCharacterChanger()
    {
        GameObject.Find("SelectedFirstCharacter").GetComponent<Image>().sprite = backgrounds[CharlieCharacter];


    }

    //Player 1 Liam image
    public void LiamCharacterChanger()
    {
        GameObject.Find("SelectedFirstCharacter").GetComponent<Image>().sprite = backgrounds[LiamCharacter];


    }


    //Player 2 San image
    public void SanCharacterChanger2()
    {
        GameObject.Find("SelectedSecondCharacter").GetComponent<Image>().sprite = backgrounds[SanCharacterOtherway];


    }

    //Player 2 Micheal Image

    public void MichealCharacterChanger2()
    {

        GameObject.Find("SelectedSecondCharacter").GetComponent<Image>().sprite = backgrounds[MichealCharacter];

    }

    //Player 2 Charlie image
    public void CharlieCharacterChanger2()
    {
        GameObject.Find("SelectedSecondCharacter").GetComponent<Image>().sprite = backgrounds[CharlieCharacterOtherway];


    }

    //Player 2 Liam image
    public void LiamCharacterChanger2()
    {
        GameObject.Find("SelectedSecondCharacter").GetComponent<Image>().sprite = backgrounds[LiamCharacter];


    }



}
