using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarChunkInfo : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject player;
    public GameObject camera;
    public AudioManager audioManager;

    public void Setup(GameObject _gameManager, GameObject _camObject, GameObject _audioManager)
    {
        gameManager = _gameManager.GetComponent<GameManager>();
        player = gameManager.currentPlayer;
        camera = _camObject;
        audioManager = _audioManager.GetComponent<AudioManager>();
    }

    public void SwitchCamViewRelay(string viewName)
    {
        camera.GetComponent<CameraRig>().SwitchView(viewName);
    }

    public void ChangeMoveSpeedRelay(string moveSpeed)
    {
        player.GetComponent<PlayerMovement>().ChangeMoveSpeed(moveSpeed);
    }


    public void SetCurrentMusic(string songName)
    {
        audioManager.SetCurrentMusic(songName);
    }

    public void PlayMusic(string audioName)
    {
        audioManager.PlayMusic(audioName);
    }

    public void StopMusic(string audioName)
    {
        audioManager.StopMusic(audioName);
    }


    public void RunFinished()
    {
        gameManager.EndGame();
    }
}
