using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerateList : MonoBehaviour
{
    public GameObject[] startCars;
    public GameObject[] denialCars;
    public GameObject[] isolationCars;
    public GameObject[] angerCars;
    public GameObject[] sorrowCars;
    public GameObject[] acceptanceCars;
    public GameObject[] inbetweenCars;
    public GameObject[] restCars;


    public GameObject GetRandomCar(string carCategory)
    {
        GameObject newCar = null;
        GameObject[] newList = startCars;
            
        switch (carCategory)
        {
            case ("Start"):
                newList = startCars;
                break;

            case ("Denial"):
                newList = denialCars;
                break;

            case ("Isolation"):
                newList = isolationCars;
                break;

            case ("Anger"):
                newList = angerCars;
                break;

            case ("Sorrow"):
                newList = sorrowCars;
                break;

            case ("Acceptance"):
                newList = acceptanceCars;
                break;

            case ("InBetween"):
                newList = inbetweenCars;
                break;
        }

        newCar = newList[Random.Range(0, newList.Length - 1)];

        return newCar;
    }
}
