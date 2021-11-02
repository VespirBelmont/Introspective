using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public Sprite[] spriteList;

    public bool showcaseMode = false;
    public float showcaseRefreshRate = 1f;


    public SpriteRenderer sprite;
    public bool canFlip;
    [Range(0, 10)] public int flipChance;

    // Start is called before the first frame update
    void Start()
    {
        //this.transform.localScale = new Vector3(1, 1, 1);

        if (showcaseMode)
        {
            InvokeRepeating("Randomize", 1f, showcaseRefreshRate);
        }
        else
            Randomize();

    }

    void Randomize()
    {
        Sprite newSprite = spriteList[Random.Range(0, spriteList.Length)];
        this.GetComponent<SpriteRenderer>().sprite = newSprite;

        if (canFlip)
        {
            int flipRoll = Random.Range(0, 10);

            if (flipChance <= flipRoll)
            {
                sprite.flipX = true;
            }
        }
    }
}
