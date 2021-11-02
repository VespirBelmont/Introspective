using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhoutteManager : MonoBehaviour
{
    [Header("Animator Controller")]
    [Space]
    public bool stretchOn;
    private int selectionValue = 1;

    [Header("Sprite Controller")]
    [Space]
    public SpriteRenderer sprite;
    public bool canFlip;
    [Range(0, 1)] public float flipChance;

    // Start is called before the first frame update
    void Start()
    {
        Animator thisAnim = this.GetComponent<Animator>();

        selectionValue = Random.Range(1, 3);

        thisAnim.SetBool("Stretch", stretchOn);
        thisAnim.SetInteger("Value", selectionValue);

        float flipChanceRoll = Random.Range(0f, 1f);
        if (flipChanceRoll <= flipChance)
        {
            sprite.flipX = true;
        }

    }
}