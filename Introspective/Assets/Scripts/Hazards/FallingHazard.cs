using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FallingHazard : MonoBehaviour
{
    [Header ("Settings")]
    [Space]
    public string[] collideableTags;
    public bool isSleeping = false;

    private Rigidbody2D body;

    [Header("Events")]
    [Space]
    public UnityEvent OnImpact;


    private void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        if (body == null)
        {
            body = this.GetComponent<Rigidbody2D>();
        }

        if (isSleeping)
        {
            body.Sleep();
           body.simulated = false;
        }
    }

    public void Fall()
    {
        if (this.GetComponent<Animator>() != null)
        {
            this.GetComponent<Animator>().SetBool("Fell", true);
        }

        ActivateHazard();
    }

    private void ActivateHazard()
    {
        isSleeping = false;
        body.simulated = true;
        body.WakeUp();
        //this.GetComponent<Rigidbody2D>().WakeUp();
    }

    public void ChangeGravity(float newGrav)
    {
        this.GetComponent<Rigidbody2D>().gravityScale = newGrav;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool properCollision = false;

        for (int t = 0; t <= collideableTags.Length-1; t++)
        {
            if (collision.gameObject.CompareTag(collideableTags[t]))
            {
                properCollision = true;
                break;
            }
        }

        if (properCollision == false)
        {
            return;
        }

        OnImpact.Invoke();
    }
}
