using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRandomizer : MonoBehaviour
{
    #region Variables
    [Header("Order Settings")]
    public int orderMin = -9;
    public int orderMax = -5;

    [Header("Color Settings")]
    public Color closeColor;
    public Color awayColor;
    public Color farAwayColor;

    [Header("Scale Settings")]
    private Vector3 defaultScale;
    private float awayScaleMod = 0.6f;
    private float farAwayScaleMod = 0.4f;

    [Header("Required References")]
    private SpriteRenderer sprite;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //This sets the default scale of the building
        defaultScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
    }


    public void Awake()
    {
        //When the object wakes up it will randomize the building
        Randomize();
    }

    //This handles all the randomization of the building
    private void Randomize()
    {
        #region Crash Avoidance
        //A check to make sure the sprite isn't non-existant; or we crash boi
        if (sprite == null)
        {
            //But if it non-existant we just patch it up real quick
            sprite = this.transform.Find("Sprite").GetComponent<SpriteRenderer>();
        }
        #endregion

        sprite.sortingOrder = Random.Range(orderMin, orderMax); //This sets the building into a random Order between our min and max
        sprite.transform.localScale = defaultScale; // This sets the scale to default just to make sure it's back to factory default

        #region Order Modifications
        //Now we do an unoptimized thing and find the distance the building is to the train

        //Close Distance
        if (sprite.sortingOrder >= -6)
        {
            gameObject.transform.localScale *= 0.8f; //We multiply it by a modifier to get a new size to fit the distance
            sprite.color = closeColor; //And we set the color to the perspective color

            //This is the same for the rest as well so use this as a reference
        }

        //Away Distance
        if (sprite.sortingOrder >= -8 && sprite.sortingOrder < -6)
        {
            sprite.color = awayColor;
            gameObject.transform.localScale *= awayScaleMod;
        }

        //Far Away Distance
        if (sprite.sortingOrder == -9)
        {
            sprite.color = farAwayColor;
            gameObject.transform.localScale *= farAwayScaleMod;
        }
        #endregion

        //Now we just set the transform to 0 on the Z axis
        //Why?
        //Honestly no idea why it happens but the buildings get set to a weird Z axis that messes it all up
        //And this fixes it
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);
    }
}
