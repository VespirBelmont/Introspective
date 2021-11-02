using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    public Transform objectList;

    public GameObject SelectRandomObject()
    {
        int rangeMax = objectList.childCount - 1;
        GameObject newObj = objectList.GetChild(Random.Range(0, rangeMax)).gameObject;

        return newObj;
    }
}
