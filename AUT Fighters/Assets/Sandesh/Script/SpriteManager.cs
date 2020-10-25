using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public Image p1Image;
    public Image p2Image;

    public Sprite[] charactersprites;

    // Start is called before the first frame update
    void Start()
    {
        p1Image.sprite = findSprite(MatchChoices.p1Character);
        p2Image.sprite = findSprite(MatchChoices.p2Character);
    }

    public Sprite findSprite(Characters character)
    {
        if(character == Characters.SAN)
        {
            return charactersprites[3];

        }
        else if(character == Characters.CHARLIE)
        {
            return charactersprites[0];
        }
        else if(character == Characters.LIAM)
        {
            return charactersprites[1];
        }
        else if(character == Characters.MICHAEL)
        {
            return charactersprites[2];
        }
        else
        {
            return null;
            Debug.Log("Character not found");
        }


    }

}
