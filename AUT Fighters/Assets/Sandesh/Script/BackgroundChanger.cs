using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Button;

//This script changes the background on StageSelect UI
//On Stage Select when a stage is selected it changes the background to that stage before confirming that stage
public class BackgroundChanger : MonoBehaviour
{
    public static int AlbertPark = 0;
    public static int LectureRoom = 1;
    public static int StPaulStreet = 2;
    public static int SymondsStreet = 3;
    public static int WellesleyStreet = 4;
    public string resourceName = "Backgrounds";
    public Sprite[] backgrounds;


    void Awake()
    {
        if (resourceName != "")
            backgrounds = Resources.LoadAll<Sprite>(resourceName);
    }
    //Albert Park
    public void AlbertParkImageChanger()
    {
        GameObject.Find("Background").GetComponent<Image>().sprite = backgrounds[AlbertPark];
        MatchChoices.chosenStage = Stages.FOUNTAIN;

    }

    //Lecture Room
    public void LectureRoomImageChanger()
    {
        GameObject.Find("Background").GetComponent<Image>().sprite = backgrounds[LectureRoom];
        MatchChoices.chosenStage = Stages.LECTURE;


    }

    //St Paul Street Image
    public void StPaulImageChanger()
    {
        GameObject.Find("Background").GetComponent<Image>().sprite = backgrounds[StPaulStreet];
        MatchChoices.chosenStage = Stages.STPAULS;


    }

    //Symonds Street Image
    public void SymondStreetImageChanger()
    {
        GameObject.Find("Background").GetComponent<Image>().sprite = backgrounds[SymondsStreet];
        MatchChoices.chosenStage = Stages.CROSSING;

    }


    //Wellesley Street Image

    public void WellesleyImage()
    {
        GameObject.Find("Background").GetComponent<Image>().sprite = backgrounds[WellesleyStreet];
        MatchChoices.chosenStage = Stages.WELLESLEY;


    }

}

