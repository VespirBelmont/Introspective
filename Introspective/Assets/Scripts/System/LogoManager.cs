using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoManager : MonoBehaviour
{
    private void Awake()
    {
        Invoke("StartGame", 3f);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
