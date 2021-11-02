using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public string currentCharacter = "Hollow";

    public Transform player;

    public void SetNewCharacter(string newCharacter)
    {
        currentCharacter = newCharacter;
    }
}
