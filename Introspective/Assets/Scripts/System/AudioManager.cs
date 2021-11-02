using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject currentMusic;

    public void SetCurrentMusic(string songName)
    {
        currentMusic = this.transform.Find("Music").transform.Find(songName).gameObject;
    }

    public void PlayMusic(string audioName)
    {
        currentMusic.GetComponent<SongControls>().PlayAudio(audioName);
    }

    public void StopMusic(string audioName)
    {
        currentMusic.GetComponent<SongControls>().MuteAudio(audioName);
    }
}
