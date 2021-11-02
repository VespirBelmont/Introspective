using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoulLedger : MonoBehaviour
{
    public TitleScreen sceneManager;

    public GameObject ui;

    public Animator thisAnim;
    public Animator uiAnim;

    bool opened = false;

    PlayerActions controls;

    public UnityEvent openLedger;

    // Start is called before the first frame update
    void Start()
    {
        controls = new PlayerActions();

        controls.UI.Interact.performed += ctx => ActivateLedger();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShowLedger();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        HideLedger();
    }


    public void ShowLedger()
    {
        ui.SetActive(true);
        controls.Enable();
    }

    public void HideLedger()
    {
        ui.SetActive(false);
        controls.Disable();
    }

    void ActivateLedger()
    {
        if (opened)
            return;

        opened = true;

        openLedger.Invoke();
        sceneManager.GetPlayerCharacter().GetComponent<CharacterController2D>().ToggleControls(false);
        thisAnim.SetBool("Opened", true);
        uiAnim.SetBool("Opened", true);
        HideLedger();
    }

    public void ShutDownLedger()
    {
        thisAnim.SetBool("Opened", false);
        opened = false;
        ShowLedger();
        sceneManager.GetPlayerCharacter().GetComponent<CharacterController2D>().ToggleControls(true);
    }
}
