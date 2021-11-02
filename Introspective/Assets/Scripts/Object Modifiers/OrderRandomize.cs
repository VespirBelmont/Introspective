using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderRandomize : MonoBehaviour
{
    public bool showcaseMode = false;

    public SpriteRenderer sprite;

    public bool randomizeOrder = true;

    [Range(-10f, 7f)] public int orderMin;
    [Range(-10f, 7f)] public int orderMax;

    // Start is called before the first frame update
    void Start()
    {
        if (randomizeOrder == false)
            return;

        if (showcaseMode)
        {
            InvokeRepeating("Randomize", 1f, 2f);
        }
        else
            Randomize();
    }

    void Randomize()
    {
        int newOrder = Random.Range(orderMin, orderMax);

        sprite.sortingOrder = newOrder;
    }    
}
