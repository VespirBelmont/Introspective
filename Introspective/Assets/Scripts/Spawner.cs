using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    public GameObject spawnObject;
    public GameObject objectPool;
    public Transform spawnPosList;

    public int poolAmount;


    private void Start()
    {
        CreatePool();
    }

    public void CreatePool()
    {

        for (int i = 0; i < poolAmount; i++)
        {
            GameObject newObj = Instantiate(spawnObject);
            newObj.SetActive(false);
            newObj.transform.SetParent(objectPool.transform);
        }
    }

    public void Spawn()
    {
        Transform spawnPos = spawnPosList.GetChild(Random.Range(0, spawnPosList.childCount - 1));

        for (int o = 0; o <= objectPool.transform.childCount-1; o++)
        {
            if (o > objectPool.transform.childCount-1)
            {
                break;
            }

            GameObject focusedObject = objectPool.transform.GetChild(o).gameObject;

            if (focusedObject.activeSelf == false)
            {
                focusedObject.transform.position = spawnPos.position;
                focusedObject.SetActive(true);
                break;
            }
        }
    }
}
