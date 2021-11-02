using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RunSettings
{
    public static string currentCharacter = "Hollow";

    public static string deathRule;

    public static void SwitchCharacter(string characterName)
    {
        currentCharacter = characterName;
    }
}
