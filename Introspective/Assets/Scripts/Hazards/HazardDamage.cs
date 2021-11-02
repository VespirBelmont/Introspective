using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HazardDamage : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    public int damage = 1;
    public GameObject hostObject;
    public string[] targetTags;

    [Header ("Events")]
    [Space]
    public UnityEvent OnHit;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool properCollision = false;

        for (int t = 0; t <= targetTags.Length-1; t++)
        {
            if (collision.gameObject.CompareTag(targetTags[t]))
            {
                properCollision = true;
                break;
            }
        }

        if (properCollision)
        {
            // Will be changed to health component later
            collision.GetComponent<Health>().TakeDamage(damage, this.transform.position.x);
            OnHit.Invoke();
        }
    }
}
