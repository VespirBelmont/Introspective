using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{

    public Animator introAnim;
    public Animator playerAnim;

    PlayerActions controls;

    public AudioSource footsteps;
    public AudioSource trainEngine;

    public string currentCharacterName;
    public GameObject characterList;

    public int runCount = 0;

    bool canStartGame = false;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerActions();

        controls.UI.Interact.performed += ctx => StartGame();

        SaveData data = SaveFunction.LoadTheGame();

        runCount = data.runCount;
    }

    public void ToggleCanStart(bool state)
    {
        canStartGame = state;
    }

    public void EnableController()
    {
        controls.Enable();
    }

    private void StartGame()
    {
        if (canStartGame == false)
        {
            return;
        }

        introAnim.SetBool("GameStarted", true);
        trainEngine.Play();
        controls.Disable();
        Invoke("ChangeScene", 5f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }

    public GameObject GetPlayerCharacter()
    {
        GameObject character = characterList.transform.Find(currentCharacterName).gameObject;
        return character;
    }


    public void SwitchCharacter()
    {

    }
}
