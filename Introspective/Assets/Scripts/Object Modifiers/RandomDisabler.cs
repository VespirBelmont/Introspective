using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDisabler : MonoBehaviour
{
    public bool useDisabler = true;

    public bool showcaseMode = false;
    public float showcaseRefreshRate = 1f;

    [Range(0f, 10f)] public float disableChance = 5f; 

    public bool randomizeChance = true;
    [Range(0f, 10f)] public float disableChanceMin;
    [Range(0f, 10f)] public float disableChanceMax;

    // Start is called before the first frame update
    void Start()
    {
        if (useDisabler == false)
        {
            return;
        }

        if (randomizeChance)
            disableChance = Random.Range(disableChanceMin, disableChanceMax);

        if (showcaseMode)
        {
            this.gameObject.SetActive(true);
            InvokeRepeating("RandomizeDisable", 1f, showcaseRefreshRate);
        }
        else
            RandomizeDisable();
    }

    void RandomizeDisable()
    {

        float randNum = Random.Range(0f, 10f);

        if (randNum <= disableChance)
        {
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
