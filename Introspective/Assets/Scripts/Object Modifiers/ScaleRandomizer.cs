using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRandomizer : MonoBehaviour
{

    public bool showcaseMode = false;

    public float scaleRangeMin;
    public float scaleRangeMax;

    // Start is called before the first frame update
    void Start()
    {

        if (showcaseMode)
            InvokeRepeating("Randomize", 1f, 2f);
        else
            Randomize();

    }


    void Randomize()
    {
        float randomScale = Random.Range(scaleRangeMin, scaleRangeMax);

        this.transform.localScale = new Vector2(randomScale, randomScale);
    }
}
