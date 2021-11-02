using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : MonoBehaviour
{

    public bool autoPlay = false;

    public AudioClip[] audioList;

    [Header("Audio Settings")]
    public float volumeMax;
    public float volumeMin;


    private void Start()
    {
        if (autoPlay)
            play_sound();
    }

    public void play_sound()
    {
        AudioClip newClip = audioList[Random.Range(0, audioList.Length)];
        this.GetComponent<AudioSource>().clip = newClip;
        this.GetComponent<AudioSource>().Play();
    }
}
