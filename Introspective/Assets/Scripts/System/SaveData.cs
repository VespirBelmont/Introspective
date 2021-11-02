using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int runCount = 0;

    public SaveData(int newrunCount)
    {
        runCount = newrunCount;
    }
}
