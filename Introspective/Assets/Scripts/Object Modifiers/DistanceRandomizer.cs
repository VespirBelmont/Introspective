using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceRandomizer : MonoBehaviour
{
    public int distanceMin = 0;
    public int distanceMax = 0;

    public Color closeColor;
    public Color awayColor;
    public Color farAwayColor;

    public float closeScaleMod;
    public float awayScaleMod;
    public float farAwayScaleMod;

    public int closeRange;
    public int awayRange;
    public int farAwayRange;

    private SpriteRenderer lightingSprite;
    private Mover mover;

    public bool showcaseMode = false;
    public float showcaseRefreshRate = 1f;

    private void Start()
    {
        if (showcaseMode)
            InvokeRepeating("Randomize", 1f, showcaseRefreshRate);
        else
            Randomize();
    }

    void Randomize()
    {
        mover = this.GetComponent<Mover>();
        lightingSprite = this.transform.Find("LightingSprite").GetComponent<SpriteRenderer>();
        lightingSprite.sprite = this.GetComponent<SpriteRenderer>().sprite;
        //lightingSprite.enabled = false;
        int randomOrder = Random.Range(distanceMin, distanceMax);
        this.GetComponent<SpriteRenderer>().sortingOrder = randomOrder;
        lightingSprite.sortingOrder = randomOrder + 1;

        closeColor.a = 130;
        awayColor.a = 130;
        farAwayColor.a = 130;

        if (randomOrder >= closeRange)
        {
            lightingSprite.color = closeColor;
            SpriteRenderer thisSprite = this.GetComponent<SpriteRenderer>();
            Color newColor = new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, thisSprite.color.a * closeScaleMod);
            thisSprite.color = newColor;
            this.transform.localScale *= closeScaleMod;
            mover.moveSpeed *= closeScaleMod;
        }

        if (randomOrder >= awayRange && randomOrder < closeRange)
        {
            lightingSprite.color = awayColor;
            SpriteRenderer thisSprite = this.GetComponent<SpriteRenderer>();
            Color newColor = new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, thisSprite.color.a * awayScaleMod);
            thisSprite.color = newColor;
            this.transform.localScale *= awayScaleMod;
            mover.moveSpeed *= awayScaleMod;
        }
        else if (randomOrder <= farAwayRange)
        {
            lightingSprite.color = farAwayColor;
            SpriteRenderer thisSprite = this.GetComponent<SpriteRenderer>();
            Color newColor = new Color(thisSprite.color.r, thisSprite.color.g, thisSprite.color.b, thisSprite.color.a * farAwayScaleMod);
            thisSprite.color = newColor;
            this.transform.localScale *= farAwayScaleMod;
            mover.moveSpeed *= farAwayScaleMod;
        }
    }
}
