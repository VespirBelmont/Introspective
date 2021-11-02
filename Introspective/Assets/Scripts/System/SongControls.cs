using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongControls : MonoBehaviour
{
    public AudioSource[] audioList;

    public void PlayAudio(string audioName)
    {
        AudioSource targetAudio = FindAudio(audioName);
        
        for (float d = 0.3f; d >= targetAudio.volume;)
        {
            targetAudio.volume += 0.02f;
        }
    }

    public void MuteAudio(string audioName)
    {
        AudioSource targetAudio = FindAudio(audioName);

        targetAudio.volume = 0;
    }

    AudioSource FindAudio(string audioName)
    {

        for (int s = 0; s <= audioList.Length-1; s++)
        {
            AudioSource targetAudio = audioList[s];

            if (targetAudio.name == audioName)
            {
                return targetAudio;
            }
        }

        return null;
    }
}
