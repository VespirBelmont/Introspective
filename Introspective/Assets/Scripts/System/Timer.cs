using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{

    public bool repeating = false;
    public bool autoStart = true;
    public float timerDuration = 1f;

    public bool randomizeDuration = false;
    public float randomizeMin = 0.5f;
    public float randomizeMax = 2f;

	[Header("Events")]
	[Space]

	public UnityEvent OnTimeOut;

    private void Start()
    {
        if (randomizeDuration)
        {
            timerDuration = Random.RandomRange(randomizeMin, randomizeMax);
        }

        if (autoStart)
        {
            if (repeating)
            {
                InvokeRepeating("TimedOut", timerDuration, timerDuration);
            }
            else
            {
                Invoke("TimedOut", timerDuration);
            }
        }
    }

    public void StartTimer()
    {
        Invoke("TimedOut", timerDuration);
    }
    private void TimedOut()
    {
		OnTimeOut.Invoke();
    }
}
