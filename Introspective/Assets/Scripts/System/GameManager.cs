using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator endGameAnim;

    public GameObject characterList;
    public GameObject currentPlayer;

    private int runCount = 0;
    private void Start()
    {
        SaveData data = SaveFunction.LoadTheGame();

        runCount = data.runCount;

        GetPlayer();
    }

    public void EndGame()
    {
        endGameAnim.SetBool("GameFinished", true);
        Invoke("GoTitle", 6);

        runCount += 1;
        SaveFunction.SaveTheGame(runCount);
    }

    public void GoTitle()
    {
        SceneManager.LoadScene(1);
    }

    void GetPlayer()
    {
        currentPlayer = characterList.transform.Find(RunSettings.currentCharacter).gameObject;
    }
    
}
