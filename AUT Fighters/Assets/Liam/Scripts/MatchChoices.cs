using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Characters
{
    SAN,
    CHARLIE,
    LIAM,
    MICHAEL
}

public class MatchChoices
{
    public static Characters p1Character = Characters.LIAM;
    public static Characters p2Character = Characters.MICHAEL;

    public static string chosenStage = Stages.CROSSING;
}
