using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public string[] targetTags;

    [Header("Events")]
	[Space]

	public UnityEvent OnTrigger;
	public UnityEvent OnExit;

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
            OnTrigger.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        bool properCollision = false;

        for (int t = 0; t <= targetTags.Length - 1; t++)
        {
            if (collision.gameObject.CompareTag(targetTags[t]))
            {
                properCollision = true;
                break;
            }
        }

        if (properCollision)
        {
            OnExit.Invoke();
        }
    }
}
