using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightBlink : MonoBehaviour
{
    public Light2D light;

    public float intensityMin;
    public float intensityMax;
    public float intensitySpeed;

    public void Blink()
    {
        float randLighting = Random.Range(intensityMin, intensityMax);
        float lerpValue = Mathf.Lerp(light.intensity, randLighting ,intensitySpeed);

        light.intensity = lerpValue;
    }
}
