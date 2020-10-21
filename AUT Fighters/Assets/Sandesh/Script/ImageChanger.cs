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
    public static int LiamCharacter = 1;
    public static int MichealCharacter = 2;
    public static int SanCharacter = 3;

    public string resourceName = "PlayerImages";
    public Sprite[] backgrounds;


    void Awake()
    {
        if (resourceName != "")
            backgrounds = Resources.LoadAll<Sprite>(resourceName);
    }

    //Player 1 image
    public void SanCharacterChanger()
    {
        GameObject.Find("SelectedFirstCharacter").GetComponent<Image>().sprite = backgrounds[SanCharacter];


    }

    //Player 2 Image

    public void MichealCharacterChanger()
    {

        GameObject.Find("SelectedFirstCharacter").GetComponent<Image>().sprite = backgrounds[MichealCharacter];

    }

    //Player 3 image
    public void CharlieCharacterChanger()
    {
        GameObject.Find("SelectedFirstCharacter").GetComponent<Image>().sprite = backgrounds[CharlieCharacter];


    }

    //Player 4 image
    public void LiamCharacterChanger()
    {
        GameObject.Find("SelectedFirstCharacter").GetComponent<Image>().sprite = backgrounds[LiamCharacter];


    }



}
